#include "logger.h"

#define LENGTH 10

namespace kmint
{
    std::vector<std::string> logger::_messages;

    void logger::log(const std::string message)
    {
        if (_messages.size() == LENGTH)
        {
            _messages.erase(_messages.begin() + LENGTH - 1);
        }

        _messages.insert(_messages.begin(), std::move(message));
    }

    std::vector<std::string> logger::all()
    {
        return _messages;
    }
}
