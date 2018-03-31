#ifndef MACHIAVELLI_BUILDINGCARD_H
#define MACHIAVELLI_BUILDINGCARD_H

#include <string>
#include "../../Models/Card.h"

class BuildingCard : public Card
{
public:
    const std::string name;
    const int price;
    const std::string color;
    const std::string ability;

    // When built by preacher
    bool protected_card = false;
    bool is_built = false;

    BuildingCard(std::string name, int price, std::string color, std::string ability);

    std::string to_string() const noexcept override;
};

#endif //MACHIAVELLI_BUILDINGCARD_H
