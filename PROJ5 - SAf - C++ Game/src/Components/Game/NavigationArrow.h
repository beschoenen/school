#ifndef GAME_NAVIGATIONARROW_H
#define GAME_NAVIGATIONARROW_H


#include "../../Engine/Components/Image.h"

class NavigationArrow : public engine::Image
{
public:
    NavigationArrow(int map_x, int map_y);

    void set_angle(int map_x, int map_y);
};


#endif //GAME_NAVIGATIONARROW_H
