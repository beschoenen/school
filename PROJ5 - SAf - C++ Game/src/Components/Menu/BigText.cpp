#include "BigText.h"

BigText::BigText(const std::string& value, int x, int y, Position anchor) :
    Text(value, FONT, x, y, {255, 255, 255, 255}, 63)
{
    this->anchor = anchor;
}
