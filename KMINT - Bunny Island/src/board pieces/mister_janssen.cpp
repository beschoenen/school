#include "mister_janssen.h"
#include "../states/mister janssen/mister_janssen_wandering_state.h"
#include "../random.h"

namespace kmint
{
    mister_janssen::mister_janssen(graph* graph) : person{graph, 5, color{255, 0, 0}}
    {
        _state_machine = new kmint::state_machine<mister_janssen>(this);
        _state_machine->set_current_state(mister_janssen_wandering_state::instance());
    }

    mister_janssen::~mister_janssen()
    {
        delete _state_machine;
    }

    void mister_janssen::update(float delta_time)
    {
        _state_machine->update();
    }

    state_machine<mister_janssen>* mister_janssen::get_state_machine()
    {
        return _state_machine;
    }

    int mister_janssen::give_water()
    {
        int amount = random::instance().get(30, 50);
        water += amount;

        return amount;
    }
}
