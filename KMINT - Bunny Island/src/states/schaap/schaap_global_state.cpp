#include "schaap_global_state.h"
#include "../../board pieces/mister_janssen.h"
#include "../../board pieces/misses_janssen.h"
#include "schaap_thirsty_state.h"
#include "schaap_sleepy_state.h"

namespace kmint
{
    schaap_global_state* schaap_global_state::instance()
    {
        static schaap_global_state instance;

        return &instance;
    }

    void schaap_global_state::enter(kmint::schaap* schaap)
    {
        //
    }

    void schaap_global_state::leave(kmint::schaap* schaap)
    {
        //
    }

    void schaap_global_state::update(kmint::schaap* schaap)
    {
        ticker++;
        if (ticker % 60 != 0) return;

        ticker = 0;

        if (schaap->is_sleepy())
        {
            schaap->get_state_machine()->go_to_state(schaap_sleepy_state::instance());
            return;
        }

        schaap->increment_thirst();

        if (schaap->is_thirsty())
        {
            schaap->get_state_machine()->go_to_state(schaap_thirsty_state::instance());
            return;
        }
    }
}
