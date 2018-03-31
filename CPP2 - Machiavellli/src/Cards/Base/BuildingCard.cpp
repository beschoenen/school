#include "BuildingCard.h"

BuildingCard::BuildingCard(const std::string name, const int price, const std::string color, const std::string ability) :
    name(name), price(price), color(color), ability(ability)
{
}

std::string BuildingCard::to_string() const noexcept
{
    return name + " (" + color + ", " + std::to_string(price) + ")";
}
