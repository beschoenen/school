#ifndef GAME_HUD_H
#define GAME_HUD_H

#include "Game/Hud/HudLabel.h"
#include "Game/Hud/HudBar.h"
#include <memory>

class Hud
{
    std::shared_ptr<HudLabel> health_label;
    std::shared_ptr<HudBar> health_bar;

    std::shared_ptr<HudLabel> armor_label;
    std::shared_ptr<HudBar> armor_bar;

    std::shared_ptr<HudLabel> score_label;
    std::shared_ptr<HudLabel> score_value;

    std::shared_ptr<HudLabel> round_label;
    std::shared_ptr<HudLabel> round_number;

    std::shared_ptr<HudLabel> level_label;
    std::shared_ptr<HudLabel> level_number;

public:
    Hud();

    void draw();

    void erase();

    void set_max_health(int max);

    void set_health(int value);

    void set_max_armor(int max);

    void set_armor(int value);

    void set_score(int score);

    void set_round(int round);

    void set_level(int level);
};


#endif //GAME_HUD_H
