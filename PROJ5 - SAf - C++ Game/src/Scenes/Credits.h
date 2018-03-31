#ifndef GAME_CREDITS_H
#define GAME_CREDITS_H

#include "../aliases.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Engine/Engine.h"
#include "../Components/Menu/TitleText.h"
#include "../Components/Menu/MenuText.h"
#include "../Components/Menu/MenuButton.h"

namespace scenes
{
    class Credits : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;
        std::shared_ptr<MenuText> credit_text;
        std::shared_ptr<MenuText> rogier_text;
        std::shared_ptr<MenuText> thomas_text;
        std::shared_ptr<MenuText> bas_text;
        std::shared_ptr<MenuText> mike_text;
        std::shared_ptr<MenuText> kevin_text;
        std::shared_ptr<MenuText> danielle_text;
        std::shared_ptr<MenuButton> escape_button;

    public:
        Credits();

        explicit Credits(std::shared_ptr<Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void on_escape_pressed();
    };
}


#endif //GAME_CREDITS_H
