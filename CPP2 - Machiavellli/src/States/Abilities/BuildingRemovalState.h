#ifndef MACHIAVELLI_BUILDINGREMOVALSTATE_H
#define MACHIAVELLI_BUILDINGREMOVALSTATE_H

#include "../../Models/State.h"

class BuildingRemovalState : public State
{
    std::shared_ptr<ClientInfo> opponent;

    std::string building_name;

public:
    BuildingRemovalState() noexcept;

    explicit BuildingRemovalState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;

    std::string build_cards_view() const noexcept;
};

#endif //MACHIAVELLI_BUILDINGREMOVALSTATE_H
