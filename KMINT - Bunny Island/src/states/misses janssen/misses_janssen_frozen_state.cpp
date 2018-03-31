#include "misses_janssen_frozen_state.h"
#include "misses_janssen_wandering_state.h"

namespace kmint
{
    misses_janssen_frozen_state* misses_janssen_frozen_state::instance()
    {
        static misses_janssen_frozen_state instance;

        return &instance;
    }

    void misses_janssen_frozen_state::enter(kmint::misses_janssen* misses_janssen)
    {
        logger::log("Misses Janssen spotted some rabbits; she froze");
    }

    void misses_janssen_frozen_state::leave(kmint::misses_janssen* misses_janssen)
    {
    }

    void misses_janssen_frozen_state::update(kmint::misses_janssen* misses_janssen)
    {
        if (misses_janssen->closest_rabbit_in_vicinity(40) == nullptr)
        {
            misses_janssen->get_state_machine()->go_to_state(misses_janssen_wandering_state::instance());
        }
    }
}
