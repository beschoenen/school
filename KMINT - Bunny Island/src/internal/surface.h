#ifndef _KMINT_INTERNAL_SURFACE_H
#define _KMINT_INTERNAL_SURFACE_H

#include <string>

struct SDL_Surface;

namespace kmint
{
    namespace internal
    {
        class surface
        {
            SDL_Surface* _surface;

        public:
            surface(const std::string& filename);

            ~surface();

            surface(const surface&) = delete;;

            surface& operator=(const surface&) = delete;

            surface(surface&&) noexcept ;

            surface& operator=(surface&&) noexcept ;

            SDL_Surface* unwrap() const { return _surface; }
        };
    }
}
#endif
