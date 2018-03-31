#pragma once
#include "state.h"
#include "journey.h"

class destination_state : public state
{
    journey* journeys_[24]{nullptr};
public:
    explicit destination_state(game* game, state* old_state);
    explicit destination_state(game* game);
    ~destination_state();

    str* render() override;
    void handle_input(const str* input) override;
};
