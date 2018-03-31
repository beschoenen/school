#ifndef GAME_MAP_H
#define GAME_MAP_H

#include "../../Level/Dungeon.h"
#include "../Level/Base/LevelLoader.h"
#include "../../defines.h"
#include "Renderable.h"
#include <vector>

namespace engine
{
    class Map
    {
        std::shared_ptr<LevelLoader> loader;

        int total_width = 0;
        int total_height = 0;

        Coordinate campfire;
        Coordinate portal;

        std::vector<std::string> path_textures = {
            "tiles/path-tile-1.png",
            "tiles/path-tile-2.png",
            "tiles/path-tile-3.png",
            "tiles/path-tile-4.png"
        };

        std::vector<std::string> border_textures = {
            "tiles/border-tile-1.png",
            "tiles/border-tile-2.png",
            "tiles/border-tile-3.png",
            "tiles/border-tile-4.png"
        };

        SDL_Texture* path_texture;
        SDL_Texture* border_texture;

    public:
        int x = 0;
        int y = 0;

        explicit Map(std::shared_ptr<LevelLoader> loader);

        explicit Map(std::shared_ptr<LevelLoader> loader, int layers);

        ~Map();

        void setup_map();

        int get_total_width();

        int get_total_height();

        std::shared_ptr<level::Dungeon> get_dungeon();

        LevelMatrix get_map();

        Coordinate campfire_location();

        Coordinate portal_location();

        SDL_Texture* get_path_texture() const;

        SDL_Texture* get_border_texture() const;
    };
}


#endif //GAME_MAP_H
