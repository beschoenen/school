#include "stdafx.h"
#include "cannon_shop_state.h"
#include "game.h"
#include "cannon_shop_buy_state.h"
#include "cannon_shop_sell_state.h"

cannon_shop_state::cannon_shop_state(game* game) : cannon_shop_state(game, nullptr)
{
}

cannon_shop_state::cannon_shop_state(game* game, state* old_state) : state(game, old_state)
{
}

cannon_shop_state::~cannon_shop_state()
{
}

str* cannon_shop_state::render()
{
    return new str("Welcome to the cannon shop.\n\nWould you like to buy or sell?\n\nUse \"buy\" to buy items.\nUse \"sell\" to sell item.\nUse \"back\" to go back.");
}

void cannon_shop_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else if (*input == "buy")
    {
        game_->go_to_state<cannon_shop_buy_state>();
    }
    else if (*input == "sell")
    {
        game_->go_to_state<cannon_shop_sell_state>();
    }
}
