#ifndef GAME_HIGHSCORE_H
#define GAME_HIGHSCORE_H

#include "../Engine/Base/Scene.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Components/Menu/TitleText.h"
#include "../Components/Menu/MenuButton.h"
#include "../Components/Menu/MenuText.h"

namespace scenes
{
    class HighScores : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;
        std::shared_ptr<MenuButton> back_button;
        std::map<std::shared_ptr<MenuText>, std::shared_ptr<MenuText>> scores;

    public:
        HighScores();

        explicit HighScores(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void back_button_clicked();
    };
}

#endif //GAME_HIGHSCORE_H
