#include "misses_janssen_wandering_state.h"
#include "../../defines.h"
#include "../../random.h"
#include "misses_janssen_frozen_state.h"

namespace kmint
{
    misses_janssen_wandering_state* misses_janssen_wandering_state::instance()
    {
        static misses_janssen_wandering_state instance;

        return &instance;
    }

    void misses_janssen_wandering_state::enter(kmint::misses_janssen* misses_janssen)
    {
        logger::log("Misses Janssen started wandering");
    }

    void misses_janssen_wandering_state::leave(kmint::misses_janssen* misses_janssen)
    {
        ticker = 0;
    }

    void misses_janssen_wandering_state::update(kmint::misses_janssen* misses_janssen)
    {
        ticker++;
        if (ticker % PERSON_SPEED != 0) return;

        ticker = 0;

        auto origin = misses_janssen->get_graph()[misses_janssen->get_node_id()];
        auto edges = origin.get_edges();
        auto edge = edges[random::instance().get(edges.size() - 1)];

        auto destination = edge.from() == origin ? edge.to() : edge.from();

        misses_janssen->move(destination.id());

        if (misses_janssen->closest_rabbit_in_vicinity(40) != nullptr)
        {
            misses_janssen->get_state_machine()->go_to_state(misses_janssen_frozen_state::instance());
        }
    }
}
