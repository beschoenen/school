#include "RenderData.h"

engine::RenderData::RenderData(SDL_Texture* texture, SDL_Rect* src_rect, SDL_RendererFlip flip, double angle) :
    texture(texture), src_rect(src_rect), flip(flip), angle(angle)
{
}

engine::RenderData::RenderData(SDL_Texture* texture, SDL_Rect* src_rect, SDL_RendererFlip flip) :
    RenderData(texture, src_rect, flip, 0.0)
{
}

engine::RenderData::RenderData(SDL_Texture* texture, SDL_RendererFlip flip, double angle) :
    RenderData(texture, nullptr, flip, angle)
{
}

engine::RenderData::RenderData(SDL_Texture* texture, SDL_RendererFlip flip) :
    RenderData(texture, nullptr, flip)
{
}

engine::RenderData::RenderData(SDL_Texture* texture, SDL_Rect* src_rect) : RenderData(texture, src_rect, SDL_FLIP_NONE)
{
}

engine::RenderData::RenderData(SDL_Texture* texture) : RenderData(texture, nullptr)
{
}

engine::RenderData::~RenderData()
{
    delete src_rect;
}
