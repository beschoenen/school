#include "astar.h"
#include <utility>

#define DIRECTIONS 4

namespace kmint
{
    astar::astar(const kmint::graph& graph) : _graph{graph}
    {
    }

    std::vector<node_id> astar::calculate_path(node_id start_node, node_id end_node)
    {
        astar_node* current = nullptr;
        astar_node* endpoint = new astar_node{ _graph[end_node] };
        astar_node* startpoint = new astar_node{ _graph[start_node] };
        std::set<astar_node*> open_list, closed_list;
        open_list.insert(startpoint);

        while (!open_list.empty())
        {
            current = *open_list.begin();
            for (auto node : open_list)
            {
                if (node->get_score() <= current->get_score())
                {
                    current = node;
                }
            }

            if (*current == endpoint) break;

            closed_list.insert(current);
            open_list.erase(open_list.find(current));

            for (auto edge : current->get_node()->get_edges())
            {
                const node& new_node = edge.to();
                if (find_node(closed_list, new_node) != nullptr) continue;

                auto total_cost = current->g() + 10;

                auto successor = find_node(open_list, new_node);
                if (successor == nullptr)
                {
                    successor = new astar_node{new_node};
                    successor->set_parent(current);
                    successor->g(total_cost);
                    successor->h(euclidean(*successor, *endpoint));

                    open_list.insert(successor);
                }
                else if (total_cost < successor->g())
                {
                    successor->set_parent(current);
                    successor->g(total_cost);
                }
            }
        }

        auto nodes = std::vector<node_id>();
        while (current != nullptr)
        {
            nodes.insert(nodes.begin(), current->get_node()->id());
            current = current->get_parent();
        }

        return nodes;
    }

    point astar::get_delta(point source, point target)
    {
        return {std::abs(source.x() - target.x()), std::abs(source.y() - target.y())};
    }

    unsigned int astar::manhattan(astar_node& source, astar_node& target)
    {
        auto delta = get_delta(source.get_node()->location(), target.get_node()->location());

        return static_cast<unsigned int>(10 * delta.x() + delta.y());
    }
    
    unsigned int astar::euclidean(astar_node& source, astar_node& target)
    {
        auto delta = get_delta(source.get_node()->location(), target.get_node()->location());
        auto x = pow(delta.x(), 2);
        auto y = pow(delta.y(), 2);

        return sqrt(x + y);
    }

    astar_node* astar::find_node(std::set<astar_node*> list, node node)
    {
        for (auto item : list)
        {
            if (item->get_node()->id() == node.id())
            {
                return item;
            }
        }

        return nullptr;
    }
}
