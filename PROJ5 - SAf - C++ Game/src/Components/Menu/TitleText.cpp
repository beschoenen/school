#include "TitleText.h"

TitleText::TitleText(const std::string& value, int x, int y, Position anchor) :
    Text(value, FONT, x, y, {255, 255, 255, 255}, 52)
{
    this->anchor = anchor;
}
