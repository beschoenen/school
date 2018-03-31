#include "HighScores.h"
#include "../Engine/Engine.h"

scenes::HighScores::HighScores() : scenes::HighScores(nullptr)
{
}

scenes::HighScores::HighScores(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText("High Scores"));

    back_button = std::shared_ptr<MenuButton>(
        new MenuButton("Back", std::bind(&scenes::HighScores::back_button_clicked, this), 10, 0, POS_BOTTOM_LEFT));

    int i = 1;
    for (auto& score : Engine::instance()->get_high_scores())
    {
        scores.insert(std::pair<std::shared_ptr<MenuText>, std::shared_ptr<MenuText>>(
            std::shared_ptr<MenuText>(new MenuText(score.first, 0, 50 + (100 * i), POS_TOP_CENTER)),
            std::shared_ptr<MenuText>(new MenuText(std::to_string(score.second), 0, 90 + (100 * i), POS_TOP_CENTER))
        ));

        i++;
    }
}

void scenes::HighScores::enter_scene()
{
    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);
    Engine::instance()->draw(back_button);

    for (auto& score : scores)
    {
        Engine::instance()->draw(score.first);
        Engine::instance()->draw(score.second);
    }
}

void scenes::HighScores::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);
    Engine::instance()->erase(back_button);

    for (auto& score : scores)
    {
        Engine::instance()->erase(score.first);
        Engine::instance()->erase(score.second);
    }
}

void scenes::HighScores::update()
{
    //
}

void scenes::HighScores::input(InputCollection input)
{
    //
}

void scenes::HighScores::back_button_clicked()
{
    Engine::instance()->exit_current_scene(old_scene);
}
