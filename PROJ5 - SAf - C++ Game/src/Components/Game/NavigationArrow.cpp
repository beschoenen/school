#include "NavigationArrow.h"

NavigationArrow::NavigationArrow(int map_x, int map_y) : Image(ARROW_IMAGE, -25, -25, 50, 50)
{
    anchor = POS_BOTTOM_RIGHT;
    layer = HUD_LAYER;

    set_angle(map_x, map_y);
}

void NavigationArrow::set_angle(int map_x, int map_y)
{
    double arc = (double) map_x / map_y;
    double res = atan(arc) * 180 / M_PI;

    if (map_y >= 0)
    {
        res += 180;
    }

    angle = -((res - 45) - 90);
}
