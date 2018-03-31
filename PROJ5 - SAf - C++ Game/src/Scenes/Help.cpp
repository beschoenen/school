#include "Help.h"
#include <utility>

scenes::Help::Help() : Help(nullptr)
{
}

scenes::Help::Help(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText("Help"));

    note_keys = std::shared_ptr<MenuText>(new MenuText("To see and customize key bindings", 0, 120, POS_TOP_CENTER));
    note_direction = std::shared_ptr<MenuText>(new MenuText("Go to the keymapper screen", 0, 150, POS_TOP_CENTER));

    text_up_label = std::shared_ptr<MenuText>(new MenuText("Up", -150, 220, POS_TOP_CENTER));
    text_up_value = std::shared_ptr<MenuText>(new MenuText("Walk up", 150, 220, POS_TOP_CENTER));

    text_down_label = std::shared_ptr<MenuText>(new MenuText("Down", -150, 270, POS_TOP_CENTER));
    text_down_value = std::shared_ptr<MenuText>(new MenuText("Walk Down", 150, 270, POS_TOP_CENTER));

    text_left_label = std::shared_ptr<MenuText>(new MenuText("Left", -150, 320, POS_TOP_CENTER));
    text_left_value = std::shared_ptr<MenuText>(new MenuText("Walk Left", 150, 320, POS_TOP_CENTER));

    text_right_label = std::shared_ptr<MenuText>(new MenuText("Right", -150, 370, POS_TOP_CENTER));
    text_right_value = std::shared_ptr<MenuText>(new MenuText("Walk Right", 150, 370, POS_TOP_CENTER));

    text_action_label = std::shared_ptr<MenuText>(new MenuText("Action", -150, 420, POS_TOP_CENTER));
    text_action_value = std::shared_ptr<MenuText>(new MenuText("Attack", 150, 420, POS_TOP_CENTER));

    text_menu_label = std::shared_ptr<MenuText>(new MenuText("Menu", -150, 470, POS_TOP_CENTER));
    text_menu_value = std::shared_ptr<MenuText>(new MenuText("Return to menu", 150, 470, POS_TOP_CENTER));

    text_cancel_label = std::shared_ptr<MenuText>(new MenuText("Cancel", -150, 520, POS_TOP_CENTER));
    text_cancel_value = std::shared_ptr<MenuText>(new MenuText("Undo", 150, 520, POS_TOP_CENTER));

    escape_button = std::shared_ptr<MenuButton>(
        new MenuButton("Back", std::bind(&Help::on_back_pressed, this), 10, 0, POS_BOTTOM_LEFT));
}

void scenes::Help::enter_scene()
{
    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);
    Engine::instance()->draw(note_keys);
    Engine::instance()->draw(note_direction);
    Engine::instance()->draw(text_up_label);
    Engine::instance()->draw(text_up_value);
    Engine::instance()->draw(text_down_label);
    Engine::instance()->draw(text_down_value);
    Engine::instance()->draw(text_left_label);
    Engine::instance()->draw(text_left_value);
    Engine::instance()->draw(text_right_label);
    Engine::instance()->draw(text_right_value);
    Engine::instance()->draw(text_action_label);
    Engine::instance()->draw(text_action_value);
    Engine::instance()->draw(text_menu_label);
    Engine::instance()->draw(text_menu_value);
    Engine::instance()->draw(text_cancel_label);
    Engine::instance()->draw(text_cancel_value);
    Engine::instance()->draw(escape_button);
}

void scenes::Help::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);
    Engine::instance()->erase(note_keys);
    Engine::instance()->erase(note_direction);
    Engine::instance()->erase(text_up_label);
    Engine::instance()->erase(text_up_value);
    Engine::instance()->erase(text_down_label);
    Engine::instance()->erase(text_down_value);
    Engine::instance()->erase(text_left_label);
    Engine::instance()->erase(text_left_value);
    Engine::instance()->erase(text_right_label);
    Engine::instance()->erase(text_right_value);
    Engine::instance()->erase(text_action_label);
    Engine::instance()->erase(text_action_value);
    Engine::instance()->erase(text_menu_label);
    Engine::instance()->erase(text_menu_value);
    Engine::instance()->erase(text_cancel_label);
    Engine::instance()->erase(text_cancel_value);
    Engine::instance()->erase(escape_button);
}

void scenes::Help::update()
{
    //
}

void scenes::Help::input(InputCollection input)
{
    //
}

void scenes::Help::on_back_pressed()
{
    Engine::instance()->exit_current_scene(old_scene);
}
