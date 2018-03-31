#include "BuildingRemovalState.h"
#include "../../Models/Game.h"

BuildingRemovalState::BuildingRemovalState() noexcept : BuildingRemovalState(nullptr)
{
}

BuildingRemovalState::BuildingRemovalState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void BuildingRemovalState::enter_state() noexcept
{
    const auto game = Game::instance();
    opponent = game->current_client == game->client1 ? game->client2 : game->client1;

    game->current_client->render_screen();
}

void BuildingRemovalState::render(std::shared_ptr<ClientInfo>& client) noexcept
{
    client->send_header();

    const auto game = Game::instance();
    if (client == game->current_client)
    {
        if (opponent->get_player().number_built_buildings() >= 8)
        {
            client->get_socket() << "Your opponent has a full city, you can't remove any of his buildings."
                                 << Socket::endl << Socket::endl
                                 << "Press any key to return" << Socket::endl;
            return;
        }

        client->get_socket() << "Your opponent has the following buildings:" << Socket::endl << Socket::endl
                             << build_cards_view()
                             << "(" << opponent->get_player().number_built_buildings() + 1
                             << ") Go back." << Socket::endl << Socket::endl
                             << "Pick a building to destroy."
                             << Socket::endl << Socket::endl << Socket::prompt;
    }
    else
    {
        client->get_socket() << "Please wait for the player to play their turn." << Socket::endl;
    }
}

void BuildingRemovalState::handle_input(std::shared_ptr<ClientCommand>& command) noexcept
{
    const auto game = Game::instance();
    const auto client = command->get_client_info().lock();

    if (client != game->current_client) return;

    if (opponent->get_player().number_built_buildings() >= 8)
    {
        switch_state = true;
        return;
    }

    int action = -1;
    try
    {
        action = atoi(command->get_cmd().c_str());
    }
    catch (...) {}
    action--;

    if (action == opponent->get_player().number_built_buildings())
    {
        switch_state = true;
        return;
    }

    if (action < 0 || action >= opponent->get_player().number_built_buildings())
    {
        client->set_message("Invalid input, please try again.");
        return;
    }

    std::shared_ptr<BuildingCard> found_card = nullptr;

    auto opp = opponent->get_player().building_cards;

    std::vector<std::shared_ptr<BuildingCard>> buildings;
    std::copy_if(opp.begin(), opp.end(), std::back_inserter(buildings), [](std::shared_ptr<BuildingCard> item) {
        return item->is_built;
    });

    int index = 0;
    for (auto& item : buildings)
    {
        if (index == action)
        {
            found_card = item;
            break;
        }

        index++;
    }

    if (found_card == nullptr)
    {
        client->set_message("Invalid input, please try again.");
        return;
    }

    if (found_card->protected_card)
    {
        client->set_message("This card is protected by the preacher!, please pick another one.");
        return;
    }

    if (client->get_player().gold < found_card->price - 1)
    {
        client->set_message("You do not have enough gold to destroy this building");
        return;
    }

    client->get_player().gold -= (found_card->price - 1);
    building_name = found_card->name;

    found_card->is_built = false;
    found_card->protected_card = false;

    opponent->get_player().building_cards.erase(std::find(
        opponent->get_player().building_cards.begin(), opponent->get_player().building_cards.end(), found_card)
    );

    opponent->set_message("Your opponent destroyed your " + building_name + ".");
    client->set_message("You destroyed your opponent's " + building_name + ".");

    client->get_player().get_turn().use_ability();

    switch_state = true;
}

void BuildingRemovalState::update() noexcept
{
    if (switch_state)
    {
        Game::instance()->exit_current_state(old_state);
    }
}

std::string BuildingRemovalState::build_cards_view() const noexcept
{
    std::stringstream view;

    auto index = 1;
    for (const auto& card : opponent->get_player().building_cards)
    {
        if (!card->is_built) continue;

        view << "(" << index++ << ") " << card->to_string() << Socket::endl;
    }

    return view.str();
}
