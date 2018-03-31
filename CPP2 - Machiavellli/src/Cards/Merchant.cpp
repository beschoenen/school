#include "Merchant.h"
#include "../Models/Game.h"

Merchant::Merchant() : CharacterCard("Merchant", "green")
{
}

void Merchant::ability() const noexcept
{
    Game::instance()->current_client->set_message(
        "Info: The merchant will receive a piece of gold when it's his turn."
    );
}

int Merchant::bonus() const noexcept
{
    return CharacterCard::bonus() + 1;
}
