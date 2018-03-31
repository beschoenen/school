#include "Game.h"
#include <utility>
#include "../defines.h"
#include "../States/LobbyState.h"

static std::shared_ptr<Game> instance_;

std::shared_ptr<Game> Game::instance() noexcept
{
    if (instance_ == nullptr)
    {
        instance_ = std::shared_ptr<Game>(new Game());
    }

    return instance_;
}

Game::Game() noexcept
{
    go_to_state<LobbyState>();

    std::ifstream c_stream{CHARACTERCARD_FILE};
    c_stream >> character_deck;
    c_stream.close();

    std::ifstream b_stream{BUILDINGCARD_FILE};
    b_stream >> building_deck;
    b_stream.close();

    round = 0;
}

bool Game::is_running() const noexcept
{
    return client1 != nullptr && client2 != nullptr;
}

void Game::handle_input(std::shared_ptr<ClientCommand>& command) const noexcept
{
    if (current_state == nullptr || command == nullptr)
    {
        log(command->get_client_info().lock(), "State was empty when handling input");
        return;
    }

    log(command->get_client_info().lock(), "Handling input");
    current_state->handle_input(command);
}

void Game::render(std::shared_ptr<ClientInfo>& client_info) const noexcept
{
    if (current_state == nullptr || client_info == nullptr)
    {
        log(client_info, "State was empty when rendering screen");
        return;
    }

    log(client_info, "Rendering screen");
    current_state->render(client_info);
}

void Game::update() const noexcept
{
    if (current_state == nullptr) return;

    current_state->update();
}

void Game::log(const std::shared_ptr<ClientInfo> client, const std::string message) const noexcept
{
    std::stringstream stream;

    stream << "(" << typeid(current_state.get()).name() << ") " << client->get_player().name << ": " << message;

    std::cout << stream.str() << std::endl;
}

int Game::number_of_clients() const noexcept
{
    int number = 0;

    if (client1 != nullptr) number++;
    if (client2 != nullptr) number++;

    return number;
}

void Game::add_client(const std::shared_ptr<ClientInfo>& client) noexcept
{
    if (client1 == nullptr)
    {
        client1 = client;
        std::cout << "client 1 connected" << std::endl;
    }
    else if (client2 == nullptr)
    {
        client2 = client;
        std::cout << "client 2 connected" << std::endl;
    }
}

void Game::remove_client(const std::shared_ptr<ClientInfo>& client) noexcept
{
    std::string message = Socket::clear + Socket::endl + "Your opponent disconnected, the game ended." + Socket::endl;

    if (current_client == client)
    {
        current_client = nullptr;
    }

    if (client1 != nullptr && client == client1)
    {
        client1 = nullptr;
        std::cout << "client 1 disconnected" << std::endl;

        if (client2 != nullptr)
        {
            client2->get_socket() << message;
            client2->disconnect = true;
        }
    }
    else if (client2 != nullptr && client == client2)
    {
        client2 = nullptr;
        std::cout << "client 2 disconnected" << std::endl;

        if (client1 != nullptr)
        {
            client1->get_socket() << message;
            client1->disconnect = true;
        }
    }

    if (client1 == nullptr && client2 == nullptr)
    {
        instance_ = nullptr;
    }
}

void Game::exit_current_state(const std::shared_ptr<State> old_state) noexcept
{
    if (old_state != nullptr)
    {
        current_state->leave_state();

        current_state.reset();
        current_state = old_state;

        current_state->enter_state();
    }
}

void Game::change_player() noexcept
{
    current_client = current_client == client1 ? client2 : client1;

    current_client->get_player().start_turn();
    current_client->set_message("It is now your turn.");
    current_client->render_screen();
}

void Game::set_player(const std::shared_ptr<ClientInfo> client) noexcept
{
    if (current_client != client)
    {
        current_client = client;
    }

    current_client->get_player().start_turn();
    current_client->render_screen();
}

void Game::set_king(const std::shared_ptr<ClientInfo> client) const noexcept
{
    client1->get_player().is_king = false;
    client2->get_player().is_king = false;

    client->get_player().is_king = true;
}
