#include "FloorTile.h"

level::FloorTile::FloorTile(int row_index, int column_index) : Tile(row_index, column_index)
{
}

bool level::FloorTile::get_visited() const
{
    return this->visited_;
}

void level::FloorTile::set_visited(bool visited)
{
    this->visited_ = visited;
}
