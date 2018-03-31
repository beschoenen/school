#include "Architect.h"
#include "../Models/Game.h"

Architect::Architect() : CharacterCard("Architect")
{
    number_buildable_buildings = 3;
    pickable_buildings = 2;
}

void Architect::ability() const noexcept
{
    Game::instance()->current_client->set_message(
        "Info: The Architect will allow you to pick and build multiple buildings."
    );
}

bool Architect::can_build_buildings(const int number_built) const noexcept
{
    return true;
}
