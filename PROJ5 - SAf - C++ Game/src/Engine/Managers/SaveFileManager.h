#ifndef GAME_SAVEFILESMANAGER_H
#define GAME_SAVEFILESMANAGER_H

#include <string>
#include <unordered_map>
#include "../../Scenes/Game.h"
#include "../Models/SaveFileData.h"

namespace engine
{
    class SaveFileManager
    {
        std::shared_ptr<SaveFileData> save_file_data;

    protected:
        SaveFileManager() = default;

    public:
        // Save games

        std::unordered_map<std::string, bool> list_save_files() const;

        void load_save_file(std::string save_file_name);

        void create_save_file(std::string json_info, std::string json_map);

        std::shared_ptr<engine::SaveFileData> get_save_file_data() const;

        void create_save_file(std::string save_file_name, std::string json_info, std::string json_map);

        void delete_save_file(std::string save_file_name);

        // High scores

        void add_high_score(int score);

        std::vector<std::pair<std::string, int>> get_high_scores();
    };
}

#endif //GAME_SAVEFILESMANAGER_H
