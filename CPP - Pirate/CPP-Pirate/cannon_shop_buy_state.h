#pragma once
#include "state.h"

class cannon_shop_buy_state : public state
{
public:
    explicit cannon_shop_buy_state(game* game);
    explicit cannon_shop_buy_state(game* game, state* old_state);
    ~cannon_shop_buy_state();

    str* render() override;
    void handle_input(const str* input) override;
};
