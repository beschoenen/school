#ifndef MACHIAVELLI_BUILDINGCARDTRADESTATE_H
#define MACHIAVELLI_BUILDINGCARDTRADESTATE_H

#include "../../Models/State.h"

class BuildingCardTradeState : public State
{
    int cards_traded = 0;

    std::vector<std::shared_ptr<BuildingCard>> trade_cards;

public:
    BuildingCardTradeState() noexcept;

    explicit BuildingCardTradeState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;

    std::string cards_view() const noexcept;

    int number_cards() const noexcept;
};

#endif //MACHIAVELLI_BUILDINGCARDTRADESTATE_H
