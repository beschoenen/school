#include "MobWandering.h"
#include "../../Utilities/Random.h"
#include "../../Components/Mob.h"
#include "MobAttack.h"
#include "../../Engine/Navigation/AStar.h"
#include "MobFollowing.h"

#define TICKS 10

MobWandering::MobWandering() : State()
{
}

void MobWandering::set_entity(std::shared_ptr<engine::Entity> entity)
{
    State::entity = entity;
    start_x = entity->get_x();
    start_y = entity->get_y();
}

void MobWandering::update()
{
    if (entity.expired()) return;

    const std::shared_ptr<engine::Entity> mob_entity = entity.lock();
    std::shared_ptr<Mob> mob = std::dynamic_pointer_cast<Mob>(mob_entity);

    if (!mob_entity->will_be_visible()) return;

    if (mob->is_in_range())
    {
        mob->set_state(std::make_shared<MobAttack>());
        mob->set_entity(mob);
        return;
    }

    auto astar = engine::Astar::find_path(mob->get_current_tiles()[0], mob->get_player()->get_current_tiles()[0]);
    if (std::get<bool>(astar))
    {
        mob->set_state(std::make_shared<MobFollowing>());
        mob->set_entity(mob);
        return;
    }

    if (direction < 1)
    {
        if (delay == 0)
        {
            mob_entity->go_to_looking_phase();
            delay = Random::instance().get(40, 100);
        }

        if (ticks <= delay)
        {
            ticks++;
            return;
        }

        delay = 0;
        ticks = 0;
        direction = Random::instance().get(1, 40);
    }

    if (direction > 0)
    {
        if (direction <= 10 && mob_entity->can_move_up())
        {
            mob_entity->move_up();
        }
        else if (direction <= 20 && mob_entity->can_move_right())
        {
            mob_entity->move_right();
        }
        else if (direction <= 30 && mob_entity->can_move_down())
        {
            mob_entity->move_down();
        }
        else if (mob_entity->can_move_left())
        {
            mob_entity->move_left();
        }

        ticks++;
        mob_entity->go_to_walking_phase();
        mob_entity->clear_cached_tiles();
    }

    if (ticks == TICKS)
    {
        direction = 0;
        ticks = 0;
    }
}
