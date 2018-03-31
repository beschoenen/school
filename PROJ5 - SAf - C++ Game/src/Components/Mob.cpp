#include "Mob.h"
#include "../States/Mob/MobFollowing.h"
#include <utility>

Mob::Mob(std::shared_ptr<engine::Map> map, int x, int y,
         int max_health_points, int max_armor_points,
         int minimum_damage, int maximum_damage) :
    Entity(std::move(map), MOB_IMAGE, x, y, TILE_SIZE, TILE_SIZE,
           max_health_points, max_health_points, 0, 0,
           minimum_damage, maximum_damage)
{
    add_phase("looking-left");
    add_frame("looking-left", {100, 0, 100, 100});
    add_frame("looking-left", {000, 0, 100, 100});

    add_phase("walking-left");
    add_frame("walking-left", {300, 0, 100, 100});
    add_frame("walking-left", {200, 0, 100, 100});

    add_phase("crying-left");
    add_frame("crying-left", {500, 0, 100, 100});
    add_frame("crying-left", {400, 0, 100, 100});

    add_phase("attacking-left");
    add_frame("attacking-left", {600, 000, 100, 100});
    add_frame("attacking-left", {705, 000, 100, 100});

    add_phase("looking-right");
    add_frame("looking-right", {100, 0, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("looking-right", {000, 0, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("walking-right");
    add_frame("walking-right", {300, 0, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("walking-right", {200, 0, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("crying-right");
    add_frame("crying-right", {500, 0, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("crying-right", {400, 0, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("attacking-right");
    add_frame("attacking-right", {600, 000, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("attacking-right", {705, 000, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("looking-up");
    add_frame("looking-up", {100, 100, 100, 100});
    add_frame("looking-up", {000, 100, 100, 100});

    add_phase("walking-up");
    add_frame("walking-up", {300, 100, 100, 100});
    add_frame("walking-up", {200, 100, 100, 100});

    add_phase("crying-up");
    add_frame("crying-up", {500, 100, 100, 100});
    add_frame("crying-up", {400, 100, 100, 100});

    add_phase("attacking-up");
    add_frame("attacking-up", {600, 100, 100, 100});
    add_frame("attacking-up", {100, 100, 100, 100});

    add_phase("looking-down");
    add_frame("looking-down", {100, 200, 100, 100});
    add_frame("looking-down", {000, 200, 100, 100});

    add_phase("walking-down");
    add_frame("walking-down", {300, 200, 100, 100});
    add_frame("walking-down", {200, 200, 100, 100});

    add_phase("crying-down");
    add_frame("crying-down", {500, 200, 100, 100});
    add_frame("crying-down", {400, 200, 100, 100});

    add_phase("attacking-down");
    add_frame("attacking-down", {600, 200, 100, 100});
    add_frame("attacking-down", {704, 200, 100, 100});

    go_to_phase("looking-down");
}

void Mob::update()
{
    update_following_position();
    Entity::update();
}

void Mob::update_following_position()
{
    const auto state = state_machine.get_current_state().get();
    const auto following_state = dynamic_cast<MobFollowing*>(state);

    if (following_state != nullptr)
    {
        clear_cached_tiles();
        following_state->set_start_point(get_current_tiles()[0]);
        following_state->set_end_point(player_location);
    }
}

void Mob::go_to_position(std::shared_ptr<level::GridTile>& shared_ptr)
{
    player_location = shared_ptr;
}

void Mob::set_player(std::shared_ptr<Player> player)
{
    Mob::player = std::move(player);
}

std::shared_ptr<Player> Mob::get_player() const
{
    return player;
}

void Mob::attack_player(int damage)
{
    player->take_damage(damage);
}

bool Mob::is_in_range()
{
    // Check range
    auto mob_current_tiles = get_current_tiles();
    auto player_current_tiles = get_player()->get_current_tiles();

    bool in_range = false;
    for (auto& m_tile : mob_current_tiles)
    {
        for (auto& p_tile : player_current_tiles)
        {
            if (m_tile == p_tile)
            {
                in_range = true;
                break;
            }
        }

        if (in_range) break;
    }

    return in_range;
}
