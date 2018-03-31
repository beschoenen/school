#ifndef GAME_HELP_H
#define GAME_HELP_H

#include "../Engine/Engine.h"
#include "../aliases.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Components/Menu/TitleText.h"
#include "../Components/Menu/MenuText.h"
#include "../Components/Menu/MenuButton.h"

namespace scenes
{
    class Help : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;

        std::shared_ptr<MenuText> note_keys;
        std::shared_ptr<MenuText> note_direction;

        //Labels
        std::shared_ptr<MenuText> text_up_label;
        std::shared_ptr<MenuText> text_down_label;
        std::shared_ptr<MenuText> text_left_label;
        std::shared_ptr<MenuText> text_right_label;
        std::shared_ptr<MenuText> text_action_label;
        std::shared_ptr<MenuText> text_menu_label;
        std::shared_ptr<MenuText> text_cancel_label;

        std::shared_ptr<MenuText> text_up_value;
        std::shared_ptr<MenuText> text_down_value;
        std::shared_ptr<MenuText> text_left_value;
        std::shared_ptr<MenuText> text_right_value;
        std::shared_ptr<MenuText> text_action_value;
        std::shared_ptr<MenuText> text_menu_value;
        std::shared_ptr<MenuText> text_cancel_value;

        std::shared_ptr<MenuButton> escape_button;

    public:
        Help();

        explicit Help(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void on_back_pressed();
    };
}

#endif //GAME_HELP_H
