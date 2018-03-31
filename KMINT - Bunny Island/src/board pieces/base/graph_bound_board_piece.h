#ifndef _KMINT_GRAPH_BOUND_ACTOR_H
#define _KMINT_GRAPH_BOUND_ACTOR_H

#include "../../graph.h"
#include "../../drawables/rect_drawable.h"
#include "../rabbit.h"

namespace kmint
{
    class graph_bound_board_piece : public board_piece
    {
        const graph* _graph;
        node_id _node_id;

    protected:
        rect_drawable _drawable;

    public:
        graph_bound_board_piece(kmint::graph* graph, node_id nid, int size, color color) :
            _graph{graph}, _node_id{nid}, _drawable{*this, size, color} {}

        point location() const override { return get_graph()[_node_id].location(); }

        virtual const drawable& get_drawable() const override { return _drawable; }

        const graph& get_graph() const { return *_graph; }

        node_id get_node_id() const { return _node_id; }

        void move(node_id nid) { _node_id = nid; }

        rabbit* closest_rabbit_in_vicinity(int radius);
    };
}

#endif
