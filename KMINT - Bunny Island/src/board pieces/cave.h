#ifndef KMINT_CAVE_H
#define KMINT_CAVE_H


#include "base/graph_bound_board_piece.h"

namespace kmint
{
    class cave : public graph_bound_board_piece
    {
    public:
        explicit cave(kmint::graph* graph);
    };
}


#endif //KMINT_CAVE_H
