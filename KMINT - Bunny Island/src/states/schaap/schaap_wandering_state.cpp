#include "schaap_wandering_state.h"
#include "../../random.h"
#include "../../defines.h"
#include "schaap_hunting_state.h"

namespace kmint
{
    schaap_wandering_state* schaap_wandering_state::instance()
    {
        static schaap_wandering_state instance;
        return &instance;
    }

    void schaap_wandering_state::enter(kmint::schaap* schaap)
    {
        logger::log("Schaap started wandering");
    }

    void schaap_wandering_state::leave(kmint::schaap* schaap)
    {
        //
    }

    void schaap_wandering_state::update(kmint::schaap* schaap)
    {
        if (schaap->closest_rabbit_in_vicinity(50) != nullptr)
        {
            schaap->get_state_machine()->go_to_state(schaap_hunting_state::instance());
            return;
        }

        ticker++;
        if (ticker % SCHAAP_SPEED != 0) return;

        ticker = 0;

        auto origin = schaap->get_graph()[schaap->get_node_id()];
        auto edges = origin.get_edges();
        auto edge = edges[random::instance().get(edges.size() - 1)];

        auto destination = edge.from() == origin ? edge.to() : edge.from();

        schaap->move(destination.id());
    }
}
