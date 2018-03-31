#ifndef MACHIAVELLI_ARCHITECT_H
#define MACHIAVELLI_ARCHITECT_H

#include "Base/CharacterCard.h"

class Architect : public CharacterCard
{
public:
    Architect();

    void ability() const noexcept override;

    bool can_build_buildings(int number_built) const noexcept override;
};

#endif //MACHIAVELLI_ARCHITECT_H
