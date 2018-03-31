#ifndef GAME_HUDLABEL_H
#define GAME_HUDLABEL_H

#include "../../../Engine/Components/Text.h"

class HudLabel : public engine::Text
{
public:
    HudLabel(const std::string& string, int x, int y, Position anchor = POS_TOP_LEFT);
};


#endif //GAME_HUDLABEL_H
