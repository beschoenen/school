#include "stdafx.h"
#include "commodity_shop_state.h"
#include "game.h"
#include "commodity_shop_buy_state.h"
#include "commodity_shop_sell_state.h"

commodity_shop_state::commodity_shop_state(game* game) : commodity_shop_state(game, nullptr)
{
}

commodity_shop_state::commodity_shop_state(game* game, state* old_state) : state(game, old_state)
{
}

commodity_shop_state::~commodity_shop_state()
{
}

str* commodity_shop_state::render()
{
    return new str("Welcome to the commodity shop.\n\nWould you like to buy of sell?\n\nUse \"buy\" to buy items.\nUse \"sell\" to sell item.\nUse \"back\" to go back.");
}

void commodity_shop_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else if (*input == "buy")
    {
        game_->go_to_state<commodity_shop_buy_state>();
    }
    else if (*input == "sell")
    {
        game_->go_to_state<commodity_shop_sell_state>();
    }
}
