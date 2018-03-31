#ifndef NAV_ASTAR_H
#define NAV_ASTAR_H

#include <vector>
#include <stack>
#include <memory>
#include "Node.h"
#include "Edge.h"
#include "../../Components/Debug/NodeTile.h"

namespace engine
{
    class Astar
    {
    protected:
        static int search_range_;

        static std::shared_ptr<Node> start_node_;
        static std::shared_ptr<Node> end_node_;
        static std::shared_ptr<Node> selected_node_;
        static std::shared_ptr<Node> previous_node_;
        static std::vector<std::shared_ptr<Node>> open_list_;
        static std::vector<std::shared_ptr<Edge>> closed_list_;
        static std::vector<std::shared_ptr<NodeTile>> debug_list_;
        static std::stack<Direction> direction_list_;

        static void search_for_endpoint();

        static void backtrack_to_startpoint();

        static bool can_reach_destination();

        static void cleanup();

        static void get_surrounding_nodes();

        static void select_node();

        static std::shared_ptr<Node> create_node(std::shared_ptr<level::GridTile> gridtile);

        static double heuristic(std::shared_ptr<Node>& current_tile);

        static bool add_to_open(std::shared_ptr<Node>& open_node);

        static bool add_to_closed(std::shared_ptr<Node>& closed_node);

        static void debugger();

        static void create_debug();

        static void destroy_debug();

    public:
        static std::tuple<std::stack<Direction>, bool> find_path(std::shared_ptr<level::GridTile>& start, std::shared_ptr<level::GridTile>& end, int search_range = ASTAR_SEARCH_RANGE);
    };
}

#endif //NAV_ASTAR_H
