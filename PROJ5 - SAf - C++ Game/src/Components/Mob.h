#ifndef GAME_MOB_H
#define GAME_MOB_H

#include "../Engine/Base/Entity.h"
#include "Player.h"

class Mob : public engine::Entity
{
    std::shared_ptr<Player> player;
    std::shared_ptr<level::GridTile> player_location = nullptr;

public:
    Mob(std::shared_ptr<engine::Map> map, int x, int y, int max_health_points, int max_armor_points, int minimum_damage, int maximum_damage);

    void update() override;

    void update_following_position();

    void go_to_position(std::shared_ptr<level::GridTile>& shared_ptr);

    void set_player(std::shared_ptr<Player> player);

    std::shared_ptr<Player> get_player() const;

    void attack_player(int damage);

    bool is_in_range();
};


#endif //GAME_MOB_H
