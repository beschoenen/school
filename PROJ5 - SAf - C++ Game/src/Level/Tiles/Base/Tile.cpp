#include "Tile.h"

level::Tile::Tile(int row_index, int column_index) : row_index_(row_index), column_index_(column_index)
{
}

int level::Tile::get_row() const
{
    return this->row_index_;
}

int level::Tile::get_column() const
{
    return this->column_index_;
}

std::weak_ptr<level::Tile> level::Tile::get_north() const
{
    return this->north_;
}

std::weak_ptr<level::Tile> level::Tile::get_east() const
{
    return this->east_;
}

std::weak_ptr<level::Tile> level::Tile::get_south() const
{
    return this->south_;
}

std::weak_ptr<level::Tile> level::Tile::get_west() const
{
    return this->west_;
}

void level::Tile::set_north(std::shared_ptr<level::Tile> north)
{
    this->north_ = north;
}

void level::Tile::set_east(std::shared_ptr<level::Tile> east)
{
    this->east_ = east;
}

void level::Tile::set_south(std::shared_ptr<level::Tile> south)
{
    this->south_ = south;
}

void level::Tile::set_west(std::shared_ptr<level::Tile> west)
{
    this->west_ = west;
}

bool level::Tile::get_accessible_from_north() const
{
    return this->accessible_from_north_;
}

bool level::Tile::get_accessible_from_east() const
{
    return this->accessible_from_east_;
}

bool level::Tile::get_accessible_from_south() const
{
    return this->accessible_from_south_;
}

bool level::Tile::get_accessible_from_west() const
{
    return this->accessible_from_west_;
}

void level::Tile::set_accessible_from_north(bool is_accessible)
{
    this->accessible_from_north_ = is_accessible;
}

void level::Tile::set_accessible_from_east(bool is_accessible)
{
    this->accessible_from_east_ = is_accessible;
}

void level::Tile::set_accessible_from_south(bool is_accessible)
{
    this->accessible_from_south_ = is_accessible;
}

void level::Tile::set_accessible_from_west(bool is_accessible)
{
    this->accessible_from_west_ = is_accessible;
}
