#ifndef GAME_MOBATTACK_H
#define GAME_MOBATTACK_H

#include "../../Engine/Base/State.h"
#include "../../Engine/Models/Sound.h"

class MobAttack : public engine::State
{
    engine::Sound sound_attack_sword = engine::Sound(nullptr);
    engine::Sound sound_player_hit = engine::Sound(nullptr);

    int ticks = 0;
    int delay = 12;

public:
    explicit MobAttack();

    void update() override;
};

#endif //GAME_MOBATTACK_H
