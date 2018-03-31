#include <fstream>
#include <iostream>
#include "MainMenu.h"
#include "Game.h"
#include "KeyMapper.h"
#include "Credits.h"
#include "HighScores.h"
#include "LoadScreen.h"
#include "Help.h"
#include "../Engine/Level/FileLevel.h"
#include "../../lib/json/src/json.hpp"
#include "../Engine/Managers/AdvertisementManager.h"

scenes::MainMenu::MainMenu() : scenes::MainMenu(nullptr)
{
}

scenes::MainMenu::MainMenu(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    AdvertisementManager::init();

    sound = Engine::instance()->load_sound(SOUND_MAINMENU);

    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText(GAME_TITLE));

    start_game_button = std::shared_ptr<MenuButton>(
        new MenuButton("Start Game", std::bind(&MainMenu::start_game_clicked, this), 0, 150, POS_TOP_CENTER));
    load_game_button = std::shared_ptr<MenuButton>(
        new MenuButton("Load Game", std::bind(&MainMenu::load_game_clicked, this), 0, 200, POS_TOP_CENTER));
    map_buttons_button = std::shared_ptr<MenuButton>(
        new MenuButton("Map Buttons", std::bind(&MainMenu::map_buttons_clicked, this), 0, -350, POS_BOTTOM_CENTER));
    button_high_scores = std::shared_ptr<MenuButton>(
        new MenuButton("High Scores", std::bind(&MainMenu::high_scores_clicked, this), 0, -250, POS_BOTTOM_CENTER));
    button_help = std::shared_ptr<MenuButton>(
        new MenuButton("Help", std::bind(&MainMenu::help_button_clicked, this), 0, -200, POS_BOTTOM_CENTER));
    button_credits = std::shared_ptr<MenuButton>(
        new MenuButton("Credits", std::bind(&MainMenu::credits_clicked, this), 0, -150, POS_BOTTOM_CENTER));
    button_exit_game = std::shared_ptr<MenuButton>(
        new MenuButton("Exit Game", std::bind(&MainMenu::exit_game_clicked, this), 0, -50, POS_BOTTOM_CENTER));
}

void scenes::MainMenu::enter_scene()
{
    ad = AdvertisementManager::pick_one();

    sound.play();

    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);
    Engine::instance()->draw(start_game_button);
    Engine::instance()->draw(load_game_button);
    Engine::instance()->draw(button_help);
    Engine::instance()->draw(button_credits);
    Engine::instance()->draw(button_high_scores);
    Engine::instance()->draw(map_buttons_button);
    Engine::instance()->draw(button_exit_game);

    if (ad != nullptr) ad->draw();
}

void scenes::MainMenu::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);
    Engine::instance()->erase(start_game_button);
    Engine::instance()->erase(load_game_button);
    Engine::instance()->erase(button_help);
    Engine::instance()->erase(button_credits);
    Engine::instance()->erase(button_high_scores);
    Engine::instance()->erase(map_buttons_button);
    Engine::instance()->erase(button_exit_game);

    if (ad != nullptr) ad->erase();
}

void scenes::MainMenu::update()
{
    //
}

void scenes::MainMenu::input(InputCollection input)
{
    //
}

void scenes::MainMenu::start_sound()
{
    sound.play();
}

void scenes::MainMenu::stop_sound()
{
    sound.stop();
}

void scenes::MainMenu::handle_file_drop(std::string path)
{
    try
    {
        level_loader = std::make_shared<engine::FileLevel>(path);
        level_loader->load_level();
        start_game_clicked();
    }
    catch (std::exception& e)
    {
        std::cerr << "Error loading level: " << std::endl << e.what() << std::endl;
    }

}

void scenes::MainMenu::start_game_clicked()
{
    sound.stop();
    Engine::instance()->go_to_scene<Game>();
}

void scenes::MainMenu::load_game_clicked()
{
    Engine::instance()->go_to_scene<LoadScreen>();
}

void scenes::MainMenu::exit_game_clicked()
{
    Engine::instance()->quit();
}

void scenes::MainMenu::map_buttons_clicked()
{
    Engine::instance()->go_to_scene<KeyMapper>();
}

void scenes::MainMenu::help_button_clicked()
{
    Engine::instance()->go_to_scene<Help>();
}

void scenes::MainMenu::credits_clicked()
{
    Engine::instance()->go_to_scene<Credits>();
}

void scenes::MainMenu::high_scores_clicked()
{
    Engine::instance()->go_to_scene<HighScores>();
}

std::shared_ptr<engine::LevelLoader> scenes::MainMenu::get_level_loader()
{
    return level_loader;
}
