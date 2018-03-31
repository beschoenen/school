#include "RandomLevel.h"
#include "../../Level/DungeonGenerator.h"
#include "../../defines.h"

void engine::RandomLevel::load_level()
{
    level::DungeonGenerator generator;
    dungeon = std::shared_ptr<level::Dungeon>{generator.generate_dungeon(LEVEL_WIDTH, LEVEL_HEIGHT)};
}

void engine::RandomLevel::load_level(int layers)
{
    level::DungeonGenerator generator;
    dungeon = std::shared_ptr<level::Dungeon>{generator.generate_dungeon(layers, layers)};
}
