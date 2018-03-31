#ifndef GAME_KEYMAPPERBUTTON_H
#define GAME_KEYMAPPERBUTTON_H

#include "Menu/MenuButton.h"
#include "../Engine/Input/Mapping.h"

class KeyMapperButton : public MenuButton
{
    engine::Buttons type;
    std::function<void(engine::Buttons)> button_callback;

public:
    KeyMapperButton(const std::string& text, std::function<void(engine::Buttons)> button_callback,
                    engine::Buttons button, int x, int y);

    void button_clicked();
};


#endif //GAME_KEYMAPPERBUTTON_H
