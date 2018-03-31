#ifndef SDL_PROJECT_DUNGEONGENERATOR_H
#define SDL_PROJECT_DUNGEONGENERATOR_H

#include "Dungeon.h"
#include "SubTileFactory.h"
#include "Tiles/RoadTile.h"

namespace level
{
    class DungeonGenerator
    {
        int width_;
        int height_;
        BaseLevelMatrix map_;

        void create_map();

        std::shared_ptr<Tile> create_tile(int row, int column) const;

        std::shared_ptr<RoadTile> define_first_tile() const;

        void define_start_passages();

        void create_passage(std::shared_ptr<RoadTile> road_tile) const;

    public:
        DungeonGenerator();

        ~DungeonGenerator() = default;

        std::shared_ptr<Dungeon> generate_dungeon(int vertical_rows, int horizontal_rows);
    };
}

#endif //SDL_PROJECT_DUNGEONGENERATOR_H
