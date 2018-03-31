#include "FileLevel.h"
#include "../../Level/SubTileFactory.h"

engine::FileLevel::FileLevel(std::string path) : path(std::move(path))
{
}

void engine::FileLevel::load_level()
{
    level::SubTileFactory factory;
    LevelMatrix grid = factory.create_tiles(path);

    dungeon = std::make_shared<level::Dungeon>(level::Dungeon(grid));
}

void engine::FileLevel::load_level(int layers)
{
    load_level();
}
