#ifndef GAME_MAIN_MENU_H
#define GAME_MAIN_MENU_H

#include "../Components/Menu/MenuWallpaper.h"
#include "../Engine/Base/Scene.h"
#include "../Components/Menu/TitleText.h"
#include "../Engine/Models/Sound.h"
#include "../Components/Menu/MenuButton.h"
#include "../Engine/Level/Base/LevelLoader.h"
#include "../Components/Menu/Advertisement.h"

namespace scenes
{
    class MainMenu : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;

        std::shared_ptr<MenuButton> start_game_button;
        std::shared_ptr<MenuButton> load_game_button;
        std::shared_ptr<MenuButton> map_buttons_button;
        std::shared_ptr<MenuButton> button_help;
        std::shared_ptr<MenuButton> button_exit_game;
        std::shared_ptr<MenuButton> button_credits;
        std::shared_ptr<MenuButton> button_high_scores;

        engine::Sound sound = engine::Sound(nullptr);

        std::shared_ptr<engine::LevelLoader> level_loader;

        std::shared_ptr<Advertisement> ad;

    public:
        MainMenu();

        explicit MainMenu(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void start_sound();

        void stop_sound();

        void handle_file_drop(std::string path) override;

        void start_game_clicked();

        void load_game_clicked();

        void help_button_clicked();

        void exit_game_clicked();

        void map_buttons_clicked();

        void credits_clicked();

        void high_scores_clicked();

        std::shared_ptr<engine::LevelLoader> get_level_loader();
    };
}


#endif //GAME_MAIN_MENU_H
