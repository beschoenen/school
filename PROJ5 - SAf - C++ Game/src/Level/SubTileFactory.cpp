#include "SubTileFactory.h"
#include "Tiles/BorderTile.h"
#include "Tiles/CampTile.h"
#include "Models/TileCollisionInfo.h"
#include "../../lib/json/src/json.hpp"
#include <fstream>

std::unordered_map<int, std::vector<std::vector<level::GridTile>>> level::SubTileFactory::get_tileset()
{
    fill_tiles_tileset();

    return tiles_tileset_;
}

LevelMatrix level::SubTileFactory::create_tiles(int height_map, int width_map, BaseLevelMatrix map)
{
    LevelMatrix grid;

    for (int row = 0; row < height_map; row++)
    {
        LevelMatrix grid_row;

        for (int column = 0; column < width_map; column++)
        {
            std::shared_ptr<Tile> tile = map.at(row).at(column);
            LevelMatrix grid_tile;

            if (std::dynamic_pointer_cast<level::CampTile>(tile))
            {
                grid_tile = create_tile(camp, true, true, true, true);
            }
            else if (std::dynamic_pointer_cast<level::BorderTile>(tile))
            {
                grid_tile = create_tile(wall, false, false, false, false);
            }
            else
            {
                grid_tile = create_tile(ground, tile->get_accessible_from_north(), tile->get_accessible_from_east(),
                                        tile->get_accessible_from_south(), tile->get_accessible_from_west());
            }

            // Combine
            if (grid_row.size() == 0)
            {
                grid_row = grid_tile;
            }
            else
            {
                grid_row = combine_tiles_horizontal(grid_row, grid_tile);
            }
        }

        if (grid.size() == 0)
        {
            grid = grid_row;
        }
        else
        {
            grid = combine_tiles_vertical(grid, grid_row);
        }
    }

    // Link
    grid = link_tiles(grid);

    return grid;
}

LevelMatrix level::SubTileFactory::create_tile(GridTileTypes type, bool north_is_open, bool east_is_open,
                                               bool south_is_open, bool west_is_open)
{
    // Create basic tile
    LevelMatrix grid;

    for (int row = 0; row < GRID_SIZE; row++)
    {
        std::vector<std::shared_ptr<level::GridTile>> grid_row;

        for (int column = 0; column < GRID_SIZE; column++)
        {
            std::shared_ptr<level::GridTile> standard_tile{std::make_shared<level::GridTile>(level::GridTile{type})};
            grid_row.push_back(standard_tile);
        }

        grid.push_back(grid_row);
    }

    if (type != ground) return grid;

    // Road tile
    int part_size{GRID_SIZE / 4};

    // Corners
    for (int i = 0; i < part_size; i++)
    {
        for (int j = 0; j < part_size; j++)
        {
            // Top left
            grid[i][j]->set_grid_tile_type(wall);

            // Top right
            grid[i][j + (part_size * 3)]->set_grid_tile_type(wall);

            // Bottom left
            grid[i + (part_size * 3)][j]->set_grid_tile_type(wall);

            // Bottom right
            grid[i + (part_size * 3)][j + (part_size * 3)]->set_grid_tile_type(wall);
        }
    }

    // North and south
    if (!north_is_open || !south_is_open)
    {
        for (int row = 0; row < part_size; row++)
        {
            for (int column = part_size; column < (part_size * 3); column++)
            {
                if (!north_is_open)
                {
                    grid[row][column]->set_grid_tile_type(wall);
                }

                if (!south_is_open)
                {
                    grid[row + (part_size * 3)][column]->set_grid_tile_type(wall);
                }
            }
        }
    }

    // West and east
    if (!west_is_open || !east_is_open)
    {
        for (int column = 0; column < part_size; column++)
        {
            for (int row = part_size; row < (part_size * 3); row++)
            {
                if (!west_is_open)
                {
                    grid[row][column]->set_grid_tile_type(wall);
                }

                if (!east_is_open)
                {
                    grid[row][column + (part_size * 3)]->set_grid_tile_type(wall);
                }
            }
        }
    }

    return grid;
}

LevelMatrix level::SubTileFactory::combine_tiles_vertical(LevelMatrix& tile1, LevelMatrix& tile2)
{
    LevelMatrix result = tile1;
    result.insert(result.end(), tile2.begin(), tile2.end());
    return result;
}

LevelMatrix level::SubTileFactory::combine_tiles_horizontal(LevelMatrix& tile1, LevelMatrix& tile2)
{
    LevelMatrix result;

    if (tile1.size() != tile2.size()) return result;

    for (int row = 0; row < tile1.size(); row++)
    {
        std::vector<std::shared_ptr<level::GridTile>> combined_row;

        for (const auto& column : tile1.at(row))
        {
            combined_row.push_back(column);
        }

        for (const auto& column : tile2.at(row))
        {
            combined_row.push_back(column);
        }

        result.push_back(combined_row);
    }

    return result;
}

std::string level::SubTileFactory::read_json(std::string path)
{
    std::ifstream file(path, std::ifstream::binary);
    std::stringstream ss;
    ss << file.rdbuf();
    const std::string contents = ss.str();

    return contents;
}

const std::vector<std::string> level::SubTileFactory::explode(const std::string& s, const char& c)
{
    std::string buff;
    std::vector<std::string> v;

    for (auto n : s)
    {
        if (n != c)
        {
            buff += n;
        }
        else if (n == c && !buff.empty())
        {
            v.push_back(buff);
            buff = "";
        }
    }

    if (!buff.empty()) v.push_back(buff);

    return v;
}

void level::SubTileFactory::fill_tiles_tileset()
{
    tiles_tileset_.clear();
    std::string tileset{read_json("tile_set.json")};

    auto json = nlohmann::json::parse(tileset);

    tile_height_ = json["tileheight"];
    tile_width_ = json["tilewidth"];

    auto _tiles{json["tiles"]};

    for (int i = 0; i < _tiles.size(); ++i)
    {
        std::vector<std::vector<level::GridTile>> grid;

        std::string n = std::to_string(i);

        // Create Tile
        const auto _name{json["tiles"][n]["image"]};
        std::vector<std::string> v{explode(_name, '/')};
        std::vector<std::string> r{explode(v.back(), '.')};
        std::string name = r.front();

        auto _image{json["tiles"][n]["image"]};
        std::string image = _image;
        int height{json["tiles"][n]["imageheight"]};
        int width{json["tiles"][n]["imagewidth"]};

        // Objects
        std::vector<level::TileCollisionInfo> objects;
        auto sub_obj{json["tiles"][n]["objectgroup"]["objects"]};

        for (auto& j : sub_obj)
        {
            int x{j["x"]};
            int y{j["y"]};

            int height{j["height"]};
            int width{j["width"]};

            auto st = level::TileCollisionInfo(x, y, height, width);
            objects.push_back(st);
        }

        int subtile_size_x = width / 8;
        int subtile_size_y = height / 8;
        int subtile_x = 0;
        int subtile_y = 0;

        //subtiles
        int size_grid = height / tile_height_;

        for (int y = 0; y < size_grid * GRID_SIZE; y++)//for each row
        {
            std::vector<level::GridTile> grid_row;

            for (int x = 0; x < size_grid * GRID_SIZE; x++)//for each column
            {
                bool is_object = false;

                // check with the objects list if there is an object on this sub tile
                for (auto& object : objects)
                {
                    int o_x{object.get_x()};
                    int o_w{object.get_width()};
                    int o_y{object.get_y()};
                    int o_h{object.get_height()};

                    if ((subtile_x >= o_x && (subtile_x + subtile_size_x) <= (o_x + o_w)) &&
                        (subtile_y >= o_y && (subtile_y + subtile_size_y) <= (o_y + o_h)))
                    {
                        is_object = true;
                    }
                }

                // Define type
                GridTileTypes type{ground};

                if (is_object)
                {
                    type = wall;
                }

                std::size_t found = image.find("Camp");
                if (found != std::string::npos)
                {
                    type = camp;
                }

                grid_row.emplace_back(type);

                subtile_x += subtile_size_x;
            }

            grid.push_back(grid_row);

            subtile_x = 0;
            subtile_y += subtile_size_y;
        }

        tiles_tileset_.emplace(i, grid);
    }
}

LevelMatrix level::SubTileFactory::create_tiles(std::string map_path)
{
    fill_tiles_tileset();
    LevelMatrix grid;

    std::string json_map{read_json(map_path)};

    nlohmann::json j = nlohmann::json::parse(json_map);

    int map_height{j["height"]};
    int map_width{j["width"]};

    for (int row = 0; row < map_height * GRID_SIZE; row++)
    {
        std::vector<std::shared_ptr<level::GridTile>> grid_row;
        for (int column = 0; column < map_width * GRID_SIZE; column++)
        {
            std::shared_ptr<level::GridTile> tile{nullptr};
            grid_row.push_back(tile);
        }
        grid.push_back(grid_row);
    }

    auto tiles{j["layers"][1]["objects"]};

    for (int i = 0; i < tiles.size(); ++i)
    {
        int id{j["layers"][1]["objects"][i]["gid"]};
        int x{j["layers"][1]["objects"][i]["x"]};
        int y{j["layers"][1]["objects"][i]["y"]};
        int rotation{j["layers"][1]["objects"][i]["rotation"]};
        int width{j["layers"][1]["objects"][i]["width"]};
        int height{j["layers"][1]["objects"][i]["height"]};

        auto iter = tiles_tileset_.find(id - 1);
        if (iter != tiles_tileset_.end())
        {
            std::vector<std::vector<level::GridTile>> grid_sub_tiles{iter->second};

            int row_size{height / tile_height_};
            int column_size{width / tile_width_};

            int row_index{y / tile_height_ - 1};
            for (int i = 1; i < row_size; i++)
            {
                row_index--;
            }
            int column_index{x / tile_width_};

            // Handle rotation
            if (rotation == 90 || rotation == 180)
            {
                row_index++;
            }
            if (rotation == 180 || rotation == 270)
            {
                column_index--;
            }

            int times_to_rotate{rotation / 90};
            for (int i = 1; i <= times_to_rotate; i++)
            {
                std::vector<std::vector<level::GridTile>> grid_tile_old{grid_sub_tiles};

                for (int i = 0; i < grid_tile_old.size(); ++i)
                {
                    for (int j = 0; j < grid_tile_old.at(i).size(); ++j)
                    {
                        grid_sub_tiles.at(i).at(j) = grid_tile_old.at(grid_tile_old.size() - j - 1).at(i);
                    }
                }
            }

            int ir{row_index * 8};
            int ic{column_index * 8};
            int igr{row_size * GRID_SIZE};
            int igc{column_size * GRID_SIZE};

            for (int grid_row_index = 0; grid_row_index < igr; grid_row_index++)
            {
                for (int grid_column_index = 0; grid_column_index < igc; grid_column_index++)
                {
                    grid.at(ir + grid_row_index).at(ic + grid_column_index) = std::make_shared<level::GridTile>(
                        grid_sub_tiles.at(grid_row_index).at(grid_column_index));
                }
            }
        }
    }

    // Link
    grid = link_tiles(grid);

    return grid;
}

LevelMatrix level::SubTileFactory::link_tiles(LevelMatrix grid) const
{
    // Link
    for (int row = 0; row < grid.size(); row++)
    {
        for (int column = 0; column < grid.at(row).size(); column++)
        {
            std::shared_ptr<level::GridTile> grid_tile_current{grid.at(row).at(column)};
            grid_tile_current->set_row(row);
            grid_tile_current->set_column(column);

            if (row > 0)
            {
                std::shared_ptr<level::GridTile> grid_tile_north{grid.at(row - 1).at(column)};
                grid_tile_current->set_north(grid_tile_north);
                grid_tile_north->set_south(grid_tile_current);
            }

            if (column > 0)
            {
                std::shared_ptr<level::GridTile> grid_tile_west{grid.at(row).at(column - 1)};
                grid_tile_current->set_west(grid_tile_west);
                grid_tile_west->set_east(grid_tile_current);
            }
        }
    }

    return grid;
}
