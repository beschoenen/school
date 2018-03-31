#ifndef SDL_PROJECT_BORDER_H
#define SDL_PROJECT_BORDER_H

#include "Base/Tile.h"

namespace level
{
    class BorderTile : public level::Tile
    {
    public:
        BorderTile(int row_index, int column_index);

        ~BorderTile() override = default;
    };
}

#endif //SDL_PROJECT_BORDER_H
