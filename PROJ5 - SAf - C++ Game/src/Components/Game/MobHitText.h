#ifndef GAME_MOBHITTEXT_H
#define GAME_MOBHITTEXT_H

#include "../../Engine/Components/Text.h"
#include "../Mob.h"

class MobHitText : public engine::Text
{
    std::shared_ptr<Mob> mob = nullptr;

public:
    explicit MobHitText(std::string text);

    SDL_Rect get_rect() override;

    std::shared_ptr<Mob> get_mob();

    void set_mob(std::shared_ptr<Mob> mob);

    void clear_mob();
};


#endif //GAME_MOBHITTEXT_H

