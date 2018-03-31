#ifndef GAME_PLAYER_H
#define GAME_PLAYER_H

#include "../Engine/Base/Entity.h"

class Player : public engine::Entity
{
public:
    Player(const std::shared_ptr<engine::Map> map,
           int max_health_points, int health_points,
           int minimum_damage, int maximum_damage);

    void update() override;

    bool is_in_camp();
};


#endif //GAME_PLAYER_H
