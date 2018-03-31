#ifndef MACHIAVELLI_ASSASSIN_H
#define MACHIAVELLI_ASSASSIN_H

#include "Base/CharacterCard.h"

class Assassin : public CharacterCard
{
public:
    Assassin();

    void ability_update() const noexcept override;
};

#endif //MACHIAVELLI_ASSASSIN_H
