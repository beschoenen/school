#ifndef _KMINT_ACTOR_H
#define _KMINT_ACTOR_H


#include "../../models/point.h"
#include "../../states/base/state.h"
#include "../../logger.h"
#include "../../states/base/state_machine.h"

namespace kmint
{
    class drawable;

    class board_piece
    {
    public:
        explicit board_piece() = default;

        virtual ~board_piece() = default;

        // remove copy and move operators and constructors
        board_piece(const board_piece&) = default;

        board_piece(board_piece&&) = delete;

        board_piece& operator=(const board_piece&) = default;

        board_piece& operator=(board_piece&&) = delete;

        virtual void update(float delta_time) {}

        virtual const drawable& get_drawable() const = 0;

        virtual point location() const = 0;
    };
}

#endif
