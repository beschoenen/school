#pragma once
#include "state.h"

class cannon_shop_sell_state : public state
{
    int count_light_ = 0;
    int count_normal_ = 0;
    int count_heavy_ = 0;
public:
    explicit cannon_shop_sell_state(game* game);
    explicit cannon_shop_sell_state(game* game, state* old_state);
    ~cannon_shop_sell_state();

    str* render() override;
    void handle_input(const str* input) override;
};
