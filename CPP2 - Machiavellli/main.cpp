#include <thread>
#include <iostream>
#include "src/Server/Socket.h"
#include "src/Server/ClientCommand.h"
#include "src/Server/ClientInfo.h"
#include "src/Server/Sync_queue.h"
#include "src/defines.h"
#include "src/Models/Game.h"

static bool running = true;

static Sync_queue<ClientCommand> queue;

void consume_command() // runs in its own thread
{
    try
    {
        while (running)
        {
            // will block here unless there are still command objects in the queue
            ClientCommand command{queue.get()};

            if (auto clientInfo = command.get_client_info().lock())
            {
                const auto game = Game::instance();
                const auto& client = clientInfo->get_socket();
                const auto& player = clientInfo->get_player();

                try
                {
                    auto cmd = std::make_shared<ClientCommand>(command);

                    game->handle_input(cmd);
                    game->render(clientInfo);
                }
                catch (const std::exception& ex)
                {
                    std::cerr << "*** exception in consumer thread for player "
                              << player.name << ": " << ex.what() << std::endl;

                    if (client.is_open())
                    {
                        client << "Sorry, something went wrong during handling of your request." << Socket::endl;
                    }
                }
                catch (...)
                {
                    std::cerr << "*** exception in consumer thread for player " << player.name << std::endl;

                    if (client.is_open())
                    {
                        client << "Sorry, something went wrong during handling of your request." << Socket::endl;
                    }
                }
            }

            std::this_thread::sleep_for(std::chrono::milliseconds(100));
        }
    }
    catch (...)
    {
        std::cerr << "consume_command crashed" << std::endl;
    }
}

std::shared_ptr<ClientInfo> init_client_session(Socket client)
{
    if (Game::instance()->is_running())
    {
        client << "The game is full!" << Socket::endl;
        return nullptr;
    }

    client << "What's your name?" << Socket::endl << Socket::prompt;

    std::string name;
    bool done = false;

    while (!done)
    {
        done = client.readline([&name](std::string input) { name = input; });
    }

    client << "How old are you?" << Socket::endl << Socket::prompt;

    int age = 0;
    done = false;

    while (!done || age <= 0)
    {
        done = client.readline([&age, &client](std::string input) {
            try
            {
                age = std::stoi(input);
            }
            catch (...)
            {
                std::cout << "Player provided wrong age input" << std::endl;
                client << Socket::prompt;
            };
        });
    }

    return std::make_shared<ClientInfo>(std::move(client), Player(name, age));
}

void handle_client(Socket client) // this function runs in a separate thread
{
    try
    {
        auto client_info = init_client_session(std::move(client));

        if (client_info == nullptr) return;

        const auto game = Game::instance();

        game->add_client(client_info);

        const auto& socket = client_info->get_socket();
        const auto& player = client_info->get_player();

        game->render(client_info);

        while (running)
        {
            try
            {
                std::string cmd;

                if (socket.readline([&cmd](std::string input) { cmd = input; }))
                {
                    std::cout << '[' << socket.get_dotted_ip()
                              << " (" << socket.get_id() << ") "
                              << player.name
                              << "] " << cmd << std::endl;

                    if (cmd == "quit" || cmd == "exit")
                    {
                        socket << "Bye!" << Socket::endl;
                        break;
                    }
                    else if (cmd == "quit_server")
                    {
                        running = false;
                        break;
                    }

                    const ClientCommand command{cmd, client_info};
                    queue.put(command);
                };

            }
            catch (const std::exception& ex)
            {
                socket << "ERROR: " << ex.what() << Socket::endl;
            }
            catch (...)
            {
                socket << "ERROR: something went wrong during handling of your request. Sorry!" << Socket::endl;
            }

            if (client_info->disconnect) break;

            if (client_info->render)
            {
                game->render(client_info);
                client_info->render = false;
            }

            std::this_thread::sleep_for(std::chrono::milliseconds(100));
        }

        game->remove_client(client_info);
    }
    catch (std::exception& ex)
    {
        std::cerr << "handle_client " << ex.what() << std::endl;
    }
    catch (...)
    {
        std::cerr << "handle_client crashed" << std::endl;
    }
}

void update_states()
{
    while (running)
    {
        try
        {
            Game::instance()->update();
        }
        catch (const std::runtime_error& ex)
        {
            std::cerr << "Update thread failed" << std::endl << ex.what() << std::endl;
        }

        std::this_thread::sleep_for(std::chrono::milliseconds(100));
    }
}

int main(int argc, const char* argv[])
{
    // start command consumer thread
    std::vector<std::thread> threads;

    threads.emplace_back(consume_command);

    // create a server socket
    ServerSocket server(PORT);

    threads.emplace_back(update_states);

    try
    {
        std::cout << "Server listening" << std::endl;

        while (running)
        {
            // Wait for connection from client; will create new socket
            server.accept([&threads](Socket client) {
                std::cerr << "Connection accepted from " << client.get_dotted_ip() << std::endl;
                threads.emplace_back(handle_client, std::move(client));
            });

            std::this_thread::sleep_for(std::chrono::milliseconds(100));
        }
    }
    catch (const std::exception& ex)
    {
        std::cerr << ex.what() << ", resuming..." << std::endl;
    }
    catch (...)
    {
        std::cerr << "problems, problems, but: keep calm and carry on!" << std::endl;
    }

    for (auto& thread : threads)
    {
        thread.join();
    }

    return 0;
}

