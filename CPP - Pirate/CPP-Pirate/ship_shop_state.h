#pragma once
#include "state.h"
#include "ship_reader.h"

class ship_shop_state : public state
{
    ship* ships_[S_SIZE]{nullptr};
public:
    explicit ship_shop_state(game* game);
    explicit ship_shop_state(game* game, state* old_state);
    ~ship_shop_state();

    str* render() override;
    void handle_input(const str* input) override;
};
