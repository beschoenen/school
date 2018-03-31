#pragma once
#include "str.h"

class game;

class state
{
    str* message_;

protected:
    game* game_;
    state* old_state_;

public:
    state(game* game, state* old_state);
    virtual ~state();

    virtual str* render();
    virtual void handle_input(const str* input);

    void cleanup() const;

    str* get_message() const;
    void set_message(str* message);
    void delete_message();
};
