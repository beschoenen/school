#include "stdafx.h"
#include "shop_state.h"
#include "game.h"
#include "cannon_shop_state.h"
#include "commodity_shop_state.h"
#include "ship_shop_state.h"

shop_state::shop_state(game* game) : shop_state(game, nullptr)
{
}

shop_state::shop_state(game* game, state* old_state) : state(game, old_state)
{
}

shop_state::~shop_state()
{
}

str* shop_state::render()
{
    return new str("Wanna go shopping, eh?\n\nUse \"cannon\" to go cannon shopping.\nUse \"commodity\" to go commodity shopping.\nUse \"ship\" to go ship shopping.\nUse \"back\" to go back.");
}

void shop_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else if (*input == "cannon")
    {
        game_->go_to_state<cannon_shop_state>();
    }
    else if (*input == "commodity")
    {
        game_->go_to_state<commodity_shop_state>();
    }
    else if (*input == "ship")
    {
        game_->go_to_state<ship_shop_state>();
    }
}
