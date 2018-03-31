#ifndef KMINT_LOGGER_H
#define KMINT_LOGGER_H

#include <string>
#include <vector>

namespace kmint
{
    class logger
    {
        static std::vector<std::string> _messages;

    public:
        static void log(std::string message);

        static std::vector<std::string> all();
    };
}


#endif //KMINT_LOGGER_H
