#include "GameOverState.h"
#include "../Models/Game.h"

GameOverState::GameOverState() noexcept : GameOverState(nullptr)
{
}

GameOverState::GameOverState(const std::shared_ptr<State>& old_state) noexcept : State(old_state)
{
}

void GameOverState::enter_state() noexcept
{
    auto game = Game::instance();

    client1_message = prepare_message(game->client1);
    client2_message = prepare_message(game->client2);

    game->client1->render_screen();
    game->client2->render_screen();
}

void GameOverState::render(std::shared_ptr<ClientInfo>& client) noexcept
{
    client->send_header();

    client->get_socket() << "The game ended." << Socket::endl << Socket::endl
                         << client_view(client) << Socket::endl
                         << "Enter exit to disconnect." << Socket::endl
                         << Socket::endl << Socket::prompt;
}

std::string GameOverState::client_view(std::shared_ptr<ClientInfo> client) noexcept
{
    auto game = Game::instance();

    return client == game->client1 ? client1_message : client2_message;
}

std::string GameOverState::prepare_message(const std::shared_ptr<ClientInfo> client) noexcept
{
    const auto game = Game::instance();
    const auto opponent = client == game->client1 ? game->client2 : game->client1;

    std::stringstream message;

    const int b_score = building_score(client);
    const int b_bonus = building_bonus(client);
    const int c_bonus = color_bonus(client);
    const int score = b_score + b_bonus + c_bonus;

    message << "You buildings have a score of: " << b_score << Socket::endl;

    if (b_bonus == 4)
    {
        message << Socket::endl << "You were the first player to have 8 buildings.";
        message << Socket::endl << "This gives you a bonus of: " << b_bonus << Socket::endl;
    }
    else if (b_bonus == 2)
    {
        message << Socket::endl << "You ended with 8 or more buildings.";
        message << Socket::endl << "This gives you a bonus of: " << b_bonus << Socket::endl;
    }

    if (c_bonus > 0)
    {
        message << Socket::endl << "You have at least one building for each color." << Socket::endl;
        message << "This gives you a bonus of: " << c_bonus << Socket::endl;
    }

    message << Socket::endl << "Your final score is: " << score << "!" << Socket::endl;

    const int o_b_score = building_score(opponent);
    const int o_b_bonus = building_bonus(opponent);
    const int o_c_bonus = color_bonus(opponent);
    const int o_score = o_b_score + o_b_bonus + o_c_bonus;

    message << Socket::endl << "Your opponent has a score of: " << o_score << Socket::endl;

    if (score > o_score)
    {
        message << Socket::endl << "You won the game!" << Socket::endl;
    }
    else if (score < o_score)
    {
        message << Socket::endl << "You lost the game!" << Socket::endl;
    }
    else
    {
        message << Socket::endl << "It's a tie!" << Socket::endl;

        if (b_score > o_b_score)
        {
            message << Socket::endl << "You won because you have more building points." << Socket::endl;
        }
        else if (b_score < o_b_score)
        {
            message << Socket::endl << "You lost because you have less building points." << Socket::endl;
        }
    }

    return message.str();
}

int GameOverState::building_score(const std::shared_ptr<ClientInfo> client) noexcept
{
    int score = 0;

    for (const auto& card : client->get_player().building_cards)
    {
        if (!card->is_built) continue;

        score += card->price;
    }

    return score;
}

int GameOverState::building_bonus(const std::shared_ptr<ClientInfo> client) noexcept
{
    if (Game::instance()->winner == client)
    {
        return 4;
    }

    return client->get_player().number_built_buildings() >= 8 ? 2 : 0;
}

int GameOverState::color_bonus(const std::shared_ptr<ClientInfo> client) noexcept
{
    bool red = false;
    bool blue = false;
    bool yellow = false;
    bool green = false;
    bool purple = false;

    for (const auto& card : client->get_player().building_cards)
    {
        if (!card->is_built) continue;

        if (card->color == "red")
        {
            red = true;
        }
        else if (card->color == "blue")
        {
            blue = true;
        }
        else if (card->color == "yellow")
        {
            yellow = true;
        }
        else if (card->color == "green")
        {
            green = true;
        }
        else if (card->color == "purple")
        {
            purple = true;
        }
    }

    return red && blue && yellow && green/* && purple;*/ ? 3 : 0;
}
