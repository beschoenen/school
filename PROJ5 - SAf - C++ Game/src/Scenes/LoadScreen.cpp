#include "LoadScreen.h"
#include "GamePause.h"
#include "../Engine/Engine.h"
#include "../../lib/json/src/json.hpp"
#include "MainMenu.h"
#include <iostream>

scenes::LoadScreen::LoadScreen() : scenes::LoadScreen(nullptr)
{
}

scenes::LoadScreen::LoadScreen(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText("Load game"));

    int i = 0;
    std::vector<std::string> save_files;
    for (auto kv : Engine::instance()->list_save_files())
    {
        save_files.push_back(kv.first);
    }

    std::sort(save_files.begin(), save_files.end());

    for (const auto& listing : save_files)
    {
        load_games.push_back(std::shared_ptr<LoadGameButton>(new LoadGameButton(
            listing, std::bind(&scenes::LoadScreen::load_game_clicked, this, listing),
            listing, 0, 200 + (i * 50), POS_TOP_CENTER
        )));
        i++;
    }

    back_button = std::shared_ptr<MenuButton>(
        new MenuButton("Back", std::bind(&scenes::LoadScreen::back_button_clicked, this), 10, 0, POS_BOTTOM_LEFT));
}

void scenes::LoadScreen::enter_scene()
{
    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);

    for (const auto& load_game : load_games)
    {
        Engine::instance()->draw(load_game);
    }

    Engine::instance()->draw(back_button);
}

void scenes::LoadScreen::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);

    for (const auto& load_game : load_games)
    {
        Engine::instance()->erase(load_game);
    }

    Engine::instance()->erase(back_button);
}

void scenes::LoadScreen::update()
{
    //
}

void scenes::LoadScreen::input(InputCollection input)
{
    //
}

void scenes::LoadScreen::load_game_clicked(std::string file_name)
{
    try
    {
        save_file_name = file_name;

        Engine::instance()->load_save_file(std::move(file_name));

        std::shared_ptr<MainMenu> main_menu = std::dynamic_pointer_cast<MainMenu>(old_scene);
        //std::dynamic_pointer_cast<MainMenu>(old_scene)->stop_sound();
        main_menu->stop_sound();
        Engine::instance()->go_to_scene<Game>();

        return;
    }
    catch (std::string& exception)
    {
        std::cout << exception << std::endl;
    }
    catch (...)
    {
        std::cout << "Something went wrong while loading the save file." << std::endl;
    }

    //std::dynamic_pointer_cast<MainMenu>(old_scene)->start_sound();
    std::shared_ptr<MainMenu> main_menu = std::dynamic_pointer_cast<MainMenu>(old_scene);
    main_menu->start_sound();
}

void scenes::LoadScreen::back_button_clicked()
{
    Engine::instance()->exit_current_scene(old_scene);
}

std::string scenes::LoadScreen::get_save_file_name() const
{
    return save_file_name;
}
