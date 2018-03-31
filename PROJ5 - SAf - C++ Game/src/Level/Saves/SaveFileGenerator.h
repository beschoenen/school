#ifndef GAME_SAVE_FILE_GENERATOR_H
#define GAME_SAVE_FILE_GENERATOR_H

#include <string>
#include "../../Scenes/Game.h"
#include "SaveFileIntegrityHandler.h"

namespace level
{
    class SaveFileGenerator
    {
        SaveFileIntegrityChecker save_file_integrity_checker;

    public:
        SaveFileGenerator() = default;

        ~SaveFileGenerator() = default;

        std::string get_info(std::shared_ptr<scenes::Game> game);

        std::string get_map(std::shared_ptr<scenes::Game> game);

        std::vector<std::vector<GridTile>> create_grid(LevelMatrix map_matrix, int row, int column);

        bool compare_grids(std::vector<std::vector<GridTile>> grid1, std::vector<std::vector<GridTile>> grid2);
    };
}

#endif //GAME_SAVE_FILE_GENERATOR_H
