#ifndef MACHIAVELLI_CHARACTERCARD_H
#define MACHIAVELLI_CHARACTERCARD_H

class Player;

#include <string>
#include <memory>
#include "../../Models/Card.h"
#include "../../Models/Player.h"

class CharacterCard : public Card
{
protected:
    explicit CharacterCard(std::string name);
    explicit CharacterCard(std::string name, std::string color);

public:
    bool robbed = false;
    bool killed = false;

    int number_buildable_buildings = 1;
    int pickable_buildings = 1;

    const std::string name;
    const std::string color;
    int order;

    virtual void ability() const noexcept {}

    virtual void ability_update() const noexcept {}

    bool operator==(CharacterCard right) const noexcept;
    
    virtual int bonus() const noexcept;

    virtual void build_building(const std::shared_ptr<BuildingCard>& card) const noexcept;

    virtual bool can_build_buildings(int number_built) const noexcept;
};

#endif //MACHIAVELLI_CHARACTERCARD_H
