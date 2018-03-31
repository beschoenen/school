#include "Mercenary.h"
#include "../Models/Game.h"
#include "../States/Abilities/BuildingRemovalState.h"

Mercenary::Mercenary() : CharacterCard("Mercenary", "red")
{
}

void Mercenary::ability_update() const noexcept
{
    Game::instance()->go_to_state<BuildingRemovalState>();
}
