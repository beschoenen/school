#ifndef MACHIAVELLI_ABILITY_MURDERSTATE_H
#define MACHIAVELLI_ABILITY_MURDERSTATE_H

#include "../../Models/State.h"
#include "../../Cards/Base/CharacterCard.h"

class MurderState : public State
{
    std::shared_ptr<CharacterCard> character;
    std::shared_ptr<ClientInfo> opponent;
    bool killed_opponent_card = false;

public:
    MurderState() noexcept;

    explicit MurderState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;
    
    void leave_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;
};

#endif //MACHIAVELLI_ABILITY_MURDERSTATE_H
