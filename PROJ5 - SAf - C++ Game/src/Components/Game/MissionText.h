#ifndef GAME_MISSIONTEXT_H
#define GAME_MISSIONTEXT_H


#include "../../Engine/Components/Text.h"

class MissionText : public engine::Text
{
public:
    explicit MissionText(std::string text);
};


#endif //GAME_MISSIONTEXT_H
