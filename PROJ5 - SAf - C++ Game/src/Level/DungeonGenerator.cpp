#include <list>
#include "DungeonGenerator.h"
#include "Tiles/BorderTile.h"
#include "../Utilities/Random.h"
#include "Tiles/CampTile.h"

level::DungeonGenerator::DungeonGenerator() : width_(0), height_(0)
{
}

std::shared_ptr<level::Dungeon> level::DungeonGenerator::generate_dungeon(int vertical_rows, int horizontal_rows)
{
    this->height_ = (vertical_rows * 2) + 4; // 2 spaces for camp, 2 spaces for border
    this->width_ = (horizontal_rows * 2) + 4; // 2 spaces for camp, 2 spaces for border

    // Create map
    create_map();

    // Generate path
    std::shared_ptr<RoadTile> tile = define_first_tile();
    tile->generate_path();

    // Define start passages
    this->define_start_passages();

    auto factory{SubTileFactory()};
    std::shared_ptr<Dungeon> generated_dungeon{std::make_shared<Dungeon>(factory.create_tiles(height_, width_, map_))};

    return generated_dungeon;
}

void level::DungeonGenerator::create_map()
{
    for (int row = 0; row < height_; row++)
    {
        std::vector<std::shared_ptr<Tile>> map_row;

        for (int column = 0; column < width_; column++)
        {
            std::shared_ptr<Tile> tile = create_tile(row, column);
            map_row.push_back(tile);

            // Relations
            if (row > 0)
            {
                std::shared_ptr<Tile> north_tile = map_.at(row - 1).at(column);
                tile->set_north(north_tile);
                north_tile->set_south(tile);
            }

            if (column > 0)
            {
                std::shared_ptr<Tile> west_tile = map_row.at(column - 1);
                tile->set_west(west_tile);
                west_tile->set_east(tile);
            }
        }

        map_.push_back(map_row);
    }
}

std::shared_ptr<level::Tile> level::DungeonGenerator::create_tile(int row, int column) const
{
    std::shared_ptr<level::Tile> tile;

    int difference_row{row - (height_ / 2)};
    int difference_column{column - (width_ / 2)};
    if ((difference_row == 0 || difference_row == -1) && (difference_column == 0 || difference_column == -1))
    {
        // Camp
        tile = std::shared_ptr<level::Tile>(new level::CampTile(row, column));
    }
    else if (row == 0 || column == 0 || row == this->height_ - 1 || column == this->width_ - 1)
    {
        // BorderTile
        tile = std::shared_ptr<level::Tile>(new level::BorderTile(row, column));
    }
    else
    {
        // Road
        tile = std::shared_ptr<level::Tile>(new RoadTile(row, column));
    }

    return tile;
}

std::shared_ptr<level::RoadTile> level::DungeonGenerator::define_first_tile() const
{
    std::shared_ptr<level::Tile> tile = nullptr;

    do
    {
        const int row_index = Random::instance().get(0, this->height_ - 1);
        const int column_index = Random::instance().get(0, this->width_ - 1);

        tile = map_[row_index][column_index];
    }
    while (!std::dynamic_pointer_cast<RoadTile>(tile));

    return std::dynamic_pointer_cast<RoadTile>(tile);
}

void level::DungeonGenerator::define_start_passages()
{
    // Random number of ways out
    const int number = Random::instance().get(2, 4);

    std::list<std::pair<int, int>> indices;

    // Define start passageways
    for (int i = 0; i < number; i++)
    {
        int difference_row = Random::instance().get(0, 3) - 1;
        int difference_column = 0;

        if (difference_row == 0 || difference_row == 1)
        {
            difference_column = -1;

            if (Random::instance().get(1, 2) == 2)
            {
                difference_column = 2;
            }
        }
        else if (Random::instance().get(1, 2) == 2)
        {
            difference_column = 1;
        }

        // Calculate row and column index
        int row_index = (height_ / 2) - 1 + difference_row;
        int column_index = (width_ / 2) - 1 + difference_column;

        std::shared_ptr<level::Tile> tile = map_[row_index][column_index];

        // Check if this passage already exists
        bool exists = false;

        for (auto& indice : indices)
        {
            if (indice.first == row_index && indice.second == column_index)
            {
                i--;
                exists = true;
                break;
            }
        }

        // Add index if it doesn't exist yet and create passage
        if (!exists)
        {
            const std::pair<int, int> pair{row_index, column_index};
            indices.push_back(pair);
            create_passage(std::dynamic_pointer_cast<RoadTile>(tile));
        }
    }

    // Create extra passages
    const int extra_passages_count = Random::instance().get(width_, width_ + height_);

    for (int i = 0; i < extra_passages_count; i++)
    {
        std::shared_ptr<RoadTile> road = nullptr;

        do
        {
            const int row_index = Random::instance().get(0, this->height_ - 1);
            const int column_index = Random::instance().get(0, this->width_ - 1);

            std::shared_ptr<level::Tile> tile = map_[row_index][column_index];
            road = std::dynamic_pointer_cast<RoadTile>(tile);
        }
        while (road == nullptr);

        road->create_extra_passage();
    }
}

void level::DungeonGenerator::create_passage(std::shared_ptr<RoadTile> road_tile) const
{
    if (!road_tile->get_north().expired() && std::dynamic_pointer_cast<level::CampTile>(road_tile->get_north().lock()))
    {
        road_tile->set_accessible_from_north(true);
        road_tile->get_north().lock()->set_accessible_from_south(true);
    }
    else if (!road_tile->get_east().expired() &&
             std::dynamic_pointer_cast<level::CampTile>(road_tile->get_east().lock()))
    {
        road_tile->set_accessible_from_east(true);
        road_tile->get_east().lock()->set_accessible_from_west(true);
    }
    else if (!road_tile->get_south().expired() &&
             std::dynamic_pointer_cast<level::CampTile>(road_tile->get_south().lock()))
    {
        road_tile->set_accessible_from_south(true);
        road_tile->get_south().lock()->set_accessible_from_north(true);
    }
    else if (!road_tile->get_west().expired() &&
             std::dynamic_pointer_cast<level::CampTile>(road_tile->get_west().lock()))
    {
        road_tile->set_accessible_from_west(true);
        road_tile->get_west().lock()->set_accessible_from_east(true);
    }
}
