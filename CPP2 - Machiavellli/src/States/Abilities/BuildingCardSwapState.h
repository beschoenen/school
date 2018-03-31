#ifndef MACHIAVELLI_BUILDINGCARDSWAPSTATE_H
#define MACHIAVELLI_BUILDINGCARDSWAPSTATE_H

#include "../../Models/State.h"

class BuildingCardSwapState : public State
{
    std::shared_ptr<ClientInfo> opponent;

    bool trade_cards = false;

public:
    BuildingCardSwapState() noexcept;

    explicit BuildingCardSwapState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;
};

#endif //MACHIAVELLI_BUILDINGCARDSWAPSTATE_H
