#include "WindowManager.h"
#include <iostream>

#define SDL_Flags (SDL_INIT_VIDEO)

engine::WindowManager::WindowManager()
{
    if (SDL_InitSubSystem(SDL_Flags) != 0)
    {
        printf("SDL_Init: %s\n", SDL_GetError());
        exit(1);
    }

    bgcolor = {0, 0, 0, 0};
    size = ScreenSize {SCREEN_WIDTH, SCREEN_HEIGHT};

    screen = SDL_CreateWindow(
        GAME_TITLE,
        SDL_WINDOWPOS_CENTERED,
        SDL_WINDOWPOS_CENTERED,
        size.w,
        size.h,
        SDL_WINDOW_RESIZABLE
    );

    SDL_SetWindowBordered(screen, SDL_TRUE);

    renderer = SDL_CreateRenderer(screen, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);

    show_window();
}

engine::WindowManager::~WindowManager()
{
    close_window();

    SDL_DestroyWindow(screen);
    SDL_DestroyRenderer(renderer);

    SDL_QuitSubSystem(SDL_Flags);
}

void engine::WindowManager::show_window()
{
    SDL_ShowWindow(screen);
}

void engine::WindowManager::close_window()
{
    SDL_HideWindow(screen);
}

void engine::WindowManager::update_size(SDL_WindowEvent event)
{
    size.w = event.data1;
    size.h = event.data2;
}

ScreenSize engine::WindowManager::get_size()
{
    return size;
}

SDL_Renderer* engine::WindowManager::get_renderer()
{
    return renderer;
}
