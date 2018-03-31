#include <iostream>
#include "HudBar.h"
#include "../../../Engine/Engine.h"

#define BAR_HEIGHT 20
#define BAR_WIDTH 150

HudBar::HudBar(int x, int y, SDL_Color bar_color, int max_value, Position anchor) :
    Renderable(x, y),
    max_value(max_value), value(max_value), bar_color(bar_color)
{
    this->anchor = anchor;
    layer = HUD_LAYER;
    build_texture();
}

SDL_Surface* HudBar::build_surface()
{
    auto surface = SDL_CreateRGBSurfaceWithFormat(0, BAR_WIDTH, BAR_HEIGHT, 32, SDL_PIXELFORMAT_RGB24);

    if (value < max_value)
    {
        SDL_FillRect(surface, nullptr, SDL_MapRGB(surface->format, background.r, background.g, background.b));
    }

    if (value > 0)
    {
        SDL_Rect rect = {0, 0, static_cast<int>(BAR_WIDTH * ((float) value / max_value)), BAR_HEIGHT};
        SDL_FillRect(surface, &rect, SDL_MapRGB(surface->format, bar_color.r, bar_color.g, bar_color.b));
    }

    return surface;
}

void HudBar::build_texture()
{
    auto surface = build_surface();

    texture = SDL_CreateTextureFromSurface(Engine::instance()->get_renderer(), surface);

    SDL_FreeSurface(surface);
}

int HudBar::get_value()
{
    return value;
}

void HudBar::set_value(int value)
{
    if (this->value != value)
    {
        this->value = value;
        build_texture();
    }
}

int HudBar::get_max_value()
{
    return max_value;
}

void HudBar::set_max_value(int max_value)
{
    if (this->max_value != max_value)
    {
        this->max_value = max_value;
        build_texture();
    }
}

SDL_Color HudBar::get_bar_color()
{
    return bar_color;
}

void HudBar::set_bar_color(SDL_Color color)
{
    bar_color = color;
    build_texture();
}
