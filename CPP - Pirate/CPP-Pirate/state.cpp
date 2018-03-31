#include "stdafx.h"
#include "state.h"

state::state(game* game, state* old_state) : message_(new str()), game_(game), old_state_(old_state)
{
}

state::~state()
{
    delete message_;
}

str* state::render()
{
    return nullptr;
}

void state::handle_input(const str* input)
{
}

void state::cleanup() const
{
    if (old_state_)
    {
        old_state_->cleanup();
    }

    delete old_state_;
}

str* state::get_message() const
{
    return message_;
}

void state::set_message(str* message)
{
    delete_message();
    message_ = message;
}

void state::delete_message()
{
    delete message_;
    message_ = nullptr;
}
