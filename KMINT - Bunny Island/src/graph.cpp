#include "graph.h"
#include <sstream>
#include <iostream>

namespace kmint
{
    graph::graph(point location) : static_board_piece{location, 0, {0, 0, 0}}, _nodes{}, _drawable{*this}
    {
    }

    void graph::fill(std::istream& is)
    {
        bool reading_nodes = true;
        for (std::string s; std::getline(is, s);)
        {
            if (s.empty())
            {
                reading_nodes = false;
                continue;
            }

            std::istringstream iss{s};
            if (reading_nodes)
            {
                point p;
                if (iss >> p)
                {
                    node n = {p, (int) num_nodes()};
                    _nodes.push_back(n);
                    _matrix.insert(std::make_pair(std::make_pair(p.x(), p.y()), n));
                }
                else
                {
                    throw std::runtime_error {"Error while reading node: " + s};
                }
            }
            else
            {
                int from;
                int to;
                float weight;

                if (iss >> from && iss >> to && iss >> weight)
                {
                    auto& from_node = _nodes.at(from);
                    from_node.add_edge({from_node, _nodes.at(to), weight});
                }
                else
                {
                    throw std::runtime_error {"Error while reading edge: " + s};
                }
            }
        }
    }

    int graph::get_node(point point) const
    {
        auto p = point.to_node();
        try
        {
            auto item = _matrix.find(std::make_pair(p.x(), p.y()));

            if (item != _matrix.end())
            {
                return item->second.id();
            }
        }
        catch(...)
        {
            // Not found
        }

        return -1;
    }
}
