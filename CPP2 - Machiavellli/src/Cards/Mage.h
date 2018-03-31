#ifndef MACHIAVELLI_MAGE_H
#define MACHIAVELLI_MAGE_H

#include "Base/CharacterCard.h"

class Mage : public CharacterCard
{
public:
    Mage();

    void ability_update() const noexcept override;
};

#endif //MACHIAVELLI_MAGE_H
