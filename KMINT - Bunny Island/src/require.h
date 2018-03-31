#ifndef _KMINT_REQUIRE_H
#define _KMINT_REQUIRE_H

#include <SDL.h>
#include <stdexcept>

namespace kmint
{
    inline void require_sdl(void* ptr)
    {
        if (!ptr)
        {
            throw std::runtime_error {SDL_GetError()};
        }
    }
}

#endif
