#pragma once
#include "state.h"
#include "ship.h"

class battle_state : public state
{
public:
    ship* pirate_ship;

    explicit battle_state(game* game);
    explicit battle_state(game* game, state* old_state);
    ~battle_state();

    str* render() override;
    void handle_input(const str* input) override;

    void get_random_pirate_ship();
    bool has_fled() const;
    void set_msg(str* message) const;
    void pirate_shoots();
    void player_shoots();
};
