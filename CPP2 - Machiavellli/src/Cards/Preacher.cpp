#include "Preacher.h"
#include "../Models/Game.h"

Preacher::Preacher() : CharacterCard("Preacher", "blue")
{
}

void Preacher::ability() const noexcept
{
    Game::instance()->current_client->set_message(
        "Info: The preacher will help you protect your buildings."
    );
}

void Preacher::build_building(const std::shared_ptr<BuildingCard>& card) const noexcept
{
    CharacterCard::build_building(card);
    card->protected_card = true;
}
