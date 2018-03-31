#include "SaveFileData.h"

engine::SaveFileData::SaveFileData(std::shared_ptr<engine::Map>& map, std::shared_ptr<Player>& player,
                                   int level, int round, int score) :
    map(map), player(player), level(level), round(round), score(score)
{
}

std::shared_ptr<engine::Map> engine::SaveFileData::get_map() const
{
    return map;
}

std::shared_ptr<Player> engine::SaveFileData::get_player() const
{
    return player;
}

int engine::SaveFileData::get_level() const
{
    return level;
}

int engine::SaveFileData::get_round() const
{
    return round;
}

int engine::SaveFileData::get_score() const
{
    return score;
}
