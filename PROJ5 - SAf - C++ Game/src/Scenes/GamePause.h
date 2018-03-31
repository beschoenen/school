#ifndef GAME_GAMEPAUSE_H
#define GAME_GAMEPAUSE_H

#include "../Engine/Components/Button.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Engine/Base/Scene.h"
#include "../Components/Menu/TitleText.h"
#include "../Engine/Models/Sound.h"
#include "../Components/Menu/MenuText.h"
#include "Game.h"

namespace scenes
{
    class GamePause : public engine::Scene
    {
        std::shared_ptr<Game> game;

        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;
        std::shared_ptr<MenuText> score_text;
        std::shared_ptr<engine::Button> continue_game;
        std::shared_ptr<engine::Button> toggle_game_speed_button;
        std::shared_ptr<engine::Button> map_buttons_button;
        std::shared_ptr<engine::Button> button_exit_game;
        std::shared_ptr<engine::Button> button_save;

        engine::Sound sound = engine::Sound(nullptr);

        std::string get_game_speed_label();

    public:
        GamePause();

        explicit GamePause(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void continue_game_clicked();

        void exit_game_clicked();

        void map_buttons_clicked();

        void save_clicked();

        void toggle_game_speed_clicked();
    };
}

#endif //GAME_GAMEPAUSE_H
