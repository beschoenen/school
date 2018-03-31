#include "Text.h"
#include "../Engine.h"
#include <iostream>

engine::Text::Text(std::string text, std::string font_name, int x, int y,
                   int width, int height, SDL_Color color, int font_size) :
    Renderable(x, y, width, height),
    text(std::move(text)), color(color), font_name(font_name), font_size(font_size)
{
    font = Engine::instance()->load_font(font_name, font_size);
    build_texture();
}

engine::Text::Text(std::string text, std::string font_name, int x, int y, SDL_Color color, int font_size) :
    Text(std::move(text), std::move(font_name), x, y, -1, -1, color, font_size)
{
}

engine::Text::~Text()
{
    SDL_DestroyTexture(texture);
}

SDL_Surface* engine::Text::build_surface()
{
    auto text_surface = TTF_RenderText_Solid(font, text.c_str(), color);
    if (text_surface == nullptr)
    {
        std::cerr << "Could not create surface" << std::endl;
        exit(1);
    }

    return text_surface;
}

void engine::Text::build_texture()
{
    auto surface = build_surface();

    SDL_DestroyTexture(texture);

    texture = SDL_CreateTextureFromSurface(Engine::instance()->get_renderer(), surface);

    SDL_FreeSurface(surface);
}

std::string engine::Text::get_text()
{
    return text;
}

void engine::Text::set_text(std::string string)
{
    if (text != string)
    {
        text = std::move(string);
        build_texture();
    }
}

void engine::Text::set_font(std::string font_name, int size)
{
    if (this->font_name == font_name && font_size == size) return;

    font = Engine::instance()->load_font(font_name, size);
    build_texture();
}

SDL_Color engine::Text::get_color()
{
    return color;
}

void engine::Text::set_color(SDL_Color c)
{
    color = c;
    build_texture();
}
