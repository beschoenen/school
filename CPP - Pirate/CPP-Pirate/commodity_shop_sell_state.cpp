#include "stdafx.h"
#include "commodity_shop_sell_state.h"
#include "game.h"
#include <stdio.h>
#include <cstring>


commodity_shop_sell_state::commodity_shop_sell_state(game* game) : commodity_shop_sell_state(game, nullptr)
{
}

commodity_shop_sell_state::commodity_shop_sell_state(game* game, state* old_state) : state(game, old_state)
{
}

commodity_shop_sell_state::~commodity_shop_sell_state()
{
}

str* commodity_shop_sell_state::render()
{
    char temp[1000]{};

    if (get_message() != nullptr && !get_message()->empty())
    {
        _snprintf_s(temp, sizeof temp, _TRUNCATE, "%s\n\n", get_message()->data());
        delete_message();
    }

    for (auto i = 0; i < 15; i++)
    {
        const auto stock = game_->current_port->stocks[i];

        const auto name = stock->name->data();
        const auto price = game_->current_port->get_price_for_stock(name);
        const auto count = game_->current_ship->get_amount_of_commodity(name);

        _snprintf_s(temp, sizeof temp, _TRUNCATE,
            "%s%02i. %s\t%s%iG\tYou have %03i onboard.\n",
            temp,
            i + 1,
            name,
            strlen(name) < 4 ? "\t" : "",
            price,
            count
        );
    }

    char buff[1000]{};

    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s\n\n%s\n\n%s",
        "Wanna sell some goods, eh?",
        temp,
        "Use a corresponding number to buy a commodity.\nUse \"back\" to go back."
    );

    return new str(buff);
}

void commodity_shop_sell_state::handle_input(const str* input)
{
    if (*input == "back")
    {
        game_->exit_current_state(old_state_);
    }
    else
    {
        auto index = -1;
        auto count = 1;
        try
        {
            auto second = false;

            auto current = 0;

            char current_string[20];
            memset(current_string, 0x00, 20);

            const auto line = input->data();
            for (size_t i = 0; i < strlen(line); i++)
            {
                if (line[i] == ' ')
                {
                    if (second) break;
                    second = true;

                    index = atoi(current_string) - 1;

                    current = 0;
                    memset(current_string, 0x00, 20);
                    continue;
                }

                current_string[current++] = line[i];
            }

            if (strlen(current_string) > 0)
            {
                count = atoi(current_string);
            }
        }
        catch (_exception)
        {
            set_message(new str("Invalid input!"));
            return;
        }

        if (index < 0 || index > 15)
        {
            set_message(new str("Invalid input!"));
            return;
        }

        const auto stock = game_->current_port->stocks[index];

        count = game_->current_ship->can_sell(stock->name->data(), count);
        if (count < 1)
        {
            char msg[100]{};

            _snprintf_s(msg, sizeof msg, _TRUNCATE, "You have no %s onboard.", stock->name->data());

            set_message(new str(msg));
            return;
        }

        game_->gold += game_->current_port->get_price_for_stock(stock->name->data()) * count;

        for (auto i = 0; i < count; i++)
        {
            const auto item = game_->current_ship->remove_commodity(stock->name->data());
            delete item;
        }

        char msg[100]{};

        _snprintf_s(msg, sizeof msg, _TRUNCATE, "You sold %i %s.", count, stock->name->data());
        set_message(new str(msg));
    }
}
