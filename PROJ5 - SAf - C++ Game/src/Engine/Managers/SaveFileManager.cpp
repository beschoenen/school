#include "SaveFileManager.h"

#include "../../Utilities/File.h"
#include "../../../lib/json/src/json.hpp"
#include "../Level/FileLevel.h"
#include <iostream>
#include "../../Level/Saves/SaveFileIntegrityHandler.h"
#include "../../Level/Saves/SaveFileGenerator.h"
#include "../../Scenes/LoadScreen.h"

#define HIGHSCORE_FILE "highscores.json"
#define MAP_FILE "map.json"
#define INFO_FILE "info.json"

std::unordered_map<std::string, bool> engine::SaveFileManager::list_save_files() const
{
    return File::list_directory(SAVEFILES_DIR);
}

void engine::SaveFileManager::load_save_file(std::string save_file_name)
{
    try
    {
        std::string path = SAVEFILES_DIR;
        path += "/" + save_file_name;

        // Read files
        std::string map_text = File::read_file(path, MAP_FILE);
        std::string info = File::read_file(path, INFO_FILE);

        // Get info
        nlohmann::json json = nlohmann::json::parse(info);

        int max_health = json["player"]["maxhealth"];
        int health = json["player"]["health"];
        int minimum_damage = json["player"]["mindamage"];
        int maximum_damage = json["player"]["maxdamage"];
        int score = json["player"]["score"];

        int level = json["game"]["level"];
        int round = json["game"]["round"];

        // Check save file integrity
        level::SaveFileIntegrityChecker save_file_integrity_checker;

        save_file_integrity_checker.generate_md5_map(map_text);
        save_file_integrity_checker.generate_md5_info(level, round, score, max_health,
                                                      health, minimum_damage, maximum_damage);

        if (save_file_integrity_checker.get_md5_hash() != json["integrity"])
        {
            throw std::string("The save file was changed.");
        }

        // Create map and player
        std::string map_path = File::get_file_path(path, MAP_FILE);
        std::shared_ptr<engine::LevelLoader> file_level = std::make_shared<engine::FileLevel>(map_path);
        std::shared_ptr<engine::Map> map = std::make_shared<engine::Map>(file_level);
        auto player = std::make_shared<Player>(Player(map, max_health, health, minimum_damage, maximum_damage));

        save_file_data = std::make_shared<SaveFileData>(map, player, level, round, score);
    }
    catch (...)
    {
        save_file_data = nullptr;
        throw std::string("Invalid save file");
    }
}

void engine::SaveFileManager::create_save_file(std::string json_info, std::string json_map)
{
    // Get date and time
    time_t raw_time;
    struct tm* time_info;
    char buffer[80];

    time(&raw_time);
    time_info = localtime(&raw_time);

    strftime(buffer, sizeof(buffer), "%d-%m-%Y %H_%M_%S", time_info);
    std::string date(buffer);

    // Create new save data
    create_save_file(date, json_info, json_map);
}

std::shared_ptr<engine::SaveFileData> engine::SaveFileManager::get_save_file_data() const
{
    return save_file_data;
}

void engine::SaveFileManager::create_save_file(std::string save_file_name, std::string json_info, std::string json_map)
{
    if (!File::has_sub_directory(SAVEFILES_DIR))
    {
        File::create_sub_directory(SAVEFILES_DIR);
    }

    std::string save_file_directory = SAVEFILES_DIR;
    save_file_directory += "/" + save_file_name;

    File::create_sub_directory(save_file_directory);
    File::write_file(save_file_directory, INFO_FILE, std::move(json_info));
    File::write_file(save_file_directory, MAP_FILE, std::move(json_map));
}

void engine::SaveFileManager::delete_save_file(std::string save_file_name)
{
    std::string save_file_directory = SAVEFILES_DIR;
    save_file_directory += "/" + save_file_name;

    File::delete_file(save_file_directory, INFO_FILE);
    File::delete_file(save_file_directory, MAP_FILE);
    File::delete_sub_directory(save_file_directory);
}

std::vector<std::pair<std::string, int>> engine::SaveFileManager::get_high_scores()
{
    std::vector<std::pair<std::string, int>> high_scores;

    if (!File::has_file("", HIGHSCORE_FILE)) return high_scores;

    try
    {
        auto json = nlohmann::json::parse(File::read_file("", HIGHSCORE_FILE));

        for (nlohmann::json::iterator it = json.begin(); it != json.end(); ++it)
        {
            high_scores.emplace_back(it.value()[0], it.value()[1]);
        }
    }
    catch (...)
    {
        std::cerr << "Invalid JSON, couldn't read high scores" << std::endl;
    }

    return high_scores;
};

void engine::SaveFileManager::add_high_score(int score)
{
    auto scores = get_high_scores();

    time_t raw_time;
    struct tm* time_info;
    char buffer[80];

    time(&raw_time);
    time_info = localtime(&raw_time);

    strftime(buffer, sizeof(buffer), "%d-%m-%Y %H:%M:%S", time_info);
    std::string date(buffer);

    scores.emplace_back(date, score);

    std::sort(scores.begin(), scores.end(),
              [](const std::pair<std::string, int> left, const std::pair<std::string, int> right) {
                  return left.second > right.second;
              }
    );

    auto json = nlohmann::json::array();

    int amount = 3;
    if (scores.size() < amount)
    {
        amount = scores.size();
    }

    for (int i = 0; i < amount; i++)
    {
        std::pair<std::string, int> item = scores.at(i);
        json.push_back({item.first, item.second});
    }

    File::write_file("", HIGHSCORE_FILE, json.dump(2));
}
