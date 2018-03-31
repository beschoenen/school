#pragma once
#include "state.h"

class cannon_shop_state : public state
{
public:
    explicit cannon_shop_state(game* game);
    explicit cannon_shop_state(game* game, state* old_state);
    ~cannon_shop_state();

    str* render() override;
    void handle_input(const str* input) override;
};
