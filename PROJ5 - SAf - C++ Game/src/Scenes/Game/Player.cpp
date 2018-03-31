#include "../Game.h"
#include "../../Engine/Engine.h"
#include "../../Utilities/Random.h"

std::shared_ptr<Player> scenes::Game::get_player() const
{
    return player;
}

void scenes::Game::move_player()
{
    if (move_up)
    {
        player->set_direction("up");

        if (player->can_move_up())
        {
            map->y -= MOVE_DISTANCE;
            arrow->set_angle(map->x, map->y);

            for (const auto& enemy : enemies)
            {
                enemy->move_down();
            }
        }
    }

    if (move_down)
    {
        player->set_direction("down");

        if (player->can_move_down())
        {
            map->y += MOVE_DISTANCE;
            arrow->set_angle(map->x, map->y);

            for (const auto& enemy : enemies)
            {
                enemy->move_up();
            }
        }
    }

    if (move_left)
    {
        player->set_direction("left");

        if (player->can_move_left())
        {
            map->x -= MOVE_DISTANCE;
            arrow->set_angle(map->x, map->y);

            for (const auto& enemy : enemies)
            {
                enemy->move_right();
            }
        }
    }

    if (move_right)
    {
        player->set_direction("right");

        if (player->can_move_right())
        {
            map->x += MOVE_DISTANCE;
            arrow->set_angle(map->x, map->y);

            for (const auto& enemy : enemies)
            {
                enemy->move_left();
            }
        }
    }

    if (move_up || move_down || move_left || move_right)
    {
        sound_footstep.play_once();
    }

    hud->set_max_health(player->get_max_health_points());
    hud->set_health(player->get_health_points());
    hud->set_max_armor(player->get_max_armor_points());
    hud->set_armor(player->get_armor_points());
}

void scenes::Game::player_attack()
{
    std::vector<std::shared_ptr<level::GridTile>> player_current_tiles{player->get_current_tiles()};
    sound_attack_sword.play_once();

    for (auto& mob : enemies)
    {
        if (mob == nullptr) continue;

        if (!mob->will_be_visible()) continue;

        std::vector<std::shared_ptr<level::GridTile>> mob_current_tiles{mob->get_current_tiles()};

        bool hit = false;
        for (auto& m_tile : mob_current_tiles)
        {
            for (auto& p_tile : player_current_tiles)
            {
                if (m_tile == p_tile)
                {
                    hit = true;
                    break;
                }
            }

            if (hit) break;
        }

        if (hit)
        {
            const int damage = player->attack();
            attack_mob(mob, damage);
            score += ATTACK_DAMAGE_POINT;
            hud->set_score(score);
        }
    }
}

void scenes::Game::handle_action()
{
    std::shared_ptr<Item> item = can_pickup_item();
    if (item != nullptr)
    {
        pickup_item(item);
    }

    if (can_pickup_mission())
    {
        finish_mission();
    }

    if (round <= GAME_ROUNDS && !enemies.empty())
    {
        attack = true;
        if (player->go_to_attacking_phase())
        {
            ticker = 0;
        }
    }
    else if (round == GAME_ROUNDS && enemies.empty())
    {
        handle_going_through_portal();
    }
}

void scenes::Game::check_health()
{
    if (player->get_health_points() <= PLAYER_LOW_HEALTH && !low_health_sound_playing)
    {
        sound_ambience.stop();
        sound_low_health.play();
        low_health_sound_playing = true;
    }
    else if (player->get_health_points() > PLAYER_LOW_HEALTH && low_health_sound_playing)
    {
        sound_low_health.stop();
        sound_ambience.play();
        low_health_sound_playing = false;
    }
}
