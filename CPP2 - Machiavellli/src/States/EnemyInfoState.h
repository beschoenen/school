#ifndef MACHIAVELLI_ENEMYINFOSTATE_H
#define MACHIAVELLI_ENEMYINFOSTATE_H

#include "../Models/State.h"

class EnemyInfoState : public State
{
public:
    EnemyInfoState() noexcept;

    explicit EnemyInfoState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;
};

#endif //MACHIAVELLI_ENEMYINFOSTATE_H
