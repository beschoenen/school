#ifndef GAME_MENUBUTTON_H
#define GAME_MENUBUTTON_H

#include "../../Engine/Components/Button.h"

class MenuButton : public engine::Button
{
public:
    MenuButton(const std::string& text, std::function<void()> callback, int x, int y, Position anchor = POS_TOP_LEFT);
};


#endif //GAME_MENUBUTTON_H
