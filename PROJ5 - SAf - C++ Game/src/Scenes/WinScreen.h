#ifndef GAME_WINSCREEN_H
#define GAME_WINSCREEN_H

#include "../Engine/Base/Scene.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Components/Menu/MenuImage.h"
#include "../Engine/Models/Sound.h"

namespace scenes
{
    class WinScreen : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<MenuImage> level_cleared_image;
        engine::Sound sound = engine::Sound(nullptr);

    public:
        WinScreen();

        explicit WinScreen(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;
    };
}

#endif //GAME_WINSCREEN_H
