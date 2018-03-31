#include <iostream>
#include "Button.h"
#include "../Engine.h"

engine::Button::Button(std::string text, std::function<void()> callback, int x, int y, int width, int height,
                       SDL_Color foreground, SDL_Color background, int font_size, int text_padding, int border_size) :
    Text(std::move(text), FONT, x, y, width, height, foreground, font_size),
    callback(std::move(callback)), background(background), text_padding(text_padding), border_size(border_size)
{
    build_texture();
}

engine::Button::Button(std::string text, std::function<void()> callback, int x, int y, SDL_Color foreground,
                       SDL_Color background, int font_size, int text_padding, int border_size) :
    Button(std::move(text), std::move(callback), x, y, -1, -1,
           foreground, background, font_size, text_padding, border_size)
{
}

SDL_Surface* engine::Button::build_surface()
{
    auto text_surface = Text::build_surface();

    button_rect = SDL_Rect
    {
        x - text_padding - border_size,
        y - text_padding - border_size,
        text_surface->w + ((text_padding + border_size) * 2),
        text_surface->h + ((text_padding + border_size) * 2),
    };

    auto button_surface = SDL_CreateRGBSurfaceWithFormat(0, button_rect.w, button_rect.h, 32, SDL_PIXELFORMAT_RGB24);
    if (button_surface == nullptr)
    {
        std::cerr << "Could not create surface" << std::endl;
        exit(1);
    }

    SDL_FillRect(button_surface, nullptr, SDL_MapRGB(button_surface->format, background.r, background.g, background.b));

    auto text_offset = SDL_Rect
    {
        text_padding + border_size,
        text_padding + border_size,
        0,
        0
    };

    SDL_BlitSurface(text_surface, nullptr, button_surface, &text_offset);

    SDL_FreeSurface(text_surface);

    return button_surface;
}

void engine::Button::build_texture()
{
    auto surface = build_surface();

    SDL_DestroyTexture(texture);

    texture = SDL_CreateTextureFromSurface(Engine::instance()->get_renderer(), surface);

    SDL_FreeSurface(surface);
}

void engine::Button::click()
{
    callback();
}

int engine::Button::get_padding()
{
    return text_padding;
}

void engine::Button::set_padding(int padding)
{
    text_padding = padding;
    build_texture();
}

int engine::Button::get_border_width()
{
    return border_size;
}

void engine::Button::set_border_width(int size)
{
    border_size = size;
    build_texture();
}

SDL_Color engine::Button::get_background_color()
{
    return background;
}

void engine::Button::set_background_color(SDL_Color color)
{
    background = color;
    build_texture();
}

SDL_Rect engine::Button::get_rect()
{
    return get_tile_based() ? button_rect : transform_on_anchor(button_rect);
}
