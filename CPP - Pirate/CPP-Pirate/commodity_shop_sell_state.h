#pragma once
#include "state.h"

class commodity_shop_sell_state : public state
{
public:
    explicit commodity_shop_sell_state(game* game);
    explicit commodity_shop_sell_state(game* game, state* old_state);
    ~commodity_shop_sell_state();

    str* render() override;
    void handle_input(const str* input) override;
};
