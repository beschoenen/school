#include "StateMachine.h"
#include "State.h"
#include <utility>

engine::StateMachine::StateMachine() : current_state(nullptr), global_state(nullptr)
{
}

std::shared_ptr<engine::State> engine::StateMachine::get_current_state() const
{
    return current_state;
}

std::shared_ptr<engine::State> engine::StateMachine::get_global_state() const
{
    return global_state;
}

void engine::StateMachine::set_global_state(std::shared_ptr<State> global_state)
{
    if (global_state != nullptr)
    {
        global_state->leave_state();
    }

    StateMachine::global_state = global_state;
    StateMachine::global_state->enter_state();
}

void engine::StateMachine::change_state(std::shared_ptr<State> new_state)
{
    if (current_state != nullptr)
    {
        current_state->leave_state();
    }

    current_state = std::move(new_state);
    current_state->enter_state();
}

void engine::StateMachine::update() const
{
    if (global_state)
    {
        global_state->update();
    }

    if (current_state)
    {
        current_state->update();
    }
}
