#ifndef SDL_PROJECT_TILE_OBJECT_H
#define SDL_PROJECT_TILE_OBJECT_H

namespace level
{
    class TileCollisionInfo
    {
        int x_;
        int y_;
        int height_;
        int width_;

    public:
        TileCollisionInfo(int x, int y, int height, int width);

        ~TileCollisionInfo() = default;

        int get_x();

        void set_x(int x);

        int get_y();

        void set_y(int y);

        int get_height();

        void set_height(int height);

        int get_width();

        void set_width(int width);
    };
}

#endif //SDL_PROJECT_TILE_OBJECT_H
