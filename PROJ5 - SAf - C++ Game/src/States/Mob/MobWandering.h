#ifndef GAME_MOBWANDERING_H
#define GAME_MOBWANDERING_H

#include "../../Engine/Base/State.h"

#define WALKAREA 4

class MobWandering : public engine::State
{
    int start_x;
    int start_y;

    int direction = 0;
    int ticks = 0;
    int delay = 0;

public:
    explicit MobWandering();

    void set_entity(std::shared_ptr<engine::Entity> entity) override;

    void update() override;
};

#endif //GAME_MOBWANDERING_H
