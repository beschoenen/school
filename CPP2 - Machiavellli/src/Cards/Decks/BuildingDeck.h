#ifndef MACHIAVELLI_BUILDINGDECK_H
#define MACHIAVELLI_BUILDINGDECK_H


#include <vector>
#include "../../Utilities/File/CsvReader.h"
#include "../Base/BuildingCard.h"
#include <memory>

class BuildingDeck : public CsvReader
{
    std::vector<std::shared_ptr<BuildingCard>> original_cards;

public:
    std::vector<std::shared_ptr<BuildingCard>> cards;

    void reset() noexcept;

    friend std::ifstream& operator>>(std::ifstream& ifstream, BuildingDeck& deck) noexcept;

    friend std::ostream& operator<<(std::ostream& ostream, const BuildingDeck& deck) noexcept;
};

#endif //MACHIAVELLI_BUILDINGDECK_H
