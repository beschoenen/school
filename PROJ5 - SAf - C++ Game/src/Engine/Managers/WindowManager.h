#ifndef SDL_PROJECT_WINDOW_H
#define SDL_PROJECT_WINDOW_H

#include <SDL.h>
#include "../../defines.h"

#undef main

namespace engine
{
    class WindowManager
    {
    protected:
        ScreenSize size;
        SDL_Color bgcolor;

        SDL_Window* screen;
        SDL_Renderer* renderer;

        WindowManager();

        ~WindowManager();

        void show_window();

        void close_window();

        void update_size(SDL_WindowEvent event);

    public:
        ScreenSize get_size();

        SDL_Renderer* get_renderer();
    };
}

#endif //SDL_PROJECT_WINDOW_H
