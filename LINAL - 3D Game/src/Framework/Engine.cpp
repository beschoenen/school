#include <iostream>
#include "Engine.h"

void Engine::add_controller(Controller* controller)
{
    controllers.push_back(controller);
}

void Engine::remove_controller(Controller* controller)
{
    auto it = std::find(controllers.begin(), controllers.end(), controller);
    if (it != controllers.end())
    {
        controllers.erase(it);
    }
}

void Engine::start()
{
    SDL_Event event{};
    auto quit = false;

    auto window = Window();

    window.show_window();

    while (!quit)
    {
        while (SDL_PollEvent(&event))
        {
            if (event.type == SDL_QUIT)
            {
                quit = true;
            }

            if (event.type == SDL_KEYUP)
            {
                pressed_keys[event.key.keysym.sym] = false;
            }
            else if (event.type == SDL_KEYDOWN)
            {
                pressed_keys[event.key.keysym.sym] = true;
            }

            for (auto& controller : controllers)
            {
                controller->handle_input(event);
            }
        }

        window.clear();

        for (auto controller : controllers)
        {
            controller->update(1.f / 60.f, pressed_keys);
            controller->render(window);
        }

        SDL_RenderPresent(window.renderer);
    }

    window.close_window();
}
