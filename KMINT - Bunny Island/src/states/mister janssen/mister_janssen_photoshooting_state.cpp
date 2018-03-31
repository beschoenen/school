#include "mister_janssen_photoshooting_state.h"
#include "mister_janssen_wandering_state.h"

#define PHOTOSHOOT_DURATION 600

namespace kmint
{
    mister_janssen_photoshooting_state* kmint::mister_janssen_photoshooting_state::instance()
    {
        static mister_janssen_photoshooting_state instance;

        return &instance;
    }

    void mister_janssen_photoshooting_state::enter(kmint::mister_janssen* mister_janssen)
    {
        logger::log("Mister Janssen started shooting photos");
    }

    void mister_janssen_photoshooting_state::leave(kmint::mister_janssen* mister_janssen)
    {
        ticker = 0;
    }

    void mister_janssen_photoshooting_state::update(kmint::mister_janssen* mister_janssen)
    {
        ticker++;
        if (ticker > PHOTOSHOOT_DURATION)
        {
            mister_janssen->get_state_machine()->go_to_previous_state();
        }
    }
}
