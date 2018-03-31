#ifndef GAME_TITLETEXT_H
#define GAME_TITLETEXT_H

#include "../../Engine/Components/Text.h"
#include "../../defines.h"

class TitleText : public engine::Text
{
public:
    TitleText(const std::string& value, int x = 0, int y = 50, Position anchor = POS_TOP_CENTER);
};

#endif //GAME_TITLETEXT_H
