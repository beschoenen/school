#ifndef SDL_PROJECT_DUNGEON_H
#define SDL_PROJECT_DUNGEON_H

#include "../aliases.h"

namespace level
{
    class Dungeon
    {
        LevelMatrix map_;
    public:
        explicit Dungeon(LevelMatrix map);

        ~Dungeon() = default;

        LevelMatrix get_map() const;
    };
}

#endif //SDL_PROJECT_DUNGEON_H
