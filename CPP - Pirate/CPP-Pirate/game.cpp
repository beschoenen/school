#include "stdafx.h"
#include "game.h"
#include "port_state.h"
#include <iostream>
#include "ship_reader.h"

game::game(port* port) : current_state(new port_state(this)), current_port(port)
{
    const auto reader = new ship_reader();
    current_ship = reader->get_ship("Pinnace");

    delete reader;
}

game::~game()
{
    current_ship->clear_items();
    current_ship->clear_cannons();

    current_state->cleanup();

    delete current_state;
    delete current_port;
    delete current_ship;
}

str* game::render() const
{
    return current_state->render();
}

void game::handle_input(const str* input) const
{
    current_state->handle_input(input);
}

void game::exit_current_state(state* old_state)
{
    if (old_state != nullptr)
    {
        delete current_state;
        current_state = old_state;
    }
}
