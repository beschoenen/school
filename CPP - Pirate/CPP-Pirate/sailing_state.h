#pragma once
#include "state.h"

class sailing_state : public state
{
public:
    int turns_;

    explicit sailing_state(game* game);
    explicit sailing_state(game* game, state* old_state);
    ~sailing_state();

    str* render() override;
    void handle_input(const str* input) override;

    bool random_pirate() const;
    int random_wind();
};
