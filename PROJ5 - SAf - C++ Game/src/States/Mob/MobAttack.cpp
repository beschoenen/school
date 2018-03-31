#include "MobAttack.h"
#include "../../Utilities/Random.h"
#include "../../Components/Mob.h"
#include "MobWandering.h"
#include "../../Engine/Engine.h"

MobAttack::MobAttack() : State()
{
    sound_attack_sword = Engine::instance()->load_sound(SOUND_SWORD_ATTACK);
    sound_player_hit = Engine::instance()->load_sound(SOUND_PLAYER_HIT);
}

void MobAttack::update()
{
    if (entity.expired()) return;

    const std::shared_ptr<engine::Entity> mob_entity = entity.lock();
    std::shared_ptr<Mob> mob = std::dynamic_pointer_cast<Mob>(mob_entity);

    // Check range
    if (!mob->is_in_range())
    {
        mob->set_state(std::make_shared<MobWandering>());
        mob->set_entity(mob);
        return;
    }

    // Attack
    if (delay == 0)
    {
        sound_attack_sword.play_once();

        mob_entity->go_to_attacking_phase();
        delay = Random::instance().get(40, 100);

        mob->attack_player(mob->attack());

        sound_player_hit.play_once();
    }

    if (ticks <= delay)
    {
        ticks++;
        return;
    }

    delay = 0;
    ticks = 0;
}
