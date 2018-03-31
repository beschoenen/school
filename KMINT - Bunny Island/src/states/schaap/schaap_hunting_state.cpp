#include <iostream>
#include "schaap_hunting_state.h"
#include "../../board pieces/rabbit.h"
#include "schaap_wandering_state.h"
#include "../../defines.h"

namespace kmint
{
    schaap_hunting_state* schaap_hunting_state::instance()
    {
        static schaap_hunting_state instance;

        return &instance;
    }

    void schaap_hunting_state::enter(kmint::schaap* schaap)
    {
        astar = new kmint::astar(schaap->get_graph());
        logger::log("Schaap started hunting");
    }

    void schaap_hunting_state::leave(kmint::schaap* schaap)
    {
        delete astar;
        astar = nullptr;
        schaap->set_path(std::vector<node_id>());
    }

    void schaap_hunting_state::update(kmint::schaap* schaap)
    {
        auto closest_rabbit = schaap->closest_rabbit_in_vicinity(50);
        if (closest_rabbit == nullptr)
        {
            schaap->get_state_machine()->go_to_state(schaap_wandering_state::instance());
            return;
        }

        if (closest_rabbit->location().distance(schaap->location()) <= 15)
        {
            closest_rabbit->state(eaten);
            std::cout << "Eaten" << std::endl;
            return;
        }

        auto node = schaap->get_graph().get_node(closest_rabbit->location().to_node());
        if (node != last_known_location)
        {
            last_known_location = node;
            nodes = astar->calculate_path(schaap->get_node_id(), static_cast<node_id>(last_known_location));

            if (!nodes.empty() && nodes[0] == schaap->get_node_id())
            {
                nodes.erase(nodes.begin());
            }
        }

        if (!nodes.empty())
        {
            ticker++;
            if (ticker % SCHAAP_SPEED != 0) return;

            ticker = 0;

            schaap->move(nodes[0]);
            nodes.erase(nodes.begin());
            schaap->set_path(nodes);
        }
    }
}
