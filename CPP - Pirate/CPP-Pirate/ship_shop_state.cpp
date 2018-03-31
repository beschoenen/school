#include "stdafx.h"
#include "ship_shop_state.h"
#include "game.h"

ship_shop_state::ship_shop_state(game* game) : ship_shop_state(game, nullptr)
{
}

ship_shop_state::ship_shop_state(game* game, state* old_state) : state(game, old_state)
{
    const auto reader = new ship_reader();
    reader->get_ships(ships_);

    delete reader;
}

ship_shop_state::~ship_shop_state()
{
    for (auto i = 0; i < S_SIZE; i++)
    {
        delete ships_[i];
    }
}

str* ship_shop_state::render()
{
    char temp[1200]{};

    if (!get_message()->empty())
    {
        _snprintf_s(temp, sizeof temp, _TRUNCATE, "%s\n\n", get_message()->data());
        delete_message();
    }

    char attributes[100]{};

    auto i = 0;
    for (auto item : ships_)
    {
        memset(attributes, 0x00, 100);

        for (auto x = 0; x < 2; x++)
        {
            str* name = nullptr;
            switch (item->attr[x])
            {
            case 0:
                name = new str("Light");
                break;
            case 1:
                name = new str("Heavy");
                break;
            case 2:
                name = new str("Small");
                break;
            default:
                continue;
            }

            if (item->attr[x] < 3)
            {
                if (strlen(attributes) > 0)
                {
                    _snprintf_s(attributes, sizeof attributes, _TRUNCATE, "%s, %s", attributes, name->data());
                }
                else
                {
                    _snprintf_s(attributes, sizeof attributes, _TRUNCATE, "%s", name->data());
                }

                delete name;
            }
        }

        if (strlen(attributes) < 1)
        {
            _snprintf_s(attributes, sizeof attributes, _TRUNCATE, "%s", "None");
        }

        _snprintf_s(temp, sizeof temp, _TRUNCATE,
            "%s%02i. %s\t%s%uG, Cargo: %u, Cannons: %u\n\t\t\tHP: %d. Attributes: %s\n",
            temp, 
            i + 1, 
            item->name->data(), 
            strlen(item->name->data()) < 8 ? "\t" : "",
            item->price, 
            item->space, 
            item->cannon_space, 
            item->health,
            attributes
        );

        i++;
    }

    char buff[1200]{};

    _snprintf_s(buff, sizeof buff, _TRUNCATE, 
        "%s\n\n%s\n%s", 
        "Welcome to the ship shop.",
        temp, 
        "Use a corresponding number to buy a ship.\nUse \"back\" to go back."
    );

    return new str(buff);
}

void ship_shop_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else
    {
        int index;
        try
        {
            index = atoi(input->data()) - 1;
        }
        catch (_exception)
        {
            set_message(new str("Invalid input!"));
            return;
        }

        if (index < 0 || index > sizeof ships_)
        {
            set_message(new str("Invalid input!"));
            return;
        }

        // Check if the user tries the buy their current ship
        if (*game_->current_ship->name == *ships_[index]->name)
        {
            set_message(new str("You can't buy your current ship."));
            return;
        }

        if (game_->gold < ships_[index]->price)
        {
            char msg[100]{};

            _snprintf_s(msg, sizeof msg, _TRUNCATE, "You don't have enough gold to buy the %s.", ships_[index]->name->data());
            set_message(new str(msg));
            return;
        }

        // Check when the new ship is small that the user doesn't have heavy cannons
        if (ships_[index]->has_attribute(ship::small))
        {
            for (auto cannon : game_->current_ship->cannons)
            {
                if (cannon == nullptr) break;

                if (!cannon->is("heavy")) continue;

                set_message(new str("Please sell all your heavy cannons before buying this ship."));
                return;
            }
        }

        // Check if current cannons fit in new ship
        const auto no_cannons = game_->current_ship->cannon_count();
        if (no_cannons > game_->current_ship->cannon_space)
        {
            char temp[100]{};

            _snprintf_s(temp, sizeof temp, _TRUNCATE, "Please sell %u cannons before buying this ship.",
                        no_cannons - game_->current_ship->cannon_space);
            set_message(new str(temp));
            return;
        }

        // Check if current cargo fits in new ship
        const auto no_items = game_->current_ship->commodity_count();
        if (no_items > game_->current_ship->space)
        {
            char temp[100]{};

            _snprintf_s(temp, sizeof temp, _TRUNCATE, "Please sell %u items before buying this ship.",
                        no_items - game_->current_ship->space);
            set_message(new str(temp));
            return;
        }

        const auto new_ship = new ship(*ships_[index]);

        game_->gold -= new_ship->price;
        game_->gold += game_->current_ship->get_sell_price();

        // Move cannons & cargo
        new_ship->move_items(*&game_->current_ship->inventory, *&game_->current_ship->cannons);

        // Replace ship
        delete game_->current_ship;
        game_->current_ship = new_ship;

        char msg[100]{};

        _snprintf_s(msg, sizeof msg, _TRUNCATE, "You bought a %s.", new_ship->name->data());
        set_message(new str(msg));
    }
}
