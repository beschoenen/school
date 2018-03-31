#ifndef _KMINT_RENDERER_H
#define _KMINT_RENDERER_H

#include "models/point.h"
#include "models/color.h"
#include <unordered_map>
#include "internal/texture.h"
#include <string>
#include <SDL_ttf.h>

struct SDL_Renderer;
struct SDL_Texture;

namespace kmint
{
    class window;
    class image;

    class renderer
    {
        TTF_Font* _font;
        SDL_Renderer* _ren;

        mutable std::unordered_map<std::string, SDL_Texture*> _texture_cache;

    public:
        renderer(const window& w) noexcept;

        ~renderer();

        renderer(const renderer&) = delete;

        renderer& operator=(const renderer&) = delete;

        renderer(renderer&&) noexcept;

        renderer& operator=(renderer&&) noexcept;

        void clear() const;

        void set_color(color c) const;

        void draw_line(point from, point to) const;

        void draw_rect(point top_left, point bottom_right) const;

        void draw_text(point center, std::string message, color color) const;

        void render() const;
    };
}

#endif
