#ifndef __socketexample__ClientCommand__
#define __socketexample__ClientCommand__

#include <string>
#include <memory>
#include <utility>

class ClientInfo;

class ClientCommand
{
    std::string cmd;
    std::weak_ptr<ClientInfo> info;

public:
    ClientCommand(std::string command_text, std::weak_ptr<ClientInfo> client_info) :
        cmd{std::move(command_text)}, info{std::move(client_info)}
    {
    }

    std::string get_cmd() const { return cmd; }

    std::weak_ptr<ClientInfo> get_client_info() const { return info; }
};

#endif //__socketexample__ClientCommand__
