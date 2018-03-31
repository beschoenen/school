#include "texture.h"
#include "surface.h"
#include <SDL_image.h>
#include <stdexcept>
#include "../require.h"

namespace kmint
{
    namespace internal
    {
        texture::texture(const std::string& filename, SDL_Renderer* ren)
        {
            surface surf{filename};
            auto internal_surface = surf.unwrap();

            _width = internal_surface->w;
            _height = internal_surface->h;
            _texture = SDL_CreateTextureFromSurface(ren, internal_surface);

            require_sdl(_texture);
        }

        texture::texture(texture&& other) noexcept :
                _texture{other._texture}, _width{other._width}, _height{other._height}
        {
            other._texture = nullptr;
        }

        texture& texture::operator=(texture&& other) noexcept
        {
            if (_texture)
            {
                SDL_DestroyTexture(_texture);
            }

            _texture = other._texture;
            _width = other._width;
            _height = other._height;
            other._texture = nullptr;

            return *this;
        }

        texture::~texture()
        {
            if (_texture)
            {
                SDL_DestroyTexture(_texture);
            }
        }
    }
}
