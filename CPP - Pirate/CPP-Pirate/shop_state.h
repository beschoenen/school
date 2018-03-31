#pragma once
#include "state.h"

class shop_state : public state
{
public:
    explicit shop_state(game* game);
    explicit shop_state(game* game, state* old_state);
    ~shop_state();

    str* render() override;
    void handle_input(const str* input) override;
};
