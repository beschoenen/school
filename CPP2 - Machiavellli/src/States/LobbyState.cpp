#include "LobbyState.h"
#include "../Models/Game.h"
#include "GameInitState.h"

LobbyState::LobbyState() noexcept : LobbyState(nullptr)
{
}

LobbyState::LobbyState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void LobbyState::render(std::shared_ptr<ClientInfo>& client_info) noexcept
{
    std::stringstream message;

    message << Socket::clear << "You are currently in the lobby." << Socket::endl;

    if (Game::instance()->number_of_clients() < 2)
    {
        message << Socket::endl << "Please wait for the other player to connect." << Socket::endl;
    }

    client_info->get_socket() << message.str();
}

void LobbyState::update() noexcept
{
    const auto game = Game::instance();

    if (game->client1 == nullptr || game->client2 == nullptr) return;

    if (game->client1->get_player().is_complete() && game->client2->get_player().is_complete())
    {
        game->go_to_state<GameInitState>(true);
    }
}
