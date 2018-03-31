#include "CharacterCard.h"
#include "../../Models/Game.h"
#include <utility>

CharacterCard::CharacterCard(const std::string name) : CharacterCard(name, "grey")
{
}

CharacterCard::CharacterCard(const std::string name, const std::string color) : name(name), color(color)
{
}

bool CharacterCard::operator==(const CharacterCard right) const noexcept
{
    return order == right.order;
}

void CharacterCard::build_building(const std::shared_ptr<BuildingCard>& card) const noexcept
{
    card->is_built = true;
}

int CharacterCard::bonus() const noexcept
{
    if (color == "grey") return 0;

    int bonus = 0;

    for (const auto& card : Game::instance()->current_client->get_player().building_cards)
    {
        if (!card->is_built || card->color != color) continue;

        bonus++;
    }

    return bonus;
}

bool CharacterCard::can_build_buildings(const int number_built) const noexcept
{
    return number_built < 8;
}
