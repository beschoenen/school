#include "BorderTile.h"

level::BorderTile::BorderTile(int row_index, int column_index) : Tile(row_index, column_index)
{
    accessible_from_north_ = false;
    accessible_from_east_ = false;
    accessible_from_south_ = false;
    accessible_from_west_ = false;
}
