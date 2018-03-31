#ifndef MACHIAVELLI_LOBBY_STATE_H
#define MACHIAVELLI_LOBBY_STATE_H

#include "../Models/State.h"

class LobbyState : public State
{
public:
    LobbyState() noexcept;

    explicit LobbyState(const std::shared_ptr<State>& old_state) noexcept;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void update() noexcept override;
};

#endif //MACHIAVELLI_LOBBY_STATE_H
