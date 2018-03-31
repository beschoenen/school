#include "WinScreen.h"
#include "../Engine/Engine.h"

scenes::WinScreen::WinScreen() : WinScreen(nullptr)
{
}

scenes::WinScreen::WinScreen(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());

    auto size = int(SCREEN_HEIGHT * 0.9);

    level_cleared_image = std::shared_ptr<MenuImage>(new MenuImage(WIN_IMAGE, 0, 0, size, size, POS_TOP_CENTER));
    sound = Engine::instance()->load_sound(SOUND_WIN);
}

void scenes::WinScreen::enter_scene()
{
    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(level_cleared_image);
    sound.play_once();
}

void scenes::WinScreen::leave_scene()
{
    sound.stop();
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(level_cleared_image);
}

void scenes::WinScreen::update()
{
    //
}

void scenes::WinScreen::input(InputCollection input)
{
    if (input[engine::Buttons::confirm])
    {
        Engine::instance()->exit_current_scene(old_scene);
    }
}
