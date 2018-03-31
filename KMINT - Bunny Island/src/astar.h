#ifndef KMINT_ASTAR_H
#define KMINT_ASTAR_H


#include <stack>
#include <set>
#include "graph.h"
#include "models/astar_node.h"

namespace kmint
{
    class astar
    {
        std::vector<point> directions {
            {0, 20},
            {20, 0},
            {0, -20},
            {-20, 0}
        };

        const graph& _graph;

        point get_delta(point source, point target);

        astar_node* find_node(std::set<astar_node*> list, node node);

    public:
        explicit astar(const graph& graph);

        std::vector<node_id> calculate_path(node_id start_node, node_id end_node);
        
        unsigned int manhattan(astar_node& source, astar_node& target);

        unsigned int euclidean(astar_node& source, astar_node& target);
    };
}


#endif //KMINT_ASTAR_H
