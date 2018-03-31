#include "stdafx.h"
#include <stdio.h>
#include "sailing_state.h"
#include "port_state.h"
#include "battle_state.h"
#include "game.h"
#include "random.h"

sailing_state::sailing_state(game* game) : sailing_state(game, nullptr)
{
}

sailing_state::sailing_state(game* game, state* old_state) : state(game, old_state), turns_(game_->current_port->turns)
{
}

sailing_state::~sailing_state()
{
}

str* sailing_state::render()
{
    char buff[600]{};
    char state[] =
        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n~~~~~~~ On the open sea ~~~~~~~\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n";

    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s", state);

    if (!get_message()->empty())
    {
        _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%s\n\n", buff, get_message()->data());
        delete_message();
    }

    char turns_left[] = " turns to port ";
    const auto port_name = game_->current_port->name;
    char actions[] = "Press \"Enter\" to go to next turn.";

    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%d%s%s.\n\n%s", buff, turns_, turns_left, port_name->data(), actions);

    return new str(buff);
}

void sailing_state::handle_input(const str* input)
{
    if (*input == "next" || *input == "")
    {
        const auto decrease_turns = random_wind();
        turns_ -= decrease_turns;

        if (turns_ > 0 && random_pirate())
        {
            game_->go_to_state<battle_state>();
            return;
        }

        if (turns_ <= 0)
        {
            game_->current_port->load_stock();
            game_->go_to_state<port_state>(true);
        }
    }
}

bool sailing_state::random_pirate() const
{
    const auto random = random::get(1, 100);

    return random <= 20;
}

int sailing_state::random_wind()
{
    auto random = random::get(1, 20);

    if (random >= 1 && random <= 2)
    {
        set_message(new str("There is no wind at the moment. You have to wait for it to pick up again.\n   -0 turn."));
        return 0;
    }

    if (random >= 3 && random <= 4)
    {
        if (game_->current_ship->has_attribute(ship::light))
        {
            set_message(new str("There is a slight breeze, just enough for your light ship to sail.\n   -1 turn."));
            return 1;
        }
        set_message(new str("There is a slight breeze, but not enough for you to sail.\n   -0 turn."));
        return 0;
    }

    if (random >= 5 && random <= 7)
    {
        if (game_->current_ship->has_attribute(ship::heavy))
        {
            set_message(new str("The wind is weak, but for a heavy ship, that isn't enough.\n   -0 turn."));
            return 0;
        }
        set_message(new str("The wind is weak, but strong enough to sail.\n   -1 turn."));
        return 1;
    }

    if (random >= 8 && random <= 17)
    {
        set_message(new str("There is good wind, a nice day to sail.\n   -1 turn."));
        return 1;
    }

    if (random >= 18 && random <= 19)
    {
        set_message(new str("There is a storm coming. The wind is fierce, good for progression.\n   -2 turns."));
        return 2;
    }

    if (random == 20)
    {
        random = random::get(1, 100);

        char buffer[600]{};

        game_->current_ship->current_health -= random;
        _snprintf_s(buffer, sizeof buffer, _TRUNCATE, "%s\n%s%d%s\n\n", "You're in the middle of a big storm!",
                                                                        "You lost ", random, " hitpoints.");

        if (random <= 40)
        {
            _snprintf_s(buffer, sizeof buffer, _TRUNCATE, "%s%s", buffer,
                        "Your ship was blown out of course, losing some progression.\n   +1 turn.");
            set_message(new str(buffer));
            return -1;
        }
        if (random > 40 && random <= 80)
        {
            _snprintf_s(buffer, sizeof buffer, _TRUNCATE, "%s%s", buffer,
                        "Fortunately you make it to the eye of the storm, not losing any progression.\n   -0 turn.");
            set_message(new str(buffer));
            return 0;
        }
        _snprintf_s(buffer, sizeof buffer, _TRUNCATE, "%s%s", buffer,
                    "Luckily, you maneuvered your ship to take advantage of the heavy winds, making some progression.\n   -1 turn.");
        set_message(new str(buffer));
        return 1;
    }

    return 0;
}
