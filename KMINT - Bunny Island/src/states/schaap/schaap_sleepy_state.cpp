#include "schaap_sleepy_state.h"
#include "schaap_wandering_state.h"
#include "../../entities.h"
#include "../../defines.h"
#include "../../astar.h"

#define SLEEP_DURATION 300

namespace kmint
{
    schaap_sleepy_state* kmint::schaap_sleepy_state::instance()
    {
        static schaap_sleepy_state instance;

        return &instance;
    }

    void schaap_sleepy_state::enter(kmint::schaap* schaap)
    {
        logger::log("Schaap became sleepy and started walking to the cave");
        auto cave = kmint::entities::instance()->cave;
        auto astar = kmint::astar(schaap->get_graph());

        nodes = astar.calculate_path(schaap->get_node_id(), cave->get_node_id());
    }

    void schaap_sleepy_state::leave(kmint::schaap* schaap)
    {
        ticker = 0;
        nodes.clear();
        schaap->set_path(std::vector<node_id>());
    }

    void schaap_sleepy_state::update(kmint::schaap* schaap)
    {
        if (nodes.empty())
        {
            if (ticker == 0) logger::log("Schaap reached the cave and started sleeping");

            if (ticker == SLEEP_DURATION / 2)
            {                
                entities::instance()->rabbit_population->reproduce_offspring();
            }

            ticker++;
            if (ticker <= SLEEP_DURATION) return;

            schaap->slept();
            schaap->get_state_machine()->go_to_state(schaap_wandering_state::instance());

            return;
        }

        ticker++;
        if (ticker % SCHAAP_SPEED != 0) return;

        ticker = 0;

        schaap->move(nodes[0]);
        nodes.erase(nodes.begin());
        schaap->set_path(nodes);
    }
}
