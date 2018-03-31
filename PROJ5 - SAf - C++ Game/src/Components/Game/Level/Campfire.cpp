#include "Campfire.h"

Campfire::Campfire(int x, int y) : Image(CAMPFIRE_IMAGE, x, y, TILE_SIZE * 2, TILE_SIZE * 2)
{
    tile_based = true;
    y_sorted = true;
}
