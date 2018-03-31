#include "Mage.h"
#include "../Models/Game.h"
#include "../States/Abilities/BuildingCardSwapState.h"

Mage::Mage() : CharacterCard("Mage")
{
}

void Mage::ability_update() const noexcept
{
    Game::instance()->go_to_state<BuildingCardSwapState>();
}
