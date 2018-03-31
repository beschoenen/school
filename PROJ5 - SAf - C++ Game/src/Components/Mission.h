#ifndef GAME_MISSION_H
#define GAME_MISSION_H

#include "../Engine/Components/Image.h"

class Mission : public engine::Image
{
public:
    Mission(std::string image, int x, int y, int width, int height);
};

#endif //GAME_MISSION_H
