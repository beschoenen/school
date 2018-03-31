#include "MobHitText.h"

MobHitText::MobHitText(std::string text) : Text(std::move(text), FONT, 0, 0, {255, 255, 255, 255}, 12)
{
    anchor = POS_CENTER_CENTER;
    layer = HUD_LAYER;
}

SDL_Rect MobHitText::get_rect()
{
    if (mob == nullptr)
    {
        return {0, 0, 0, 0};
    }

    SDL_Rect rect =
    {
        mob->get_x(),
        mob->get_y() - 30,
        width < 0 ? calculated_width : width,
        height < 0 ? calculated_height : height
    };

    return transform_on_anchor(rect);
}

std::shared_ptr<Mob> MobHitText::get_mob()
{
    return mob;
}

void MobHitText::set_mob(std::shared_ptr<Mob> mob)
{
    this->mob = mob;
}

void MobHitText::clear_mob()
{
    mob = nullptr;
    set_text(" ");
}
