#ifndef KMINT_PERSON_H
#define KMINT_PERSON_H


#include "graph_bound_board_piece.h"

namespace kmint
{
    class person : public graph_bound_board_piece
    {
    protected:
        int water = 0;

    public:
        person(kmint::graph* graph, kmint::node_id nid, const kmint::color& color);

        virtual int give_water() = 0;

        int water_given();
    };
}


#endif //KMINT_PERSON_H
