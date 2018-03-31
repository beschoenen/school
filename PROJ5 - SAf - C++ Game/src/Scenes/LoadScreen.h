#ifndef GAME_LOADSCREEN_H
#define GAME_LOADSCREEN_H

#include "../Components/Menu/MenuWallpaper.h"
#include "../Engine/Base/Scene.h"
#include "../Components/Menu/TitleText.h"
#include "../Engine/Models/Sound.h"
#include <vector>
#include "../Components/Menu/LoadGameButton.h"
#include "../Engine/Base/Map.h"
#include <memory>
#include "../Components/Player.h"

namespace scenes
{
    class LoadScreen : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;
        std::vector<std::shared_ptr<LoadGameButton>> load_games;
        std::shared_ptr<MenuButton> back_button;

        engine::Sound sound = engine::Sound(nullptr);

        std::string save_file_name;

    public:
        LoadScreen();

        explicit LoadScreen(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void load_game_clicked(std::string file_name);

        std::string get_save_file_name() const;

        void back_button_clicked();
    };
}


#endif //GAME_LOADSCREEN_H
