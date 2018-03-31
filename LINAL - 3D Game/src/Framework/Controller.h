#ifndef LINAL_CONTROLLER_H
#define LINAL_CONTROLLER_H


#include <SDL_events.h>
#include "Window.h"

class Controller
{
public:
    virtual void handle_input(SDL_Event& event) = 0;

    virtual void update(float delta_time, std::map<SDL_Keycode, bool>& keys) = 0;

    virtual void render(Window& window) = 0;
};


#endif //LINAL_CONTROLLER_H
