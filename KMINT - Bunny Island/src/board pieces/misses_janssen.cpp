#include "misses_janssen.h"
#include "../states/misses janssen/misses_janssen_wandering_state.h"
#include "../random.h"

namespace kmint
{
    misses_janssen::misses_janssen(graph* graph) : person{graph, 1570, color{255, 0, 128}}
    {
        _state_machine = new kmint::state_machine<misses_janssen>(this);
        _state_machine->set_current_state(misses_janssen_wandering_state::instance());
    }

    misses_janssen::~misses_janssen()
    {
        delete _state_machine;
    }

    void misses_janssen::update(float delta_time)
    {
        _state_machine->update();
    }

    kmint::state_machine<misses_janssen>* misses_janssen::get_state_machine()
    {
        return _state_machine;
    }

    int misses_janssen::give_water()
    {
        int amount = random::instance().get(10, 80);
        water += amount;

        return amount;
    }
}
