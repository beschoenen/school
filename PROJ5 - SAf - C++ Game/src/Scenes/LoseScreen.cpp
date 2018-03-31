#include "LoseScreen.h"
#include "../Engine/Engine.h"
#include "MainMenu.h"

scenes::LoseScreen::LoseScreen() : LoseScreen(nullptr)
{
}

scenes::LoseScreen::LoseScreen(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    auto size = int(SCREEN_HEIGHT * 0.9);

    level_lost_image = std::shared_ptr<MenuImage>(new MenuImage(LOSE_IMAGE, 0, 0, size, size, POS_TOP_CENTER));
    sound = Engine::instance()->load_sound(SOUND_LOSE);
}

void scenes::LoseScreen::enter_scene()
{
    sound.play_once();

    score_text = std::shared_ptr<BigText>(new BigText(std::to_string(std::dynamic_pointer_cast<Game>(old_scene)->get_score()), 0, 0, POS_BOTTOM_CENTER));

    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(level_lost_image);
    Engine::instance()->draw(score_text);

    Engine::instance()->add_high_score(std::dynamic_pointer_cast<Game>(old_scene)->get_score());
}

void scenes::LoseScreen::leave_scene()
{
    sound.stop();
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(level_lost_image);
    Engine::instance()->erase(score_text);
}

void scenes::LoseScreen::update()
{
    //
}

void scenes::LoseScreen::input(InputCollection input)
{
    if (input[engine::Buttons::confirm])
    {
        Engine::instance()->go_to_scene<MainMenu>(true);
    }
}
