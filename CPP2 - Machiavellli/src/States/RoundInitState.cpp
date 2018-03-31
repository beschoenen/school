#include <random>
#include "RoundInitState.h"
#include "../Utilities/Random.h"
#include "RoundState.h"

RoundInitState::RoundInitState() noexcept : RoundInitState(nullptr)
{
}

RoundInitState::RoundInitState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void RoundInitState::enter_state() noexcept
{
    const auto game = Game::instance();

    // Reset cards
    game->current_card = nullptr;
    game->character_deck.reset();

    game->client1->get_player().character_cards.clear();
    game->client2->get_player().character_cards.clear();

    // Shuffle deck
    std::random_device rd;
    std::mt19937 g(rd());

    std::shuffle(game->character_deck.cards.begin(), game->character_deck.cards.end(), g);

    // Assign king
    game->current_client = game->client1->get_player().is_king ? game->client1 : game->client2;

    if (game->round > 0)
    {
        game->client1->set_message("New round.");
        game->client2->set_message("New round.");

        game->current_client->set_message("You were the king last round, you may pick a card first.");
    }

    // Render screens for players
    game->client1->render_screen();
    game->client2->render_screen();
}

void RoundInitState::leave_state() noexcept
{
    const auto game = Game::instance();

    game->round++;

    game->client1->set_message("Round " + std::to_string(game->round));
    game->client2->set_message("Round " + std::to_string(game->round));
}

void RoundInitState::render(std::shared_ptr<ClientInfo>& client) noexcept
{
    client->send_header();

    if (client == Game::instance()->current_client)
    {
        client->get_socket() << "Please pick a card:" << Socket::endl
                             << Game::instance()->character_deck.to_string()
                             << Socket::endl << Socket::prompt;
    }
    else
    {
        client->get_socket() << "Please wait for the player to pick a card." << Socket::endl;
    }
}

void RoundInitState::handle_input(std::shared_ptr<ClientCommand>& command) noexcept
{
    const auto game = Game::instance();
    const auto client = command->get_client_info().lock();

    if (client != game->current_client) return;

    int card = -1;
    try
    {
        card = atoi(command->get_cmd().c_str());
    }
    catch (...) {}

    card--;

    if (card < 0 || card > game->character_deck.cards.size() - 1)
    {
        client->set_message("Invalid input, please try again.");
        return;
    }

    const auto character = game->character_deck.cards.at(card);
    client->get_player().character_cards.push_back(character);
    game->character_deck.cards.erase(std::remove(game->character_deck.cards.begin(), game->character_deck.cards.end(), character));

    if (character->name == "King")
    {
        game->set_king(client);
    }

    const int index = Random::instance().get(game->character_deck.cards.size() - 1);
    game->character_deck.cards.erase(game->character_deck.cards.begin() + index);

    switch_state = game->character_deck.cards.empty();

    if (!switch_state)
    {
        game->change_player();
    }
}

void RoundInitState::update() noexcept
{
    if (switch_state)
    {
        Game::instance()->go_to_state<RoundState>(true);
    }
}
