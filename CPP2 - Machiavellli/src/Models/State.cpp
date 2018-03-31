#include "State.h"
#include "Game.h"
#include <utility>

State::State() noexcept : State(nullptr)
{
}

State::State(const std::shared_ptr<State> old_state) noexcept : old_state(std::move(old_state))
{
}

std::shared_ptr<State> State::get_old_state() const noexcept
{
    return old_state;
}

void State::set_old_state(const std::shared_ptr<State> state) noexcept
{
    old_state = std::move(state);
}
