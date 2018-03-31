#ifndef MACHIAVELLI_MERCENARY_H
#define MACHIAVELLI_MERCENARY_H

#include "Base/CharacterCard.h"

class Mercenary : public CharacterCard
{
public:
    Mercenary();

    void ability_update() const noexcept override;
};

#endif //MACHIAVELLI_MERCENARY_H
