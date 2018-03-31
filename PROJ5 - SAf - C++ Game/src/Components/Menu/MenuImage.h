#ifndef GAME_MENUIMAGE_H
#define GAME_MENUIMAGE_H

#include "../../Engine/Components/Image.h"

class MenuImage : public engine::Image
{
public:
    MenuImage(std::string image, int x, int y, int width, int height, Position anchor);
};


#endif //GAME_MENUIMAGE_H
