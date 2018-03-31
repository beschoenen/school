#ifndef GAME_PLAYERNULL_H
#define GAME_PLAYERNULL_H

#include "../../Engine/Base/State.h"

class PlayerNull : public engine::State
{
public:
    explicit PlayerNull();

    void update() override;
};

#endif //GAME_PLAYERNULL_H
