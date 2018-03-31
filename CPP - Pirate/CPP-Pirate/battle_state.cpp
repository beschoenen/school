#include "stdafx.h"
#include "battle_state.h"
#include "game.h"
#include "ship_reader.h"
#include "random.h"
#include "light_cannon.h"
#include "heavy_cannon.h"

battle_state::battle_state(game* game) : battle_state(game, nullptr)
{
}

battle_state::battle_state(game* game, state* old_state) : state(game, old_state)
{
    get_random_pirate_ship();
}

battle_state::~battle_state()
{
    pirate_ship->clear_cannons();
    pirate_ship->clear_items();

    delete pirate_ship;
}

str* battle_state::render()
{
    char buff[1000]{};
    char state[] = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n~~~~~~~~~ Sea Battle! ~~~~~~~~~\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n";

    char pirate_first[] = "You've encountered a pirate crew on a ";

    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%s%s. You count %u cannons.\n\n", state, pirate_first, pirate_ship->name->data(), pirate_ship->cannon_count());

    if (!get_message()->empty())
    {
        _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%s\n\n", buff, get_message()->data());
        delete_message();
    }

    char actions[] = "Use \"shoot\" to combat the pirates.\nUse \"flee\" to try and escape.\nUse \"surrender\" to lose all inventory but stay alive.";

    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%s", buff, actions);

    return new str(buff);
}

void battle_state::handle_input(const str* input)
{
    if (*input == "shoot")
    {
        player_shoots();

        if (pirate_ship->current_health > 0)
        {
            pirate_shoots();
        }
        else
        {
            set_msg(new str("You sank the pirate vessel."));
            game_->exit_current_state(old_state_);
        }
    }
    else if (*input == "flee")
    {
        pirate_shoots();

        if (has_fled())
        {
            set_msg(new str("You fled from the pirate."));
            game_->exit_current_state(old_state_);
        }
    }
    else if (*input == "surrender")
    {
        set_msg(new str("You surrendered leaving your ship unharmed, but you lost your inventory."));

        game_->current_ship->clear_items();
        game_->exit_current_state(old_state_);
    }
}

void battle_state::get_random_pirate_ship()
{
    ship* ships[S_SIZE];

    const auto reader = new ship_reader();
    reader->get_ships(ships);

    delete reader;

    const auto index = random::get(S_SIZE - 1);

    pirate_ship = ships[index];

    const auto min_cannons = 5;
    const int max_cannons = random::get(min_cannons, static_cast<int>(pirate_ship->cannon_space * 0.8));

    for (auto i = 0; i < max_cannons; i++)
    {
        const int cannon_type = random::get(100);
        if (cannon_type <= 40)
        {
            pirate_ship->add_cannon(new light_cannon);
        }
        else if (cannon_type <= 80)
        {
            pirate_ship->add_cannon(new cannon);
        }
        else
        {
            if (pirate_ship->has_attribute(ship::small))
            {
                i--;
                continue;
            }

            pirate_ship->add_cannon(new heavy_cannon);            
        }
    }

    for (auto i = 0; i < S_SIZE; i++)
    {
        if (i == index) continue;

        delete ships[i];
    }
}


bool battle_state::has_fled() const
{
    const int flee_chance = random::get(1, 100);

    const auto pirate = pirate_ship->has_attribute(ship::light)
                           ? ship::light
                           : pirate_ship->has_attribute(ship::heavy)
                           ? ship::heavy
                           : 2;

    const auto player = game_->current_ship->has_attribute(ship::light)
                           ? ship::light
                           : game_->current_ship->has_attribute(ship::heavy)
                           ? ship::heavy
                           : 2;

    //2 = normal
    //1 = heavy
    //0 = light
    if (pirate == 2)
    {
        if (player == 2)
        {
            return flee_chance <= 40;
        }
        if (player == 1)
        {
            return flee_chance <= 15;
        }
        if (player == 0)
        {
            return flee_chance <= 60;
        }
    }
    else if (pirate == 1)
    {
        if (player == 2)
        {
            return flee_chance <= 55;
        }
        if (player == 1)
        {
            return flee_chance <= 30;
        }
        if (player == 0)
        {
            return flee_chance <= 75;
        }
    }
    else if (pirate == 0)
    {
        if (player == 2)
        {
            return flee_chance <= 30;
        }
        if (player == 1)
        {
            return flee_chance <= 5;
        }
        if (player == 0)
        {
            return flee_chance <= 50;
        }
    }

    return false;
}

void battle_state::set_msg(str* message) const
{
    if (!old_state_->get_message()->empty())
    {
        char buff[300]{};
        const auto old_message = old_state_->get_message();

        _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s\n\n%s", message->data(), old_message->data());
        old_state_->set_message(new str(buff));
        delete message;
    }
    else
    {
        old_state_->set_message(message);
    }
}

void battle_state::pirate_shoots()
{
    auto dmg = 0;
    for (auto cannon : pirate_ship->cannons)
    {
        if (cannon == nullptr) break;
        dmg += cannon->get_damage();
    }

    game_->current_ship->current_health -= dmg;

    char buff[600]{};
    if (!get_message()->empty())
    {
        _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s\n", get_message()->data());
    }
    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%s%d%s", buff, "The pirate has done ", dmg, " point of damage to you.");

    set_message(new str(buff));
}

void battle_state::player_shoots()
{
    auto dmg = 0;
    for (auto cannon : game_->current_ship->cannons)
    {
        if (cannon == nullptr) break;
        dmg += cannon->get_damage();
    }

    pirate_ship->current_health -= dmg;

    char buff[600]{};
    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s%d%s%d%s", "You have done ", dmg, " to the pirate, he has ",
                pirate_ship->current_health, " hitpoints left.");
    set_message(new str(buff));
}
