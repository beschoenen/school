#include "Dungeon.h"

level::Dungeon::Dungeon(LevelMatrix map) : map_(std::move(map))
{
}

LevelMatrix level::Dungeon::get_map() const
{
    return this->map_;
}
