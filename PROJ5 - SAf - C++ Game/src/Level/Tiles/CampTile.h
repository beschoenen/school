#ifndef SDL_PROJECT_CAMPTILE_H
#define SDL_PROJECT_CAMPTILE_H

#include "FloorTile.h"

namespace level
{
    class CampTile : public level::FloorTile
    {
    public:
        CampTile(int row_index, int column_index);

        ~CampTile() override = default;
    };
}

#endif //SDL_PROJECT_CAMPTILE_H
