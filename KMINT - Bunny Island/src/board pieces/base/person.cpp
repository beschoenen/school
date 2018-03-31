#include "person.h"
#include <utility>

namespace kmint
{
    person::person(kmint::graph* graph, kmint::node_id nid, const kmint::color& color) :
        graph_bound_board_piece{graph, nid, 20, color}
    {
        //
    }

    int person::water_given()
    {
        return water;
    }
}
