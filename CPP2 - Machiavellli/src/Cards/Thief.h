#ifndef MACHIAVELLI_THIEF_H
#define MACHIAVELLI_THIEF_H

#include "Base/CharacterCard.h"

class Thief : public CharacterCard
{
public:
    Thief();

    void ability_update() const noexcept override;
};

#endif //MACHIAVELLI_THIEF_H
