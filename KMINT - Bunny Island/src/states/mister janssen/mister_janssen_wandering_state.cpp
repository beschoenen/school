#include "mister_janssen_wandering_state.h"
#include "../../defines.h"
#include "../../random.h"
#include "mister_janssen_photoshooting_state.h"

#define WALKING_DURATION 1200

namespace kmint
{
    mister_janssen_wandering_state* mister_janssen_wandering_state::instance()
    {
        static mister_janssen_wandering_state instance;

        return &instance;
    }

    void mister_janssen_wandering_state::enter(kmint::mister_janssen* mister_janssen)
    {
        logger::log("Mister Janssen started wandering");
    }

    void mister_janssen_wandering_state::leave(kmint::mister_janssen* mister_janssen)
    {
        ticker = 0;
    }

    void mister_janssen_wandering_state::update(kmint::mister_janssen* mister_janssen)
    {
        ticker++;
        if (ticker % PERSON_SPEED != 0) return;

        auto origin = mister_janssen->get_graph()[mister_janssen->get_node_id()];
        auto edges = origin.get_edges();
        auto edge = edges[random::instance().get(edges.size() - 1)];

        auto destination = edge.from() == origin ? edge.to() : edge.from();

        mister_janssen->move(destination.id());

        if (ticker > WALKING_DURATION)
        {
            mister_janssen->get_state_machine()->go_to_state(mister_janssen_photoshooting_state::instance());
        }
    }
}
