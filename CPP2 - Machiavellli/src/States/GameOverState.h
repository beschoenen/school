#ifndef MACHIAVELLI_GAMEOVERSTATE_H
#define MACHIAVELLI_GAMEOVERSTATE_H


#include "../Models/State.h"

class GameOverState : public State
{
    std::string client1_message;
    std::string client2_message;

public:
    GameOverState() noexcept;

    explicit GameOverState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;
    
    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    std::string client_view(std::shared_ptr<ClientInfo> client) noexcept;

    std::string prepare_message(std::shared_ptr<ClientInfo> client) noexcept;

    static int building_score(std::shared_ptr<ClientInfo> client) noexcept;

    static int building_bonus(std::shared_ptr<ClientInfo> client) noexcept;

    static int color_bonus(std::shared_ptr<ClientInfo> client) noexcept;
};


#endif //MACHIAVELLI_GAMEOVERSTATE_H
