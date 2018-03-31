#include "stdafx.h"
#include "destination_state.h"
#include "shop_state.h"
#include "game.h"
#include "journey_reader.h"
#include "sailing_state.h"

destination_state::destination_state(game* game) : destination_state(game, nullptr)
{
}

destination_state::destination_state(game* game, state* old_state) : state(game, old_state)
{
    const auto reader = new journey_reader();
    reader->get_journeys(*&journeys_, game_->current_port->name->data());

    delete reader;
}

destination_state::~destination_state()
{
    for (auto journey : journeys_)
    {
        delete journey;
    }
}

str* destination_state::render()
{
    char temp[500]{};

    auto i = 0;
    for (auto item : journeys_)
    {
        _snprintf_s(temp, sizeof temp, _TRUNCATE, "%s%02i. %s: %i\n", temp, i + 1, item->name->data(), item->turns);
        i++;
    }

    const char first[] = "Wanna go sailing, eh?\n\nHere's a list of ports you can sail to:";
    const char second[] = "Use a corresponding number to sail to that port.\nUse \"back\" to go back.";

    char buff[600]{};
    _snprintf_s(buff, sizeof buff, _TRUNCATE, "%s\n\n%s\n\n%s", first, temp, second);

    return new str(buff);
}

void destination_state::handle_input(const str* input)
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

        if (index < 0 || index > sizeof journeys_)
        {
            set_message(new str("Invalid input!"));
            return;
        }

        if (*game_->current_port->name == *journeys_[index]->name)
        {
            set_message(new str("You can't sail to your current location."));
            return;
        }

        delete game_->current_port;
        game_->current_port = new port(journeys_[index]->name->data(), journeys_[index]->turns);

        game_->go_to_state<sailing_state>(true);
    }
}
