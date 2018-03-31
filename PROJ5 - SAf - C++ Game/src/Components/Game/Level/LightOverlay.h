#ifndef GAME_LIGHTOVERLAY_H
#define GAME_LIGHTOVERLAY_H

#include <SDL.h>
#include "../../../Engine/Base/Renderable.h"

class LightOverlay : public engine::Renderable
{
    int radius_percentage;
    ScreenSize current_size;

    int calc_radius();

protected:
    SDL_Surface* build_surface() override;

    void build_texture() override;

public:
    LightOverlay(int percentage);

    SDL_Rect get_rect() override;

    std::shared_ptr<engine::RenderData> render() override;

    int get_radius();

    void set_radius_percentage(int new_percentage);
};

#endif //GAME_LIGHTOVERLAY_H
