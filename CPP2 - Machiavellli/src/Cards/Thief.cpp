#include "Thief.h"
#include "../Models/Game.h"
#include "../States/Abilities/StealState.h"

Thief::Thief() : CharacterCard("Thief")
{
}

void Thief::ability_update() const noexcept
{
    Game::instance()->go_to_state<StealState>();
}
