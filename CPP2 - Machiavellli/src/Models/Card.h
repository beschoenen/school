#ifndef MACHIAVELLI_CARD_H
#define MACHIAVELLI_CARD_H

#include <string>

class Card
{
public:
    virtual ~Card() = default;

private:
    virtual std::string to_string() const noexcept { return "<base card>"; };
};

#endif //MACHIAVELLI_CARD_H
