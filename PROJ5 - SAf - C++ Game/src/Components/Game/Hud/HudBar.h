#ifndef GAME_HUDBAR_H
#define GAME_HUDBAR_H

#include "../../../Engine/Base/Renderable.h"

class HudBar : public engine::Renderable
{
    int value = 0;
    int max_value = 0;

    SDL_Color bar_color;
    SDL_Color background = {54, 54, 54};

protected:
    SDL_Surface* build_surface() override;

    void build_texture() override;

public:
    HudBar(int x, int y, SDL_Color bar_color, int max_value, Position anchor = POS_TOP_LEFT);

    int get_value();

    void set_value(int value);

    int get_max_value();

    void set_max_value(int max_value);

    SDL_Color get_bar_color();

    void set_bar_color(SDL_Color color);
};


#endif //GAME_HUDBAR_H
