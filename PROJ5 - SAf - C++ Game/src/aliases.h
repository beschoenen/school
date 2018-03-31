#ifndef SDL_PROJECT_ALIASES_H
#define SDL_PROJECT_ALIASES_H

#include <memory>
#include <functional>
#include <SDL.h>
#include "Level/Tiles/GridTile.h"
#include "Engine/Input/Mapping.h"
#include "Level/Tiles/Base/Tile.h"
#include <vector>

using BaseLevelMatrix = std::vector<std::vector<std::shared_ptr<level::Tile>>>;
using LevelMatrix = std::vector<std::vector<std::shared_ptr<level::GridTile>>>;
using InputCollection = std::map<engine::Buttons, bool>;

#endif //SDL_PROJECT_ALIASES_H
