#ifndef GAME_SAVE_FILE_INTEGRITY_HANDLER_H
#define GAME_SAVE_FILE_INTEGRITY_HANDLER_H

#include <string>

namespace level
{
    class SaveFileIntegrityChecker
    {
        std::string md5_info;
        std::string md5_map;

    public:
        SaveFileIntegrityChecker() = default;

        ~SaveFileIntegrityChecker() = default;

        std::string generate_md5_info(int level, int round, int score, int max_health,
                                      int health_points, int min_damage, int max_damage);

        std::string generate_md5_map(std::string map_json);

        std::string get_md5_hash() const;
    };
}

#endif //GAME_SAVE_FILE_INTEGRITY_HANDLER_H
