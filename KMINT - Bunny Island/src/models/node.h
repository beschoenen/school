#ifndef _KMINT_NODE_H
#define _KMINT_NODE_H

#include <vector>
#include "point.h"
#include "edge.h"

namespace kmint
{
    class node
    {
        point _location;
        int _id;
        std::vector<edge> _edges;

    public:
        node(point location, int id) : _location{location}, _id{id}, _edges{} {}

        bool operator==(const node& node) const { return node._id == _id; }

        int id() const noexcept { return _id; }

        point location() const noexcept { return _location; }

        void add_edge(edge e) { _edges.push_back(e); }

        std::vector<edge> get_edges() { return _edges; }

        decltype(auto) begin() const noexcept { return _edges.begin(); }

        decltype(auto) end() const noexcept { return _edges.end(); }
    };
}

#endif
