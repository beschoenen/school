#ifndef GAME_RANDOMDUNGEON_H
#define GAME_RANDOMDUNGEON_H

#include "Base/LevelLoader.h"

namespace engine
{
    class RandomLevel : public engine::LevelLoader
    {
    public:
        RandomLevel() = default;

        void load_level() override;

        void load_level(int layers) override;
    };
}

#endif //GAME_RANDOMDUNGEON_H
