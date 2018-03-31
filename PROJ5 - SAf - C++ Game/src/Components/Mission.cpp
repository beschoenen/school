#include "Mission.h"

Mission::Mission(std::string image, int x, int y, int width, int height) : Image(std::move(image), x, y, width, height)
{
    tile_based = true;
    layer = MAP_LAYER;
}
