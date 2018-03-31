#pragma once
#include "state.h"

class port_state : public state
{
public:
    explicit port_state(game* game);
    explicit port_state(game* game, state* old_state);
    ~port_state();

    str* render() override;
    void handle_input(const str* input) override;

    void repair();
};
