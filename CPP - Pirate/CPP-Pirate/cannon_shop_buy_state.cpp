#include "stdafx.h"
#include "game.h"
#include "cannon_shop_buy_state.h"
#include <stdio.h>
#include "light_cannon.h"
#include "heavy_cannon.h"

cannon_shop_buy_state::cannon_shop_buy_state(game* game) : cannon_shop_buy_state(game, nullptr)
{
}

cannon_shop_buy_state::cannon_shop_buy_state(game* game, state* old_state) : state(game, old_state)
{
}

cannon_shop_buy_state::~cannon_shop_buy_state()
{
}

str* cannon_shop_buy_state::render()
{
    char temp[600] = "Wanna buy some cannons, eh?\n\n";

    if (!get_message()->empty())
    {
        _snprintf_s(temp, sizeof temp, _TRUNCATE, "%s%s\n\n", temp, get_message()->data());
        delete_message();
    }

    _snprintf_s(temp, sizeof temp, _TRUNCATE,
                "%sUse \"light\" to buy a light cannons, for 50G.\tWe have %i in stock (You have %02i).\n", temp,
                game_->current_port->get_stock("light_cannon"), game_->current_ship->get_amount_of_cannon("light"));
    _snprintf_s(temp, sizeof temp, _TRUNCATE,
                "%sUse \"normal\" to buy a normal cannons, for 200G.\tWe have %i in stock (You have %02i).\n", temp,
                game_->current_port->get_stock("normal_cannon"), game_->current_ship->get_amount_of_cannon("normal"));
    _snprintf_s(temp, sizeof temp, _TRUNCATE,
                "%sUse \"heavy\" to buy a heavy cannons, for 1000G.\tWe have %i in stock (You have %02i).\n", temp,
                game_->current_port->get_stock("heavy_cannon"), game_->current_ship->get_amount_of_cannon("heavy"));
    _snprintf_s(temp, sizeof temp, _TRUNCATE, "%sUse \"back\" to go back.", temp);

    return new str(temp);
}

void cannon_shop_buy_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else if (*input == "light")
    {
        if (game_->current_port->can_buy("light_cannon") < 1)
        {
            set_message(new str("No more light cannons in stock."));
        }
        else if (!game_->current_ship->can_fit_cannon())
        {
            set_message(new str("You can't fit any more cannons."));
        }
        else if (game_->gold < 50)
        {
            set_message(new str("You don't have enough to gold to buy a light cannon."));
        }
        else
        {
            const auto cannon_ = new light_cannon();
            game_->current_port->buy("light_cannon");
            game_->current_ship->add_cannon(cannon_);
            game_->gold -= cannon_->price;
            set_message(new str("You bought a light cannon."));
        }
    }
    else if (*input == "normal")
    {
        if (game_->current_port->can_buy("normal_cannon") < 1)
        {
            set_message(new str("No more normal cannons in stock."));
        }
        else if (!game_->current_ship->can_fit_cannon())
        {
            set_message(new str("You can't fit any more cannons."));
        }
        else if (game_->gold < 200)
        {
            set_message(new str("You don't have enough to gold to buy a cannon."));
        }
        else
        {
            const auto cannon_ = new cannon();
            game_->current_port->buy("normal_cannon");
            game_->current_ship->add_cannon(cannon_);
            game_->gold -= cannon_->price;
            set_message(new str("You bought a normal cannon."));
        }
    }
    else if (*input == "heavy")
    {
        if (game_->current_ship->has_attribute(ship::small))
        {
            set_message(new str("Your current ship is too small to carry heavy cannons."));
        }
        else if (game_->current_port->can_buy("heavy_cannon") < 1)
        {
            set_message(new str("No more heavy cannons in stock."));
        }
        else if (!game_->current_ship->can_fit_cannon())
        {
            set_message(new str("You can't fit any more cannons."));
        }
        else if (game_->gold < 1000)
        {
            set_message(new str("You don't have enough to gold to buy a heavy cannon."));
        }
        else
        {
            const auto cannon_ = new heavy_cannon();
            game_->current_port->buy("heavy_cannon");
            game_->current_ship->add_cannon(cannon_);
            game_->gold -= cannon_->price;
            set_message(new str("You bought a heavy cannon."));
        }
    }
}
