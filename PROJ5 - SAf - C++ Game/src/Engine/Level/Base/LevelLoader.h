#ifndef GAME_LEVELLOADER_H
#define GAME_LEVELLOADER_H

#include "../../../aliases.h"
#include "../../../Level/Dungeon.h"

namespace engine
{
    class LevelLoader
    {
    protected:
        std::shared_ptr<level::Dungeon> dungeon;

    public:
        virtual void load_level();

        virtual void load_level(int layers);

        virtual std::shared_ptr<level::Dungeon> get_dungeon();
    };
}


#endif //GAME_LEVELLOADER_H
