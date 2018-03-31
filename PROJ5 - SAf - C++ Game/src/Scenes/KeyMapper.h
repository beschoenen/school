#ifndef GAME_KEYMAPPERMENU_H
#define GAME_KEYMAPPERMENU_H

#include "../Engine/Base/Scene.h"
#include "../Components/Menu/MenuWallpaper.h"
#include "../Components/Menu/KeyMapperText.h"
#include "../Components/Menu/TitleText.h"
#include "../Components/KeyMapperButton.h"
#include "../Components/Menu/MenuText.h"
#include <vector>

namespace scenes
{
    class KeyMapper : public engine::Scene
    {
        std::shared_ptr<MenuWallpaper> wallpaper;
        std::shared_ptr<TitleText> title;
        std::shared_ptr<MenuButton> back_button;

        std::shared_ptr<MenuText> mapping_text;
        std::vector<std::shared_ptr<KeyMapperButton>> bind_buttons;
        std::vector<std::shared_ptr<KeyMapperText>> pressed_text;

        engine::Buttons binding_button = engine::none;

    public:
        KeyMapper();

        explicit KeyMapper(std::shared_ptr<engine::Scene> old_scene);

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        void input(InputCollection input) override;

        void set_button_on_mapper();

        void back_button_clicked();

        void set_bind_key(engine::Buttons button);
    };
}

#endif //GAME_KEYMAPPERMENU_H
