#ifndef SDL_PROJECT_SUBTILEFACTORY_H
#define SDL_PROJECT_SUBTILEFACTORY_H

#include <unordered_map>
#include "../aliases.h"

#define GRID_SIZE 8

namespace level
{
    class SubTileFactory
    {
        int tile_height_;
        int tile_width_;

        typedef std::unordered_map<int, std::vector<std::vector<level::GridTile>>> tiles_tileset;
        tiles_tileset tiles_tileset_;

        LevelMatrix create_tile(GridTileTypes type, bool north_is_open, bool east_is_open, bool south_is_open, bool west_is_open);

        LevelMatrix combine_tiles_vertical(LevelMatrix& tile1, LevelMatrix& tile2);

        LevelMatrix combine_tiles_horizontal(LevelMatrix& tile1, LevelMatrix& tile2);

        std::string read_json(std::string path);

        const std::vector<std::string> explode(const std::string& s, const char& c);

        LevelMatrix link_tiles(LevelMatrix grid) const;

    public:
        SubTileFactory() = default;

        ~SubTileFactory() = default;

        LevelMatrix create_tiles(int height_map, int width_map, BaseLevelMatrix map);

        void fill_tiles_tileset();

        LevelMatrix create_tiles(std::string map_path);

        std::unordered_map<int, std::vector<std::vector<level::GridTile>>> get_tileset();
    };
}

#endif //SDL_PROJECT_SUBTILEFACTORY_H
