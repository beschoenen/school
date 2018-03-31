#ifndef _KMINT_FREE_ROAMING_ACTOR_H
#define _KMINT_FREE_ROAMING_ACTOR_H

#include "board_piece.h"
#include "../../models/point.h"
#include "../../models/color.h"
#include "../../drawables/rect_drawable.h"

namespace kmint
{
    class free_roaming_board_piece : public board_piece
    {
        rect_drawable _drawable;

    protected:
        point _location;

    public:
        free_roaming_board_piece(point location, int size, color color) :
            _location{location}, _drawable{*this, size, color} {}

        point location() const override { return _location; }

        const drawable& get_drawable() const override { return _drawable; }
    };
}

#endif
