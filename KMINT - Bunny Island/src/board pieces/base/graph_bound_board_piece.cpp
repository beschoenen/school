#include "graph_bound_board_piece.h"
#include "../../entities.h"

namespace kmint
{
    rabbit* graph_bound_board_piece::closest_rabbit_in_vicinity(int radius)
    {
        rabbit* closest = nullptr;
        float closest_distance = -1;

        for (auto rabbit : entities::instance()->rabbit_population->get_individuals())
        {
            if (rabbit->state() != alive) continue;

            auto index = get_graph().get_node(rabbit->location().to_node());

            if (index < 0 || index >= get_graph().num_nodes()) continue;

            auto node = get_graph()[index];

            auto distance = location().distance(node.location());

            if ((closest_distance < 0 || distance < closest_distance) && distance <= radius)
            {
                closest_distance = distance;
                closest = rabbit;
            }
        }

        return closest;
    }
}
