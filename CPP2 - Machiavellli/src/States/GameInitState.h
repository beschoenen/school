#ifndef MACHIAVELLI_GAMEINIT_H
#define MACHIAVELLI_GAMEINIT_H


#include "../Models/State.h"
#include "../Models/Game.h"

class GameInitState : public State
{
public:
    GameInitState() noexcept;

    explicit GameInitState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void update() noexcept override;
};


#endif //MACHIAVELLI_GAMEINIT_H
