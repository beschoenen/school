#pragma once
#include "str.h"
#include "state.h"
#include "port.h"
#include "ship.h"

class game
{
public:
    state* current_state;
    port* current_port;
    ship* current_ship;

    size_t gold = 100000;
    bool over = false;

    explicit game(port* port);
    ~game();

    str* render() const;
    void handle_input(const str* input) const;

    template <class T>
    void go_to_state(bool clean = false);

    void exit_current_state(state* old_state);
};

template <class T>
void game::go_to_state(const bool clean)
{
    if (clean)
    {
        current_state->cleanup();
        delete current_state;

        current_state = new T(this);
    }
    else
    {
        auto new_state = new T(this, current_state);
        current_state = new_state;
    }
}
