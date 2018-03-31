#ifndef GAME_LOSESCREEN_H
#define GAME_LOSESCREEN_H

#include "../Engine/Base/Scene.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Components/Menu/BigText.h"
#include "../Engine/Models/Sound.h"
#include "../Components/Menu/MenuImage.h"

namespace scenes
{
    class LoseScreen : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<MenuImage> level_lost_image;
        std::shared_ptr<BigText> score_text;
        engine::Sound sound = engine::Sound(nullptr);

    public:
        LoseScreen();

        explicit LoseScreen(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;
    };
}

#endif //GAME_LOSESCREEN_H
