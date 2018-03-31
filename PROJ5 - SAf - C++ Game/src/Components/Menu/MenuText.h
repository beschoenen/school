#ifndef GAME_MENUTEXT_H
#define GAME_MENUTEXT_H

#include "../../Engine/Components/Text.h"

class MenuText : public engine::Text
{
public:
    MenuText(const std::string& value, int x, int y, Position anchor = POS_TOP_LEFT);
};

#endif //GAME_MENUTEXT_H
