#include "Map.h"
#include "../Engine.h"
#include "../../Utilities/Random.h"
#include <iostream>

engine::Map::Map(std::shared_ptr<LevelLoader> loader) : loader(loader)
{
    loader->load_level();
    setup_map();
}

engine::Map::Map(std::shared_ptr<LevelLoader> loader, int layers) : loader(loader)
{
    loader->load_level(layers);
    setup_map();
}

engine::Map::~Map()
{
}

void engine::Map::setup_map()
{
    path_texture = Engine::instance()->load_image(path_textures.at(Random::instance().get(path_textures.size() - 1)));
    border_texture = Engine::instance()->load_image(
        border_textures.at(Random::instance().get(border_textures.size() - 1)));

    // Size map
    total_width = static_cast<int>(TILE_SIZE * get_map().size());
    total_height = static_cast<int>(TILE_SIZE * get_map().at(0).size());

    int min_x = -1;
    int min_y = -1;

    int x = 0;
    int y = 0;

    auto map = get_map();
    for (const auto& row : map)
    {
        x = 0;

        for (const auto& tile : row)
        {
            if (tile->get_grid_tile_type() == GridTileTypes::camp)
            {
                if (min_x < 0 || x < min_x)
                {
                    min_x = x;
                }

                if (min_y < 0 || y < min_y)
                {
                    min_y = y;
                }
            }

            x++;
        }

        y++;
    }

    if (min_x < 0 || min_y < 0)
    {
        std::cerr << "Invalid Map (No Camp)" << std::endl;
        exit(1);
    }

    campfire = Coordinate{min_x + 7, min_y + 7};
    portal = Coordinate{min_x + 3, min_y + 3};

    map.at(min_y + 8).at(min_x + 7)->set_grid_tile_type(GridTileTypes::campfire);
    map.at(min_y + 8).at(min_x + 8)->set_grid_tile_type(GridTileTypes::campfire);

    map.at(min_y + 3).at(min_x + 3)->set_grid_tile_type(GridTileTypes::portal);
    map.at(min_y + 3).at(min_x + 4)->set_grid_tile_type(GridTileTypes::portal);
    map.at(min_y + 4).at(min_x + 3)->set_grid_tile_type(GridTileTypes::portal);
    map.at(min_y + 4).at(min_x + 4)->set_grid_tile_type(GridTileTypes::portal);

    this->x = ((min_x + 5) * TILE_SIZE) + (TILE_SIZE / 2) - (total_width / 2);
    this->y = ((min_y + 8) * TILE_SIZE) - (total_height / 2);
}

std::shared_ptr<level::Dungeon> engine::Map::get_dungeon()
{
    return loader->get_dungeon();
}

LevelMatrix engine::Map::get_map()
{
    return get_dungeon()->get_map();
}

int engine::Map::get_total_width()
{
    return total_width;
}

int engine::Map::get_total_height()
{
    return total_height;
}

Coordinate engine::Map::campfire_location()
{
    return campfire;
}

Coordinate engine::Map::portal_location()
{
    return portal;
}

SDL_Texture* engine::Map::get_path_texture() const
{
    return path_texture;
}

SDL_Texture* engine::Map::get_border_texture() const
{
    return border_texture;
}
