#include "KeyMapperButton.h"

KeyMapperButton::KeyMapperButton(const std::string& text, std::function<void(engine::Buttons)> button_callback,
                                 engine::Buttons type, int x, int y) :
    MenuButton(text, std::bind(&KeyMapperButton::button_clicked, this), x, y),
    button_callback(std::move(button_callback)), type(type)
{
}

void KeyMapperButton::button_clicked()
{
    button_callback(type);
}
