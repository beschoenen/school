#include "GameInitState.h"
#include "RoundInitState.h"
#include "../Utilities/Random.h"

GameInitState::GameInitState() noexcept : GameInitState(nullptr)
{
}

GameInitState::GameInitState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void GameInitState::enter_state() noexcept
{
    switch_state = false;

    const auto game = Game::instance();

    const std::string younger = "You are the younger player, the other player will begin.";
    const std::string older = "You are the older player, you will begin.";

    // Determine the oldest player
    if (game->client1->get_player().age >= game->client2->get_player().age)
    {
        game->current_client = game->client1;
        game->client1->set_message(older);
        game->client2->set_message(younger);
    }
    else
    {
        game->current_client = game->client2;
        game->client1->set_message(younger);
        game->client2->set_message(older);
    }

    // Give players basic gold and assign king
    game->current_client->get_player().is_king = true;
    game->client1->get_player().gold = 2;
    game->client2->get_player().gold = 2;

    // 4 Random building cards for player 1
    for (int i = 0; i < 4; ++i)
    {
        const auto index = Random::instance().get(game->building_deck.cards.size() - 1);

        game->client1->get_player().building_cards.push_back(game->building_deck.cards.at(index));
        game->building_deck.cards.erase(game->building_deck.cards.begin() + index);
    }

    // 4 Random building cards for player 2
    for (int i = 0; i < 4; ++i)
    {
        const auto index = Random::instance().get(game->building_deck.cards.size() - 1);

        game->client2->get_player().building_cards.push_back(game->building_deck.cards.at(index));
        game->building_deck.cards.erase(game->building_deck.cards.begin() + index);
    }

    switch_state = true;
}

void GameInitState::update() noexcept
{
    if (switch_state)
    {
        Game::instance()->go_to_state<RoundInitState>(true);
    }
}
