#ifndef SDL_PROJECT_ROADTILE_H
#define SDL_PROJECT_ROADTILE_H

#include "FloorTile.h"

namespace level
{
    class RoadTile : public level::FloorTile
    {
        bool has_any_remaining_sideways() const;

        bool check_if_path_can_be_created(std::weak_ptr<Tile> room, bool is_visited) const;

        bool create_passage(int side, bool is_visited);

    public:
        RoadTile(int row_index, int column_index);

        void generate_path();

        void create_extra_passage();
    };
}

#endif //SDL_PROJECT_ROADTILE_H
