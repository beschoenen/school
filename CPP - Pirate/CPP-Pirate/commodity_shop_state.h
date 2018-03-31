#pragma once
#include "state.h"

class commodity_shop_state : public state
{
public:
    explicit commodity_shop_state(game* game);
    explicit commodity_shop_state(game* game, state* old_state);
    ~commodity_shop_state();

    str* render() override;
    void handle_input(const str* input) override;
};
