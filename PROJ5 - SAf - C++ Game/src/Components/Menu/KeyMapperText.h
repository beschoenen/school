#ifndef GAME_BUTTONMAPPERTEXT_H
#define GAME_BUTTONMAPPERTEXT_H

#include "../../Engine/Components/Text.h"

class KeyMapperText : public engine::Text
{
    bool pressed = false;
public:
    KeyMapperText(int x, int y);

    bool get_pressed();

    void set_pressed(bool press);
};

#endif //GAME_BUTTONMAPPERTEXT_H
