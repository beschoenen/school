#ifndef _CLIENT_INFO_H
#define _CLIENT_INFO_H

#include <string>
#include <utility>
#include <memory>
#include "Socket.h"
#include "../Models/Player.h"

class ClientInfo
{
    Socket _socket{};
    Player _player{};

    std::string ui_message;

public:
    ClientInfo(Socket socket, const Player& player) : _socket{std::move(socket)}, _player{std::move(player)}
    {
    }

    bool render = false;
    bool disconnect = false;

    Socket& get_socket() { return _socket; }

    const Socket& get_socket() const { return _socket; }

    Player& get_player() { return _player; }

    const Player& get_player() const { return _player; }

    const std::string get_message() { return ui_message; }

    const void set_message(const std::string& message) { ui_message += std::move(message) + Socket::endl; }

    const void clear_message() { ui_message = ""; }

    const bool has_message() const { return !ui_message.empty(); }

    const void send_header()
    {
        get_socket() << Socket::clear << Socket::endl
                     << "#############################" << Socket::endl
                     << "# " << get_player().gold << " Gold "
                     << get_player().number_built_buildings() << " Buildings" << Socket::endl
                     << "#############################" << Socket::endl << Socket::endl;

        if (has_message())
        {
            get_socket() << get_message() << Socket::endl;
            clear_message();
        }
    }

    const void render_screen()
    {
        render = true;
    }

    bool operator==(ClientInfo& client)
    {
        return this->get_socket().get_id() == client.get_socket().get_id();
    }

    bool operator!=(ClientInfo& client)
    {
        return !operator==(client);
    }

    bool operator==(std::shared_ptr<ClientInfo> client)
    {
        return operator==(*client);
    }

    bool operator!=(std::shared_ptr<ClientInfo> client)
    {
        return !operator==(client);
    }
};

#endif
