#include <iostream>
#include "LightOverlay.h"
#include "../../../Engine/Engine.h"

LightOverlay::LightOverlay(int percentage) : Renderable(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), radius_percentage(percentage)
{
    current_size = {SCREEN_WIDTH, SCREEN_HEIGHT};
    layer = OVERLAY_LAYER;

    build_texture();
}

SDL_Surface* LightOverlay::build_surface()
{
    auto surface = SDL_CreateRGBSurfaceWithFormat(0, current_size.w, current_size.h, 32, SDL_PIXELFORMAT_RGB24);
    SDL_FillRect(surface, nullptr, SDL_MapRGB(surface->format, 0, 0, 0));

    auto pixel = SDL_CreateRGBSurfaceWithFormat(0, 5, 5, 32, SDL_PIXELFORMAT_RGB24);
    auto color_key = SDL_MapRGB(surface->format, 255, 0, 255);
    SDL_FillRect(pixel, nullptr, color_key);

    int x, y, r2;
    int x_center = current_size.w / 2;
    int y_center = current_size.h / 2;

    float radius = calc_radius();

    r2 = radius * radius;
    for (x = -radius; x <= radius; x++)
    {
        y = (int) lround(sqrt(r2 - x * x) + 0.5);

        SDL_Rect pixel_rect = this->get_rect();
        pixel_rect.x = x_center + x;
        pixel_rect.y = y_center + y;

        for (int i = y_center - y; i < y_center + y; i++)
        {
            pixel_rect.y = i;
            SDL_BlitSurface(pixel, nullptr, surface, &pixel_rect);
        }
    }

    SDL_SetColorKey(surface, SDL_TRUE, color_key);

    SDL_FreeSurface(pixel);

    return surface;
}

void LightOverlay::build_texture()
{
    auto surface = build_surface();

    texture = SDL_CreateTextureFromSurface(Engine::instance()->get_renderer(), surface);

    SDL_FreeSurface(surface);
}

SDL_Rect LightOverlay::get_rect()
{
    auto size = Engine::instance()->get_size();

    return {x, y, size.w, size.h};
}

std::shared_ptr<engine::RenderData> LightOverlay::render()
{
    auto compare_size = Engine::instance()->get_size();

    if (current_size.w != compare_size.w || current_size.h != compare_size.h)
    {
        current_size = compare_size;
        build_texture();
    }

    return Renderable::render();
}

int LightOverlay::calc_radius()
{
    auto percentage = ((double) radius_percentage / 2) / 100;

    if (current_size.h >= current_size.w)
    {
        return percentage * current_size.w;
    }
    else
    {
        return percentage * current_size.h;
    }
}

int LightOverlay::get_radius()
{
    return radius_percentage;
}

void LightOverlay::set_radius_percentage(int new_percentage)
{
    radius_percentage = new_percentage;
    build_texture();
}
