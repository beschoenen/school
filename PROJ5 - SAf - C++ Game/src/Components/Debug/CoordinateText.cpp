#include "CoordinateText.h"

CoordinateText::CoordinateText(int val_x, int val_y) :
    Text("0,0", FONT, 5, 0, {255, 255, 255, 0}, 24),
    value_x(val_x), value_y(val_y)
{
    anchor = POS_BOTTOM_LEFT;
    layer = HUD_LAYER;
    update_text();
}

void CoordinateText::move_up()
{
    value_y -= MOVE_DISTANCE;

    update_text();
}

void CoordinateText::move_down()
{
    value_y += MOVE_DISTANCE;

    update_text();
}

void CoordinateText::move_left()
{
    value_x -= MOVE_DISTANCE;

    update_text();
}

void CoordinateText::move_right()
{
    value_x += MOVE_DISTANCE;

    update_text();
}

void CoordinateText::update_text()
{
    text = std::to_string(value_x) + "," + std::to_string(value_y);
    build_texture();
}
