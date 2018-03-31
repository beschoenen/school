#include "Assassin.h"
#include "../Models/Game.h"
#include "../States/Abilities/MurderState.h"

Assassin::Assassin() : CharacterCard("Assassin")
{
}

void Assassin::ability_update() const noexcept
{
    Game::instance()->go_to_state<MurderState>();
}
