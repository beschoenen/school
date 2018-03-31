#ifndef GAME_ITEM_H
#define GAME_ITEM_H

#include "../Engine/Components/Image.h"

class Item : public engine::Image
{
public:
    Item(std::string image, int x, int y, int width, int height);
};

#endif //GAME_ITEM_H
