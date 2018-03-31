#include "LevelLoader.h"

void engine::LevelLoader::load_level()
{
    throw "Method not implemented";
}

void engine::LevelLoader::load_level(int layers)
{
    throw "Method not implemented";
}

std::shared_ptr<level::Dungeon> engine::LevelLoader::get_dungeon()
{
    return dungeon;
}
