#include "Player.h"

Player::Player(const std::shared_ptr<engine::Map> map,
               int max_health_points, int health_points,
               int minimum_damage, int maximum_damage) :
    engine::Entity(map, PLAYER_IMAGE, 0, 0, TILE_SIZE, TILE_SIZE,
                   max_health_points, health_points,
                   100, 0,
                   minimum_damage, maximum_damage)
{
    add_phase("looking-left");
    add_frame("looking-left", {000, 0, 100, 100});
    add_frame("looking-left", {100, 0, 100, 100});

    add_phase("walking-left");
    add_frame("walking-left", {200, 0, 100, 100});
    add_frame("walking-left", {300, 0, 100, 100});

    add_phase("crying-left");
    add_frame("crying-left", {400, 0, 100, 100});
    add_frame("crying-left", {500, 0, 100, 100});

    add_phase("attacking-left");
    add_frame("attacking-left", {600, 000, 100, 100});
    add_frame("attacking-left", {705, 000, 100, 100});

    add_phase("looking-right");
    add_frame("looking-right", {000, 0, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("looking-right", {100, 0, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("walking-right");
    add_frame("walking-right", {200, 0, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("walking-right", {300, 0, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("crying-right");
    add_frame("crying-right", {400, 0, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("crying-right", {500, 0, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("attacking-right");
    add_frame("attacking-right", {600, 000, 100, 100, SDL_FLIP_HORIZONTAL});
    add_frame("attacking-right", {705, 000, 100, 100, SDL_FLIP_HORIZONTAL});

    add_phase("looking-up");
    add_frame("looking-up", {000, 100, 100, 100});
    add_frame("looking-up", {100, 100, 100, 100});

    add_phase("walking-up");
    add_frame("walking-up", {200, 100, 100, 100});
    add_frame("walking-up", {300, 100, 100, 100});

    add_phase("crying-up");
    add_frame("crying-up", {400, 100, 100, 100});
    add_frame("crying-up", {500, 100, 100, 100});

    add_phase("attacking-up");
    add_frame("attacking-up", {600, 100, 100, 100});
    add_frame("attacking-up", {100, 100, 100, 100});

    add_phase("looking-down");
    add_frame("looking-down", {000, 200, 100, 100});
    add_frame("looking-down", {100, 200, 100, 100});

    add_phase("walking-down");
    add_frame("walking-down", {200, 200, 100, 100});
    add_frame("walking-down", {300, 200, 100, 100});

    add_phase("crying-down");
    add_frame("crying-down", {400, 200, 100, 100});
    add_frame("crying-down", {500, 200, 100, 100});

    add_phase("attacking-down");
    add_frame("attacking-down", {600, 200, 100, 100});
    add_frame("attacking-down", {704, 200, 100, 100});

    go_to_phase("looking-down");
}

void Player::update()
{
    engine::Entity::update();
    clear_cached_tiles();
}

bool Player::is_in_camp()
{
    auto current_tiles = get_current_tiles();

    bool player_is_in_camp = true;

    for (const auto& current_tile : current_tiles)
    {
        if (current_tile->get_grid_tile_type() != GridTileTypes::camp &&
            current_tile->get_grid_tile_type() != GridTileTypes::portal)
        {
            player_is_in_camp = false;
            break;
        }
    }

    return player_is_in_camp;
}
