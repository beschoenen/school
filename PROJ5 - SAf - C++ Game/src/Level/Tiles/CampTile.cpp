#include "CampTile.h"

level::CampTile::CampTile(int row_index, int column_index) : FloorTile(row_index, column_index)
{
    visited_ = true;

    accessible_from_north_ = true;
    accessible_from_east_ = true;
    accessible_from_south_ = true;
    accessible_from_west_ = true;

    if (row_index % 2 == 1)
    {
        accessible_from_north_ = false;
    }
    else
    {
        accessible_from_south_ = false;
    }

    if (column_index % 2 == 1)
    {
        accessible_from_west_ = false;
    }
    else
    {
        accessible_from_east_ = false;
    }
}
