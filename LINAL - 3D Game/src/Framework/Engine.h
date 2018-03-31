#ifndef LINAL_ENGINE_H
#define LINAL_ENGINE_H


#include <vector>
#include <map>
#include "Controller.h"

class Engine
{
    std::vector<Controller*> controllers;

    std::map<SDL_Keycode, bool> pressed_keys;

public:
    Engine() = default;

    void add_controller(Controller* controller);

    void remove_controller(Controller* controller);

    void start();
};


#endif //LINAL_ENGINE_H
