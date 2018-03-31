#include "MenuText.h"

MenuText::MenuText(const std::string& value, int x, int y, Position anchor) :
    Text(value, FONT, x, y, {255, 255, 255, 255}, 30)
{
    this->anchor = anchor;
}
