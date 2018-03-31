#include "GridTile.h"

level::GridTile::GridTile(GridTileTypes grid_tile_type) : grid_tile_type_(grid_tile_type)
{
}

int level::GridTile::get_row() const
{
    return row_index_;
}

void level::GridTile::set_row(int row)
{
    row_index_ = row;
}

int level::GridTile::get_column() const
{
    return column_index_;
}

void level::GridTile::set_column(int column)
{
    column_index_ = column;
}

GridTileTypes level::GridTile::get_grid_tile_type() const
{
    return grid_tile_type_;
}

void level::GridTile::set_grid_tile_type(GridTileTypes grid_tile_type)
{
    grid_tile_type_ = grid_tile_type;
}

bool level::GridTile::get_accessible() const
{
    return grid_tile_type_ != wall && grid_tile_type_ != campfire;
}

std::weak_ptr<level::GridTile> level::GridTile::get_north() const
{
    return north_;
}

std::weak_ptr<level::GridTile> level::GridTile::get_east() const
{
    return east_;
}

std::weak_ptr<level::GridTile> level::GridTile::get_south() const
{
    return south_;
}

std::weak_ptr<level::GridTile> level::GridTile::get_west() const
{
    return west_;
}

void level::GridTile::set_north(std::shared_ptr<level::GridTile> north)
{
    north_ = std::move(north);
}

void level::GridTile::set_east(std::shared_ptr<level::GridTile> east)
{
    east_ = std::move(east);
}

void level::GridTile::set_south(std::shared_ptr<level::GridTile> south)
{
    south_ = std::move(south);
}

void level::GridTile::set_west(std::shared_ptr<level::GridTile> west)
{
    west_ = std::move(west);
}
