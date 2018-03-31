#include "MenuImage.h"
#include "../../Engine/Engine.h"

MenuImage::MenuImage(std::string image, int x, int y, int width, int height, Position anchor) :
    Image(image, x, y, width, height)
{
    this->anchor = anchor;
}
