#include <algorithm>
#include "MobFollowing.h"
#include "MobWandering.h"
#include "MobAttack.h"
#include "../../Engine/Engine.h"
#include "../../Engine/Navigation/AStar.h"

void MobFollowing::update()
{
    auto found_player = true;

    if (counter <= 0)
    {
        if (endpoint != nullptr)
        {
            directions = std::stack<Direction>();
            std::tie(directions, found_player) = engine::Astar::find_path(temp_startpoint, temp_endpoint);
            endpoint = nullptr;
        }

        const auto mob_entity = entity.lock();
        auto mob = std::dynamic_pointer_cast<Mob>(mob_entity);

        if (mob->is_in_range())
        {
            mob->set_state(std::make_shared<MobAttack>());
            mob->set_entity(mob);
            return;
        }

        if (!found_player)
        {
            mob->set_state(std::make_shared<MobWandering>());
            mob->set_entity(mob);
            return;
        }

        counter = TILE_SIZE / MOVE_DISTANCE;
    }

    if (counter > 0)
    {
        counter--;
    }

    if (!directions.empty())
    {
        auto ent = entity.lock();

        switch (directions.top())
        {
            case DIR_UP:
                ent->move_up();
                break;
            case DIR_UP_RIGHT:
                ent->move_up();
                ent->move_right();
                break;
            case DIR_RIGHT:
                ent->move_right();
                break;
            case DIR_DOWN_RIGHT:
                ent->move_down();
                ent->move_right();
                break;
            case DIR_DOWN:
                ent->move_down();
                break;
            case DIR_DOWN_LEFT:
                ent->move_down();
                ent->move_left();
                break;
            case DIR_LEFT:
                ent->move_left();
                break;
            case DIR_UP_LEFT:
                ent->move_up();
                ent->move_left();
                break;
            default:
                break;
        }

        ent->go_to_walking_phase();

        if (counter <= 0)
        {
            directions.pop();
        }
    }
}

void MobFollowing::set_start_point(std::shared_ptr<level::GridTile>& gridtile)
{
    temp_startpoint = gridtile;
    startpoint = std::make_shared<engine::Node>(gridtile->get_row(), gridtile->get_column(), gridtile->get_accessible());
}

void MobFollowing::set_end_point(std::shared_ptr<level::GridTile>& gridtile)
{
    temp_endpoint = gridtile;
    endpoint = std::make_shared<engine::Node>(gridtile->get_row(), gridtile->get_column(), gridtile->get_accessible());
}
