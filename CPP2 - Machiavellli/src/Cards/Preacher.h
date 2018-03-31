#ifndef MACHIAVELLI_PREACHER_H
#define MACHIAVELLI_PREACHER_H

#include "Base/CharacterCard.h"

class Preacher : public CharacterCard
{
public:
    Preacher();

    void ability() const noexcept override;

    void build_building(const std::shared_ptr<BuildingCard>& card) const noexcept override;
};

#endif //MACHIAVELLI_PREACHER_H
