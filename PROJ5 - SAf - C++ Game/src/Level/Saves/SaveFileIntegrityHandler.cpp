#include "SaveFileIntegrityHandler.h"

#include <sstream>
#include "../../Utilities/md5.h"

std::string
level::SaveFileIntegrityChecker::generate_md5_info(int level, int round, int score, int max_health,
                                                   int health_points, int min_damage, int max_damage)
{
    std::stringstream info;

    info << round << max_health << max_health << min_damage << level << health_points << max_damage
         << score << level << max_damage << score << round << health_points;

    md5_info = md5(info.str());

    return md5_info;
}

std::string level::SaveFileIntegrityChecker::generate_md5_map(std::string map_json)
{
    md5_map = md5(std::move(map_json));

    return md5_map;
}

std::string level::SaveFileIntegrityChecker::get_md5_hash() const
{
    return md5_info + md5_map;
}
