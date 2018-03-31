#ifndef LINAL_WINDOW_H
#define LINAL_WINDOW_H

#include <SDL.h>

#undef main

class Window
{
public:
    SDL_Window* screen;
    SDL_Renderer* renderer;

    Window();

    ~Window();

    void show_window();

    void close_window();

    void clear();
};

#endif //LINAL_WINDOW_H
