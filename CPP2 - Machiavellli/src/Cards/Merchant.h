#ifndef MACHIAVELLI_MERCHANT_H
#define MACHIAVELLI_MERCHANT_H

#include "Base/CharacterCard.h"

class Merchant : public CharacterCard
{
public:
    Merchant();

    void ability() const noexcept override;

    int bonus() const noexcept override;
};

#endif //MACHIAVELLI_MERCHANT_H
