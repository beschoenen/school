#include "TileCollisionInfo.h"

level::TileCollisionInfo::TileCollisionInfo(int x, int y, int height, int width) :
    x_(x), y_(y), height_(height), width_(width)
{
}

int level::TileCollisionInfo::get_x()
{
    return x_;
}

void level::TileCollisionInfo::set_x(int x)
{
    x_ = x;
}

int level::TileCollisionInfo::get_y()
{
    return y_;
}

void level::TileCollisionInfo::set_y(int y)
{
    y_ = y;
}

int level::TileCollisionInfo::get_height()
{
    return height_;
}

void level::TileCollisionInfo::set_height(int height)
{
    height_ = height;
}

int level::TileCollisionInfo::get_width()
{
    return width_;
}

void level::TileCollisionInfo::set_width(int width)
{
    width_ = width;
}
