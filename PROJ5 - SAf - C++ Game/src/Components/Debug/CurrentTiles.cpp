#include <iostream>
#include "CurrentTiles.h"
#include "../../Engine/Engine.h"

CurrentTiles::CurrentTiles(std::shared_ptr<engine::Entity>& entity, int x, int y) :
    Renderable(x, y, TILE_SIZE, TILE_SIZE),
    entity(entity)
{
    build_texture();
}

SDL_Surface* CurrentTiles::build_surface()
{
    auto surface = SDL_CreateRGBSurfaceWithFormat(0, width, height, 32, SDL_PIXELFORMAT_RGB24);
    if (surface == nullptr)
    {
        std::cerr << "Could not create surface" << std::endl;
        exit(1);
    }

    SDL_FillRect(surface, nullptr, SDL_MapRGB(surface->format, 0, 255, 0));

    return surface;
}

void CurrentTiles::build_texture()
{
    auto surface = build_surface();

    SDL_DestroyTexture(texture);

    texture = SDL_CreateTextureFromSurface(Engine::instance()->get_renderer(), surface);

    SDL_FreeSurface(surface);
}

void CurrentTiles::set_coordinate(int x_offset, int y_offset)
{
    auto size = Engine::instance()->get_size();
    auto rect = entity->get_rect();

    auto center_x = size.w / 2;
    auto center_y = size.h / 2;

    auto mod_x = (x_offset - (center_x - rect.x)) % TILE_SIZE;
    auto mod_y = (y_offset - (center_y - rect.y)) % TILE_SIZE;

    auto tiles_x = rect.x - mod_x;
    auto tiles_y = rect.y - mod_y;

    if (mod_x != 0)
    {
        width = TILE_SIZE * 2;
        if (mod_x < 0)
        {
            tiles_x -= TILE_SIZE;
        }
    }
    else
    {
        width = TILE_SIZE;
    }

    if (mod_y != 0)
    {
        height = TILE_SIZE * 2;
        if (mod_y < 0)
        {
            tiles_y -= TILE_SIZE;
        }
    }
    else
    {
        height = TILE_SIZE;
    }

    x = tiles_x;
    y = tiles_y;
}
