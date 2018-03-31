#ifndef KMINT_STATIC_BOARD_PIECE_H
#define KMINT_STATIC_BOARD_PIECE_H


#include "board_piece.h"
#include "../../drawables/rect_drawable.h"

namespace kmint
{
    class static_board_piece : public board_piece
    {
        point _location;
        rect_drawable _drawable;

    public:
        static_board_piece(point location, int size, color color) :
            _location{location}, _drawable{*this, size, color} {}

        point location() const override { return _location; }

        const drawable& get_drawable() const override { return _drawable; }
    };
}


#endif //KMINT_STATIC_BOARD_PIECE_H
