#include "EnemyInfoState.h"
#include "../Models/Game.h"

EnemyInfoState::EnemyInfoState() noexcept : EnemyInfoState(nullptr)
{
}

EnemyInfoState::EnemyInfoState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void EnemyInfoState::enter_state() noexcept
{
    switch_state = false;
    Game::instance()->current_client->render_screen();
}

void EnemyInfoState::render(std::shared_ptr<ClientInfo>& client) noexcept
{
    client->send_header();

    const auto game = Game::instance();
    if (client == game->current_client)
    {
        const auto opponent = client == game->client1 ? game->client2 : game->client1;

        client->get_socket() << "Your opponent has:" << Socket::endl << Socket::endl
                             << opponent->get_player().gold << " Gold." << Socket::endl
                             << opponent->get_player().building_cards.size() - opponent->get_player().number_built_buildings()
                             << " Building cards." << Socket::endl << Socket::endl
                             << opponent->get_player().building_cards_view() << Socket::endl << Socket::endl
                             << "Press any key to return" << Socket::endl;
    }
    else
    {
        client->get_socket() << "Please wait for the player to play their turn." << Socket::endl;
    }
}

void EnemyInfoState::handle_input(std::shared_ptr<ClientCommand>& command) noexcept
{
    const auto client = command->get_client_info().lock();

    if (client != Game::instance()->current_client) return;

    switch_state = true;
}

void EnemyInfoState::update() noexcept
{
    if (switch_state)
    {
        Game::instance()->exit_current_state(old_state);
    }
}
