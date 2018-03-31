#ifndef GAME_BIGTEXT_H
#define GAME_BIGTEXT_H

#include "../../Engine/Components/Button.h"

class BigText : public engine::Text
{
public:
    BigText(const std::string& value, int x, int y, Position anchor);
};

#endif //GAME_BIGTEXT_H
