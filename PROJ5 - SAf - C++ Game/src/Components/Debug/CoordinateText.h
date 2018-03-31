#ifndef GAME_COORDINATETEXT_H
#define GAME_COORDINATETEXT_H


#include "../../Engine/Components/Text.h"

class CoordinateText : public engine::Text
{
    int value_x = 0;
    int value_y = 0;

    void update_text();

public:
    explicit CoordinateText(int val_x, int val_y);

    void move_up();

    void move_down();

    void move_left();

    void move_right();
};


#endif //GAME_COORDINATETEXT_H
