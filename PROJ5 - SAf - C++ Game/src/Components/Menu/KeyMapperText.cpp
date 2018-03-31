#include "KeyMapperText.h"

KeyMapperText::KeyMapperText(int x, int y) : Text(" ", FONT, x, y, {255, 255, 255, 255}, 30)
{
}

bool KeyMapperText::get_pressed()
{
    return pressed;
}

void KeyMapperText::set_pressed(bool press)
{
    if (press == pressed) return;

    pressed = press;
    if (press)
    {
        set_text("Pressed");
    }
    else
    {
        set_text(" ");
    }
}
