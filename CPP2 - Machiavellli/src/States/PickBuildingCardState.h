#ifndef MACHIAVELLI_PICKBUILDINGSTATE_H
#define MACHIAVELLI_PICKBUILDINGSTATE_H

#include "../Cards/Base/BuildingCard.h"
#include "../Models/State.h"

class PickBuildingCardState : public State
{
    std::vector<std::shared_ptr<BuildingCard>> cards;
    std::shared_ptr<BuildingCard> selected_card;

public:
    PickBuildingCardState() noexcept;

    explicit PickBuildingCardState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void leave_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;

    std::string cards_view() const noexcept;

    void select_random_cards() noexcept;
};


#endif //MACHIAVELLI_PICKBUILDINGSTATE_H
