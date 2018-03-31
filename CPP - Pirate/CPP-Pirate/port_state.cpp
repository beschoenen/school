#include "stdafx.h"
#include "port_state.h"
#include "shop_state.h"
#include <utility>
#include "game.h"
#include "destination_state.h"

port_state::port_state(game* game) : port_state(game, nullptr)
{
}

port_state::port_state(game* game, state* old_state) : state(game, old_state)
{
}

port_state::~port_state()
{
}

str* port_state::render()
{
    char temp[100]{};

    if (!get_message()->empty())
    {
        _snprintf_s(temp, sizeof temp, _TRUNCATE, "%s\n\n", get_message()->data());
        delete_message();
    }

    char buff[600]{};
    char second[] = "Use \"shop\" to go shopping.\nUse \"sail\" to go sailing.\nUse \"repair\" to restore your hitpoints (1G for 10HP).\nUse \"quit\" to stop the game.";

    _snprintf_s(buff, sizeof buff, _TRUNCATE, "You are currently in port %s.\n\n%s%s", game_->current_port->name->data(), temp, second);

    return new str(buff);
}

void port_state::handle_input(const str* input)
{
    if (*input == "quit")
    {
        game_->over = true;
    }
    else if (*input == "sail")
    {
        game_->go_to_state<destination_state>();
    }
    else if (*input == "shop")
    {
        game_->go_to_state<shop_state>();
    }
    else if (*input == "repair")
    {
        repair();
    }
}

void port_state::repair()
{
    const auto difference = game_->current_ship->health - game_->current_ship->current_health;
    if(difference > 0)
    {
        const auto cost = static_cast<int>(ceil(difference / 10));

        if (game_->gold < cost)
        {
            return set_message(new str("You don't have enough gold to repair your ship."));
        }

        game_->gold -= cost;
        game_->current_ship->current_health = game_->current_ship->health;

        char buff[100]{};
        _snprintf_s(buff, sizeof buff, _TRUNCATE, "You repaired your ship for %d gold.", cost);

        return set_message(new str(buff));
    }

    return set_message(new str("Your ship doesn't need repairs."));
}
