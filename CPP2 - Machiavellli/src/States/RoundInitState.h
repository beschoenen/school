#ifndef MACHIAVELLI_ROUNDINITSTATE_H
#define MACHIAVELLI_ROUNDINITSTATE_H

#include "../Models/State.h"
#include "../Models/Game.h"

class RoundInitState : public State
{
public:
    RoundInitState() noexcept;

    explicit RoundInitState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void leave_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;
};

#endif //MACHIAVELLI_ROUNDINITSTATE_H
