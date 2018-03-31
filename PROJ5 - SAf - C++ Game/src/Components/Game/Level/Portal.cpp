#include "Portal.h"

Portal::Portal(int x, int y) : Image(PORTAL_IMAGE, x, y, TILE_SIZE * 2, TILE_SIZE * 2)
{
    tile_based = true;
    layer = MAP_LAYER;
}
