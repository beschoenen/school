#include "stdafx.h"
#include "cannon_shop_sell_state.h"
#include "game.h"
#include <stdio.h>


cannon_shop_sell_state::cannon_shop_sell_state(game* game) : cannon_shop_sell_state(game, nullptr)
{
}

cannon_shop_sell_state::cannon_shop_sell_state(game* game, state* old_state) : state(game, old_state)
{
    for (auto i = 0; i < CANNON_SIZE; i++)
    {
        cannon* cannon = game_->current_ship->cannons[i];

        if (cannon == nullptr) break;

        if (cannon->is("light"))
        {
            count_light_++;
        }
        else if (cannon->is("heavy"))
        {
            count_heavy_++;
        }
        else if (cannon->is("normal"))
        {
            count_normal_++;
        }
    }
}

cannon_shop_sell_state::~cannon_shop_sell_state()
{
}

str* cannon_shop_sell_state::render()
{
    char temp[600] = "Wanna sell some cannons, eh?\n\n";

    if (!get_message()->empty())
    {
        _snprintf_s(temp, sizeof temp, _TRUNCATE, "%s%s\n\n", temp, get_message()->data());
        delete_message();
    }

    _snprintf_s(temp, sizeof temp, _TRUNCATE, "%sUse \"light\" to sell a light cannon for 25G.\tYou have %i.\n", temp,
                count_light_);
    _snprintf_s(temp, sizeof temp, _TRUNCATE, "%sUse \"normal\" to sell a normal cannon for 100G.\tYou have %i.\n",
                temp, count_normal_);
    _snprintf_s(temp, sizeof temp, _TRUNCATE, "%sUse \"heavy\" to sell a heavy cannon for 500G.\tYou have %i.\n", temp,
                count_heavy_);
    _snprintf_s(temp, sizeof temp, _TRUNCATE, "%sUse \"back\" to go back.", temp);

    return new str(temp);
}

void cannon_shop_sell_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else if (*input == "light")
    {
        if (count_light_ < 1)
        {
            set_message(new str("You have no more light cannons to sell."));
        }
        else
        {
            auto* cannon_ = game_->current_ship->remove_cannon("light");
            game_->gold += cannon_->get_sell_price();
            count_light_--;
            delete cannon_;
            set_message(new str("You sold a light cannon."));
        }
    }
    else if (*input == "normal")
    {
        if (count_normal_ < 1)
        {
            set_message(new str("You have no more normal cannons to sell."));
        }
        else
        {
            auto* cannon_ = game_->current_ship->remove_cannon("normal");
            game_->gold += cannon_->get_sell_price();
            count_normal_--;
            delete cannon_;
            set_message(new str("You sold a normal cannon."));
        }
    }
    else if (*input == "heavy")
    {
        if (count_heavy_ < 1)
        {
            set_message(new str("You have no more heavy cannons to sell."));
        }
        else
        {
            auto* cannon_ = game_->current_ship->remove_cannon("heavy");
            game_->gold += cannon_->get_sell_price();
            count_heavy_--;
            delete cannon_;
            set_message(new str("You sold a heavy cannon."));
        }
    }
}
