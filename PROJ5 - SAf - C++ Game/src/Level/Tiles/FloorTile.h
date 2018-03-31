#ifndef SDL_PROJECT_FLOORTILE_H
#define SDL_PROJECT_FLOORTILE_H

#include "Base/Tile.h"

namespace level
{
    class FloorTile : public level::Tile
    {
    protected:
        bool visited_;
    public:
        FloorTile(int row_index, int column_index);

        bool get_visited() const;

        void set_visited(bool visited);
    };
}

#endif //SDL_PROJECT_FLOORTILE_H
