#include "BuildingCardTradeState.h"
#include "../../Utilities/Random.h"
#include "../../Models/Game.h"

BuildingCardTradeState::BuildingCardTradeState() noexcept : BuildingCardTradeState(nullptr)
{
}

BuildingCardTradeState::BuildingCardTradeState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void BuildingCardTradeState::enter_state() noexcept
{
    trade_cards = std::vector<std::shared_ptr<BuildingCard>>();
    Game::instance()->current_client->render_screen();
}

void BuildingCardTradeState::render(std::shared_ptr<ClientInfo>& client) noexcept
{
    client->send_header();

    if (client == Game::instance()->current_client)
    {
        const auto size = number_cards();
        client->get_socket()
            << "You can put away any number of cards, and take the same number of new cards from the table."
            << Socket::endl << Socket::endl << cards_view()
            << "(" << size + 1 << ") Reset" << Socket::endl
            << "(" << size + 2 << ") Confirm" << Socket::endl
            << "(" << size + 3 << ") Go Back" << Socket::endl << Socket::endl
            << Socket::prompt;
    }
    else
    {
        client->get_socket() << "Please wait for the player to play their turn." << Socket::endl;
    }
}

void BuildingCardTradeState::handle_input(std::shared_ptr<ClientCommand>& command) noexcept
{
    const auto game = Game::instance();
    const auto client = command->get_client_info().lock();

    if (client != game->current_client) return;

    int action = -1;
    try
    {
        action = atoi(command->get_cmd().c_str());
    }
    catch (...) {}
    action--;

    const auto size = number_cards();

    if (action == size)
    {
        trade_cards.clear();
        return;
    }

    if (action == size + 1)
    {
        cards_traded = static_cast<int>(trade_cards.size());

        if (cards_traded < 1) return;

        // Remove cards from player deck, give his the same amount of new random cards.

        for (const auto card : trade_cards)
        {
            // If it's not in there, skip.
            const auto it = std::find(client->get_player().building_cards.begin(), client->get_player().building_cards.end(), card);
            if (it == trade_cards.end()) continue;

            // Erase the card from the player's deck
            client->get_player().building_cards.erase(std::find(
                client->get_player().building_cards.begin(),
                client->get_player().building_cards.end(),
                card
            ));

            // Put the card back into the game deck
            game->building_deck.cards.push_back(card);
        }

        for (int i = 0; i < cards_traded; ++i)
        {
            const auto index = Random::instance().get(game->building_deck.cards.size() - 1);

            client->get_player().building_cards.push_back(game->building_deck.cards.at(index));
            game->building_deck.cards.erase(game->building_deck.cards.begin() + index);
        }

        client->get_player().get_turn().use_ability();

        client->set_message("You got " + std::to_string(cards_traded) + " new cards.");
        switch_state = true;
        return;
    }

    if (action == size + 2)
    {
        switch_state = true;
        return;
    }

    if (action < 0 || action >= size)
    {
        game->current_client->set_message("Invalid input, please try again.");
        return;
    }

    const auto card = client->get_player().building_cards.at(action);

    // Toggle card from trading vector

    const auto it = std::find(trade_cards.begin(), trade_cards.end(), card);
    if (it != trade_cards.end())
    {
        trade_cards.erase(it);
    }
    else
    {
        trade_cards.push_back(card);
    }
}

void BuildingCardTradeState::update() noexcept
{
    if (switch_state)
    {
        Game::instance()->exit_current_state(old_state);
    }
}

std::string BuildingCardTradeState::cards_view() const noexcept
{
    std::stringstream view;

    auto index = 1;
    for (const auto& card : Game::instance()->current_client->get_player().building_cards)
    {
        if (card->is_built) continue;

        view << "(" << index++ << ") " << card->to_string();

        if (std::find(trade_cards.begin(), trade_cards.end(), card) != trade_cards.end())
        {
            view << "  (Will trade)";
        }

        view << Socket::endl;
    }

    return view.str();
}

int BuildingCardTradeState::number_cards() const noexcept
{
    auto game = Game::instance();
    int size = 0;

    for (const auto& card : game->current_client->get_player().building_cards)
    {
        if (card->is_built) continue;

        size++;
    }

    return size;
}
