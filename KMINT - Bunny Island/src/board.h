#ifndef _KMINT_BOARD_H
#define _KMINT_BOARD_H

#include "window.h"
#include "renderer.h"
#include <vector>
#include <SDL_ttf.h>

namespace kmint
{
    class board_piece;

    class board
    {
        window _window;
        renderer _renderer;
        std::vector<board_piece*> _board_pieces;

    public:
        board();

        ~board() = default;

        void play();

        void add_board_piece(board_piece* piece);

        void remove_board_piece(board_piece* piece);
    };
}

#endif
