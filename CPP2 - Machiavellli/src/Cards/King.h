#ifndef MACHIAVELLI_KING_H
#define MACHIAVELLI_KING_H

#include "Base/CharacterCard.h"

class King : public CharacterCard
{
public:
    King();

    void ability() const noexcept override;
};

#endif //MACHIAVELLI_KING_H
