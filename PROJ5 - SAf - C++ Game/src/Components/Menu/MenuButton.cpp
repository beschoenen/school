#include <utility>
#include "MenuButton.h"

MenuButton::MenuButton(const std::string& text, std::function<void()> callback, int x, int y, Position anchor) :
    Button(text, std::move(callback), x, y, {255, 255, 255, 255}, {0, 0, 0, 0}, 30, 5, 1)
{
    this->anchor = anchor;
}
