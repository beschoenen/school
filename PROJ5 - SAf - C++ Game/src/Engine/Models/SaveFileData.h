#ifndef GAME_SAVE_FILE_DATA_H
#define GAME_SAVE_FILE_DATA_H

#include "../../Components/Player.h"

namespace engine
{
    class SaveFileData
    {
        std::shared_ptr<engine::Map> map;
        std::shared_ptr<Player> player;
        int level;
        int round;
        int score;

    public:
        SaveFileData(std::shared_ptr<engine::Map>& map, std::shared_ptr<Player>& player,
                     int level, int round, int score);

        std::shared_ptr<engine::Map> get_map() const;

        std::shared_ptr<Player> get_player() const;

        int get_level() const;

        int get_round() const;

        int get_score() const;
    };
}


#endif //GAME_SAVE_FILE_DATA_H
