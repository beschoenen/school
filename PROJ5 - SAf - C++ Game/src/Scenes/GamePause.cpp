#include "GamePause.h"
#include "KeyMapper.h"
#include "../Engine/Engine.h"
#include "MainMenu.h"
#include "../Level/Saves/SaveFileGenerator.h"
#include "../Utilities/File.h"
#include "LoadScreen.h"
#include <ctime>

scenes::GamePause::GamePause() : GamePause(nullptr)
{
}

scenes::GamePause::GamePause(std::shared_ptr<engine::Scene> old_scene) : Scene(old_scene)
{
    sound = Engine::instance()->load_sound(SOUND_PAUSEMENU);

    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText("Pause"));

    game = std::dynamic_pointer_cast<Game>(old_scene);

    // Score
    int score = game == nullptr ? 0 : game->get_score();
    score_text = std::shared_ptr<MenuText>(new MenuText("Score " + std::to_string(score), 0, 150, POS_TOP_CENTER));

    continue_game = std::shared_ptr<MenuButton>(
        new MenuButton("Continue Game", std::bind(&GamePause::continue_game_clicked, this), 0, 200, POS_TOP_CENTER));
    toggle_game_speed_button = std::shared_ptr<MenuButton>(
        new MenuButton(get_game_speed_label(), std::bind(&GamePause::toggle_game_speed_clicked, this), 0, -300, POS_BOTTOM_CENTER));
    map_buttons_button = std::shared_ptr<MenuButton>(
        new MenuButton("Map Buttons", std::bind(&GamePause::map_buttons_clicked, this), 0, -200, POS_BOTTOM_CENTER));
    button_save = std::shared_ptr<MenuButton>(
        new MenuButton("Save and exit game", std::bind(&GamePause::save_clicked, this), 0, -100, POS_BOTTOM_CENTER));
    button_exit_game = std::shared_ptr<MenuButton>(
        new MenuButton("Exit Game", std::bind(&GamePause::exit_game_clicked, this), 0, -50, POS_BOTTOM_CENTER));
}

void scenes::GamePause::enter_scene()
{
    sound.play();

    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);
    Engine::instance()->draw(score_text);
    Engine::instance()->draw(continue_game);
    Engine::instance()->draw(button_save);
    Engine::instance()->draw(toggle_game_speed_button);
    Engine::instance()->draw(map_buttons_button);
    Engine::instance()->draw(button_exit_game);
}

void scenes::GamePause::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);
    Engine::instance()->erase(score_text);
    Engine::instance()->erase(continue_game);
    Engine::instance()->erase(button_save);
    Engine::instance()->erase(toggle_game_speed_button);
    Engine::instance()->erase(map_buttons_button);
    Engine::instance()->erase(button_exit_game);
}

void scenes::GamePause::update()
{
    //
}

void scenes::GamePause::input(InputCollection input)
{
    if (input[engine::Buttons::menu])
    {
        return continue_game_clicked();
    }
}

void scenes::GamePause::continue_game_clicked()
{
    sound.stop();
    Engine::instance()->exit_current_scene(old_scene);
}

void scenes::GamePause::exit_game_clicked()
{
    sound.stop();
    Engine::instance()->go_to_scene<MainMenu>(true);
}

void scenes::GamePause::map_buttons_clicked()
{
    Engine::instance()->go_to_scene<KeyMapper>();
}

void scenes::GamePause::save_clicked()
{
    if (game == nullptr)
    {
        exit_game_clicked();
        return;
    }

    // Generate info and map
    level::SaveFileGenerator save_file_generator;

    std::string json_map = save_file_generator.get_map(game);
    std::string json_info = save_file_generator.get_info(game);

    // Check if it's a new or loaded game
    auto load_screen = std::dynamic_pointer_cast<scenes::LoadScreen>(game->get_old_scene());
    if (load_screen == nullptr)
    {
        // New game
        std::unordered_map<std::string, bool> directory_listing = Engine::instance()->list_save_files();
        if (directory_listing.size() >= 5)
        {
            std::string oldest_save_file = File::get_oldest_sub_directory(SAVEFILES_DIR);
            Engine::instance()->delete_save_file(oldest_save_file);
        }
    }
    else
    {
        // Loaded save file
        std::string save_file_name = load_screen->get_save_file_name();
        Engine::instance()->delete_save_file(save_file_name);
    }

    Engine::instance()->create_save_file(json_info, json_map);

    exit_game_clicked();
}

void scenes::GamePause::toggle_game_speed_clicked()
{
    auto scene = std::dynamic_pointer_cast<Game>(old_scene);

    if (scene == nullptr) return;

    scene->set_game_speed(scene->get_game_speed() == 1 ? 2 : 1);
    toggle_game_speed_button->set_text(get_game_speed_label());
}

std::string scenes::GamePause::get_game_speed_label()
{
    std::string base = "Unknown";

    auto scene = std::dynamic_pointer_cast<Game>(old_scene);
    if (scene != nullptr)
    {
        base = scene->get_game_speed() == 1 ? "Normal" : "Fast";
    }

    return "Game speed: " + base;
}
