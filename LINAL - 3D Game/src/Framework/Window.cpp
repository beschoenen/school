#include "Window.h"
#include "../defines.h"

Window::Window()
{
    screen = SDL_CreateWindow(
        "LINAL",
        SDL_WINDOWPOS_UNDEFINED,
        SDL_WINDOWPOS_UNDEFINED,
        SCREEN_WIDTH,
        SCREEN_HEIGHT,
        0
    );

    SDL_SetWindowBordered(screen, SDL_TRUE);

    renderer = SDL_CreateRenderer(screen, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
}

Window::~Window()
{
    SDL_DestroyWindow(screen);
    SDL_DestroyRenderer(renderer);
}

void Window::show_window()
{
    SDL_ShowWindow(screen);
}

void Window::close_window()
{
    SDL_HideWindow(screen);
}

void Window::clear()
{
    SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
    SDL_RenderClear(renderer);
    SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
}
