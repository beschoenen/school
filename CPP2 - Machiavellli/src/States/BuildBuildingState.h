#ifndef MACHIAVELLI_BUILDBUILDINGSTATE_H
#define MACHIAVELLI_BUILDBUILDINGSTATE_H

#include "../Models/State.h"

class BuildBuildingState : public State
{
public:
    BuildBuildingState() noexcept;

    explicit BuildBuildingState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;

    static std::string cards_view() noexcept;
};

#endif //MACHIAVELLI_BUILDBUILDINGSTATE_H
