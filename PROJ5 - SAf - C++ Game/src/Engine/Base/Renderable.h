#ifndef SDL_PROJECT_RENDERABLE_H
#define SDL_PROJECT_RENDERABLE_H

#include "../Models/RenderData.h"
#include "../../defines.h"
#include <memory>

namespace engine
{
    class Renderable
    {
    protected:
        SDL_Texture* texture = nullptr;

        int x;
        int y;

        int width;
        int height;

        int calculated_width = -1;
        int calculated_height = -1;

        Position anchor = POS_TOP_LEFT;

        bool tile_based = false;
        bool y_sorted = false;
        DrawLayer layer = ENTITY_LAYER;

        Renderable(int x, int y, int width, int height, bool tile_based = false);

        Renderable(int x, int y, bool tile_based = false);

        virtual SDL_Surface* build_surface();

        virtual void build_texture();

        SDL_Rect transform_on_anchor(SDL_Rect rect);

    public:
        virtual std::shared_ptr<engine::RenderData> render();

        bool will_be_visible();

        virtual SDL_Rect get_rect();

        bool get_tile_based();

        // Getters/setters

        int get_width();

        void set_width(int w);

        int get_height();

        void set_height(int h);

        int get_x();

        void set_x(int _x);

        int get_y();

        void set_y(int _y);

        Position get_anchor();

        void set_anchor(Position pos);

        bool get_y_sorted();

        void set_y_sorted(bool sorted);

        DrawLayer get_layer();

        void set_layer(DrawLayer new_layer);
    };
}

#endif //SDL_PROJECT_RENDERABLE_H
