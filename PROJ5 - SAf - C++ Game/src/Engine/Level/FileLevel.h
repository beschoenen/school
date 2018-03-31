#ifndef GAME_FILELEVEL_H
#define GAME_FILELEVEL_H

#include "Base/LevelLoader.h"

namespace engine
{
    class FileLevel : public LevelLoader
    {
        std::string path;

    public:
        explicit FileLevel(std::string path);

        void load_level() override;

        void load_level(int layers) override;
    };
}

#endif //GAME_FILELEVEL_H
