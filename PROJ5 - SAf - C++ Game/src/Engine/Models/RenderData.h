#ifndef SDL_PROJECT_RENDER_H
#define SDL_PROJECT_RENDER_H

#include <SDL_render.h>

namespace engine
{
    class RenderData
    {
    public:
        SDL_Texture* texture;
        SDL_Rect* src_rect;

        SDL_RendererFlip flip;
        double angle;

        RenderData(SDL_Texture* texture, SDL_Rect* src_rect, SDL_RendererFlip flip, double angle);
        RenderData(SDL_Texture* texture, SDL_Rect* src_rect, SDL_RendererFlip flip);
        RenderData(SDL_Texture* texture, SDL_RendererFlip flip, double angle);
        RenderData(SDL_Texture* texture, SDL_RendererFlip flip);
        RenderData(SDL_Texture* texture, SDL_Rect* src_rect);
        explicit RenderData(SDL_Texture* texture);

        ~RenderData();
    };
}


#endif //SDL_PROJECT_RENDER_H
