#include "MenuWallpaper.h"
#include "../../Engine/Engine.h"

MenuWallpaper::MenuWallpaper() : Image(BACKGROUND_IMAGE, 0, 0)
{
    layer = BACKGROUND_LAYER;
}

SDL_Rect MenuWallpaper::get_rect()
{
    auto size = Engine::instance()->get_size();

    return {x, y, size.w, size.h};
}
