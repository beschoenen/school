#ifndef MACHIAVELLI_ROUNDSTATE_H
#define MACHIAVELLI_ROUNDSTATE_H

#include "../Models/State.h"
#include "../Models/Game.h"

class RoundState : public State
{
    int current_order = 0;
    bool use_ability = false;

    // State switching booleans
    bool building_building = false;
    bool pick_building_card = false;

public:
    RoundState() noexcept;

    explicit RoundState(const std::shared_ptr<State>& old_state) noexcept;

    void enter_state() noexcept override;

    void render(std::shared_ptr<ClientInfo>& client) noexcept override;

    void handle_input(std::shared_ptr<ClientCommand>& command) noexcept override;

    void update() noexcept override;

    void determine_next_player() noexcept;

    bool check_player_card(const std::shared_ptr<ClientInfo> client, const std::shared_ptr<CharacterCard> card) const noexcept;

    static std::string round_header(const std::shared_ptr<ClientInfo> client) noexcept;

    void switch_card(const std::shared_ptr<ClientInfo>& client, const std::shared_ptr<CharacterCard>& card) const noexcept;
};

#endif //MACHIAVELLI_ROUNDSTATE_H
