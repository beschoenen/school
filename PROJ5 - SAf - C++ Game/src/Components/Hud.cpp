#include "Hud.h"
#include "../Engine/Engine.h"

Hud::Hud()
{
    health_label = std::shared_ptr<HudLabel>(new HudLabel("Health", 10, 10));
    health_bar = std::shared_ptr<HudBar>(new HudBar(10, 35, {57, 181, 74}, 100));;

    armor_label = std::shared_ptr<HudLabel>(new HudLabel("Armor", 180, 10));
    armor_bar = std::shared_ptr<HudBar>(new HudBar(180, 35, {29, 120, 222}, 100));

    level_label = std::shared_ptr<HudLabel>(new HudLabel("Level", 365, 10));
    level_number = std::shared_ptr<HudLabel>(new HudLabel("1", 365, 35));

    score_label = std::shared_ptr<HudLabel>(new HudLabel("Score", -10, 10, POS_TOP_RIGHT));
    score_value = std::shared_ptr<HudLabel>(new HudLabel("0", -10, 35, POS_TOP_RIGHT));

    round_label = std::shared_ptr<HudLabel>(new HudLabel("Round", 500, 10));
    round_number = std::shared_ptr<HudLabel>(new HudLabel("1 of " + std::to_string(GAME_ROUNDS), 500, 35));
}

void Hud::draw()
{
    Engine::instance()->draw(health_label);
    Engine::instance()->draw(health_bar);

    Engine::instance()->draw(armor_label);
    Engine::instance()->draw(armor_bar);

    Engine::instance()->draw(score_label);
    Engine::instance()->draw(score_value);

    Engine::instance()->draw(round_label);
    Engine::instance()->draw(round_number);

    Engine::instance()->draw(level_label);
    Engine::instance()->draw(level_number);
}

void Hud::erase()
{
    Engine::instance()->erase(health_label);
    Engine::instance()->erase(health_bar);

    Engine::instance()->erase(armor_label);
    Engine::instance()->erase(armor_bar);

    Engine::instance()->erase(score_label);
    Engine::instance()->erase(score_value);

    Engine::instance()->erase(round_label);
    Engine::instance()->erase(round_number);

    Engine::instance()->erase(level_label);
    Engine::instance()->erase(level_number);
}

void Hud::set_max_health(int max)
{
    health_bar->set_max_value(max);
}

void Hud::set_health(int value)
{
    health_bar->set_value(value);
}

void Hud::set_max_armor(int max)
{
    armor_bar->set_max_value(max);
}

void Hud::set_armor(int value)
{
    armor_bar->set_value(value);
}

void Hud::set_score(int score)
{
    score_value->set_text(std::to_string(score));
}

void Hud::set_round(int round)
{
    round_number->set_text(std::to_string(round) + " of " + std::to_string(GAME_ROUNDS));
}

void Hud::set_level(int level)
{
    level_number->set_text(std::to_string(level));
}
