#include "SaveFileGenerator.h"
#include "../../../lib/json/src/json.hpp"
#include "../SubTileFactory.h"

std::string level::SaveFileGenerator::get_info(std::shared_ptr<scenes::Game> game)
{
    nlohmann::json json;

    // Info
    std::shared_ptr<Player> player = game->get_player();

    int health = player->get_health_points_start_round();

    json["player"]["maxhealth"] = player->get_max_health_points();
    json["player"]["mindamage"] = player->get_minimum_damage();
    json["player"]["maxdamage"] = player->get_maximum_damage();

    // Level, round and score progress
    int level = game->get_level();
    int round = game->get_round();
    int score = game->get_score_start_round();

    if (round == 0)
    {
        round = 1;
    }
    else if (game->get_enemies().empty())
    {
        round++;
        health = player->get_health_points();
        score = game->get_score();
        if (round == 11)
        {
            round = 1;
            level++;
        }
    }

    json["player"]["health"] = health;
    json["player"]["score"] = score;
    json["game"]["round"] = round;
    json["game"]["level"] = level;

    // md5 hash
    save_file_integrity_checker.generate_md5_info(level, round, score, player->get_max_health_points(),
                                                  health, player->get_minimum_damage(),
                                                  player->get_maximum_damage());

    json["integrity"] = save_file_integrity_checker.get_md5_hash();

    return json.dump();
}

std::string level::SaveFileGenerator::get_map(std::shared_ptr<scenes::Game> game)
{
    std::shared_ptr<engine::Map> map = game->get_map();
    LevelMatrix map_matrix = map->get_dungeon()->get_map();
    int height = map->get_total_height() / TILE_SIZE / 8;
    int width = map->get_total_width() / TILE_SIZE / 8;

    nlohmann::json json;

    json["height"] = height;
    json["width"] = width;

    // Tile layer
    json["layers"][0]["height"] = height;
    json["layers"][0]["width"] = width;

    // Objects
    SubTileFactory sub_tile_factory;
    std::unordered_map<int, std::vector<std::vector<level::GridTile>>> tileset = sub_tile_factory.get_tileset();

    int id = 0;
    int x = 0;
    int y = 2048;
    bool camp_added = false;

    for (int row = 0; row < height; row++)
    {
        x = 0;

        for (int column = 0; column < width; column++)
        {
            // Check if matrix is a camp matrix
            std::shared_ptr<GridTile> initial_tile = map_matrix.at(row * 8).at(column * 8);

            if (initial_tile->get_grid_tile_type() != campfire)
            {
                int gid = 8;
                int width_tile = 4096;
                int height_tile = 4096;
                int rotation = 0;

                if (initial_tile->get_grid_tile_type() != camp)
                {
                    width_tile /= 2;
                    height_tile /= 2;

                    // Create matrix
                    std::vector<std::vector<level::GridTile>> grid = create_grid(map_matrix, row, column);

                    bool grid_found = false;
                    while (!grid_found)
                    {
                        for (auto& value : tileset)
                        {
                            if (!compare_grids(value.second, grid)) continue;

                            gid = value.first + 1;
                            grid_found = true;
                            break;
                        }

                        if (grid_found) continue;

                        rotation += 90;
                        std::vector<std::vector<level::GridTile>> grid_tile_old{grid};

                        for (int i = 0; i < grid_tile_old.size(); ++i)
                        {
                            for (int j = 0; j < grid_tile_old.at(i).size(); ++j)
                            {
                                grid.at(i).at(j) = grid_tile_old.at(grid_tile_old.size() - j - 1).at(i);
                            }
                        }
                    }
                }

                int x_modification = 0;
                int y_modification = 0;

                if (rotation != 0)
                {
                    rotation = 360 - rotation;

                    if (rotation == 90 || rotation == 180)
                    {
                        y_modification = -height_tile;
                    }
                    if (rotation == 180 || rotation == 270)
                    {
                        x_modification = width_tile;
                    }
                }

                if (initial_tile->get_grid_tile_type() != camp || !camp_added)
                {
                    if (initial_tile->get_grid_tile_type() == camp)
                    {
                        y_modification += 2048;
                        camp_added = true;
                    }

                    json["layers"][1]["objects"][id]["gid"] = gid;
                    json["layers"][1]["objects"][id]["x"] = x + x_modification;
                    json["layers"][1]["objects"][id]["y"] = y + y_modification;
                    json["layers"][1]["objects"][id]["rotation"] = rotation;
                    json["layers"][1]["objects"][id]["width"] = width_tile;
                    json["layers"][1]["objects"][id]["height"] = height_tile;

                    id++;
                }
            }

            x += 2048;
        }

        y += 2048;
    }

    std::string json_map = json.dump();

    save_file_integrity_checker.generate_md5_map(json_map);

    return json_map;
}

std::vector<std::vector<level::GridTile>> level::SaveFileGenerator::create_grid(LevelMatrix map_matrix,
                                                                                int row, int column)
{
    std::vector<std::vector<level::GridTile>> grid;

    for (int r = 0; r < 8; r++)
    {
        std::vector<level::GridTile> grid_row;

        for (int c = 0; c < 8; c++)
        {
            std::shared_ptr<level::GridTile> tile = map_matrix.at((row * 8) + r).at((column * 8) + c);
            level::GridTile grid_tile{tile->get_grid_tile_type()};
            grid_row.push_back(grid_tile);
        }

        grid.push_back(grid_row);
    }

    return grid;
}

bool level::SaveFileGenerator::compare_grids(std::vector<std::vector<level::GridTile>> grid1,
                                             std::vector<std::vector<level::GridTile>> grid2)
{
    bool is_the_same = true;

    for (int r = 0; r < grid1.size(); r++)
    {
        for (int c = 0; c < grid1.at(r).size(); c++)
        {
            if (grid1.at(r).at(c).get_grid_tile_type() != grid2.at(r).at(c).get_grid_tile_type())
            {
                is_the_same = false;
                break;
            }
        }

        if (!is_the_same) break;
    }

    return is_the_same;
}
