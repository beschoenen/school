#ifndef MACHIAVELLI_ABILITY_STEALSTATE_H
#define MACHIAVELLI_ABILITY_STEALSTATE_H

#include "../../Models/State.h"

class StealState : public State
{
    std::shared_ptr<CharacterCard> character;
    std::shared_ptr<ClientInfo> opponent;
    bool stole_from_opponent = false;

public:
    StealState() noexcept;

    explicit StealState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void leave_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;
};

#endif //MACHIAVELLI_ABILITY_STEALSTATE_H
