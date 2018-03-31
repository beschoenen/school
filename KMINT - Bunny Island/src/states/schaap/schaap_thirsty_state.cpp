#include "schaap_thirsty_state.h"
#include "schaap_wandering_state.h"
#include "../../random.h"
#include "../../astar.h"
#include "../../entities.h"
#include "../../defines.h"

#define DRINK_DURATION 60

namespace kmint
{
    schaap_thirsty_state* kmint::schaap_thirsty_state::instance()
    {
        static schaap_thirsty_state instance;

        return &instance;
    }

    void schaap_thirsty_state::enter(kmint::schaap* schaap)
    {
        logger::log("Schaap is thirsty");

        auto entities = kmint::entities::instance();

        auto astar = kmint::astar(schaap->get_graph());
        float rand = random::instance().getf(1);
        float pref = 0.5;

        if (entities->misses_janssen->water_given() > 0 && entities->mister_janssen->water_given() > 0)
        {
            pref = (entities->misses_janssen->water_given() / entities->mister_janssen->water_given()) / 2;
        }

        if (rand >= pref)
        {
            logger::log("Schaap choose for mister janssen");
            target = entities->mister_janssen;
        }
        else
        {
            logger::log("Schaap choose for misses janssen");
            target = entities->misses_janssen;
        }

        nodes = astar.calculate_path(schaap->get_node_id(), target->get_node_id());
        schaap->set_path(nodes);
    }

    void schaap_thirsty_state::leave(kmint::schaap* schaap)
    {
        ticker = 0;
        target = nullptr;
        nodes.clear();
        schaap->set_path(std::vector<node_id>());
    }

    void schaap_thirsty_state::update(kmint::schaap* schaap)
    {
        if (target == nullptr) return;

        if (!nodes.empty())
        {
            ticker++;
            if (ticker % SCHAAP_SPEED != 0) return;

            ticker = 0;

            schaap->move(nodes[0]);

            if (target->get_node_id() != nodes.back())
            {
                auto astar = kmint::astar(schaap->get_graph());
                nodes = astar.calculate_path(nodes[0], target->get_node_id());

                if (nodes[0] == nodes.back())
                {
                    nodes.clear();
                    return;
                }
            }

            nodes.erase(nodes.begin());
            schaap->set_path(nodes);
        }
        else
        {
            if (ticker == 0) logger::log("Schaap reached target");

            ticker++;
            if (ticker <= DRINK_DURATION) return;

            int water = target->give_water();

            logger::log("Schaap' thirst -" + std::to_string(water));

            schaap->drink(water);
            schaap->get_state_machine()->go_to_state(schaap_wandering_state::instance());
        }
    }
}
