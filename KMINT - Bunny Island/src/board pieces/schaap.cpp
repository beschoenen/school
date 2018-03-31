#include "schaap.h"
#include "../states/schaap/schaap_wandering_state.h"
#include "../states/schaap/schaap_global_state.h"
#include "../entities.h"
#include <algorithm>

#define MAX_THIRST 10
#define DRINKS_BEFORE_SLEEP 1

namespace kmint
{
    schaap::schaap(graph* graph) :
        graph_bound_board_piece{graph, kmint::entities::instance()->cave->get_node_id(), 10, {0, 0, 0}},
        _path_drawable{*this, graph}, _combined_drawable{*this, _path_drawable, _drawable}
    {
        _state_machine = new kmint::state_machine<schaap>(this);
        _state_machine->set_current_state(schaap_wandering_state::instance());
        _state_machine->set_global_state(schaap_global_state::instance());
    }

    schaap::~schaap()
    {
        delete _state_machine;
    }

    void schaap::update(float delta_time)
    {
        _state_machine->update();
    }

    state_machine<schaap>* schaap::get_state_machine()
    {
        return _state_machine;
    }

    void schaap::increment_thirst()
    {
        _thirst = std::min(_thirst + 1, MAX_THIRST);
    }

    bool schaap::is_thirsty()
    {
        return _thirst == MAX_THIRST;
    }

    void schaap::drink(int clench)
    {
        _thirst = std::max(0, _thirst - clench);
        _no_drinks++;
    }

    bool schaap::is_sleepy()
    {
        return _no_drinks == DRINKS_BEFORE_SLEEP;
    }

    void schaap::slept()
    {
        _no_drinks = 0;
    }

    void schaap::set_path(std::vector<node_id> path)
    {
        _path_drawable.set_path(std::move(path));
    }
}
