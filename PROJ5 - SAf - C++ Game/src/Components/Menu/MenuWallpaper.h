#ifndef GAME_MENUWALLPAPER_H
#define GAME_MENUWALLPAPER_H

#include "../../Engine/Components/Image.h"

class MenuWallpaper : public engine::Image
{
public:
    MenuWallpaper();

    SDL_Rect get_rect();
};


#endif //GAME_MENUWALLPAPER_H
