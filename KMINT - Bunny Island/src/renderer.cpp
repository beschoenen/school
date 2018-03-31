#include "renderer.h"
#include "window.h"
#include "require.h"

namespace kmint
{
    renderer::renderer(const window& w) noexcept :
        _ren{SDL_CreateRenderer(w._win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC)}, _texture_cache{}
    {
        require_sdl(_ren);
        _font = TTF_OpenFont("FiraCode-Medium.ttf", 14);
    }

    renderer::~renderer()
    {
        _texture_cache.clear();
        if (_ren) SDL_DestroyRenderer(_ren);
    }

    renderer::renderer(renderer&& other) noexcept : _ren{other._ren}
    {
        other._ren = nullptr;
    }

    renderer& renderer::operator=(renderer&& other) noexcept
    {
        if (_ren) SDL_DestroyRenderer(_ren);
        _ren = other._ren;
        other._ren = nullptr;
        return *this;
    }

    void renderer::clear() const
    {
        SDL_SetRenderDrawColor(_ren, 0, 5, 193, SDL_ALPHA_OPAQUE);
        SDL_RenderClear(_ren);
    }

    void renderer::set_color(color c) const
    {
        SDL_SetRenderDrawColor(_ren, c.r(), c.g(), c.b(), c.a());
    }

    void renderer::draw_line(point from, point to) const
    {
        SDL_RenderDrawLine(_ren, from.x(), from.y(), to.x(), to.y());
    }

    void renderer::draw_rect(point top_left, point bottom_right) const
    {
        SDL_Rect r;
        r.x = top_left.x();
        r.y = top_left.y();
        r.w = bottom_right.x() - top_left.x();
        r.h = bottom_right.y() - top_left.y();

        SDL_RenderFillRect(_ren, &r);
    }

    void renderer::render() const
    {
        SDL_RenderPresent(_ren);
    }

    void renderer::draw_text(point center, std::string message, color color) const
    {
        auto texture_it = _texture_cache.find(message);
        if (texture_it == _texture_cache.end())
        {
            auto text_surface = TTF_RenderText_Solid(_font, message.c_str(), {color.r(), color.g(), color.b()});
            if (text_surface == nullptr)
            {
                std::cerr << "Could not create surface" << std::endl;
                exit(1);
            }

            SDL_Texture* texture = SDL_CreateTextureFromSurface(_ren, text_surface);

            texture_it = _texture_cache.emplace(message, texture).first;
        }

        int width, height;

        SDL_QueryTexture(texture_it->second, nullptr, nullptr, &width, &height);

        SDL_Rect r;
        r.x = center.x();
        r.y = center.y();
        r.w = width;
        r.h = height;


        SDL_RenderCopy(_ren, texture_it->second, nullptr, &r);
    }
}
