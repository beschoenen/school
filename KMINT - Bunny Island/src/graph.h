#ifndef _KMINT_GRAPH_H
#define _KMINT_GRAPH_H

#include "models/node.h"
#include "models/point.h"
#include "board pieces/base/free_roaming_board_piece.h"
#include "drawables/graph_drawable.h"
#include "board pieces/base/static_board_piece.h"
#include <unordered_map>
#include <vector>
#include <istream>
#include <map>

namespace kmint
{
    using node_id = std::vector<node>::size_type;

    class graph : public static_board_piece
    {
        std::vector<node> _nodes;
        std::map<std::pair<float, float>, node> _matrix;
        graph_drawable _drawable;

    public:
        graph(point location);

        void fill(std::istream& is);

        const drawable& get_drawable() const override { return _drawable; }

        decltype(auto) begin() { return _nodes.begin(); }

        decltype(auto) begin() const { return _nodes.begin(); }

        decltype(auto) end() { return _nodes.end(); }

        decltype(auto) end() const { return _nodes.end(); }

        decltype(auto) num_nodes() const { return _nodes.size(); }

        decltype(auto) operator[](node_id id) const { return _nodes[id]; }

        int get_node(point point) const;
    };
}

#endif
