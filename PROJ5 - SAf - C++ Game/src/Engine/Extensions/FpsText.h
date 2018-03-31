#ifndef GAME_FPSTEXT_H
#define GAME_FPSTEXT_H

#include "../../Engine/Components/Text.h"

namespace engine
{
    class FpsText : public Text
    {
    public:
        explicit FpsText(std::string text);
    };
}

#endif //GAME_FPSTEXT_H
