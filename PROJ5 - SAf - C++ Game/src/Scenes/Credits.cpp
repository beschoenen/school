#include "Credits.h"
#include <utility>

scenes::Credits::Credits() : Credits(nullptr)
{
}

scenes::Credits::Credits(std::shared_ptr<Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText("Credits"));

    credit_text = std::shared_ptr<MenuText>(new MenuText("Created by group Foxtrot", 0, 150, POS_TOP_CENTER));
    rogier_text = std::shared_ptr<MenuText>(new MenuText("Rogier Bazelmans", 0, 220, POS_TOP_CENTER));
    thomas_text = std::shared_ptr<MenuText>(new MenuText("Thomas van Doorn", 0, 270, POS_TOP_CENTER));
    bas_text = std::shared_ptr<MenuText>(new MenuText("Bas Fonville", 0, 320, POS_TOP_CENTER));
    mike_text = std::shared_ptr<MenuText>(new MenuText("Mike Jaartsveld", 0, 370, POS_TOP_CENTER));
    kevin_text = std::shared_ptr<MenuText>(new MenuText("Kevin Richter", 0, 420, POS_TOP_CENTER));
    danielle_text = std::shared_ptr<MenuText>(new MenuText("Danielle van Rooij", 0, 470, POS_TOP_CENTER));

    escape_button = std::shared_ptr<MenuButton>(
        new MenuButton("Back", std::bind(&Credits::on_escape_pressed, this), 10, 0, POS_BOTTOM_LEFT));
}

void scenes::Credits::enter_scene()
{
    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);
    Engine::instance()->draw(credit_text);
    Engine::instance()->draw(rogier_text);
    Engine::instance()->draw(thomas_text);
    Engine::instance()->draw(bas_text);
    Engine::instance()->draw(mike_text);
    Engine::instance()->draw(kevin_text);
    Engine::instance()->draw(danielle_text);
    Engine::instance()->draw(escape_button);
}

void scenes::Credits::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);
    Engine::instance()->erase(rogier_text);
    Engine::instance()->erase(thomas_text);
    Engine::instance()->erase(bas_text);
    Engine::instance()->erase(mike_text);
    Engine::instance()->erase(kevin_text);
    Engine::instance()->erase(danielle_text);
    Engine::instance()->erase(credit_text);
    Engine::instance()->erase(escape_button);
}

void scenes::Credits::update()
{
    //
}

void scenes::Credits::input(InputCollection input)
{
    //
}

void scenes::Credits::on_escape_pressed()
{
    Engine::instance()->exit_current_scene(old_scene);
}
