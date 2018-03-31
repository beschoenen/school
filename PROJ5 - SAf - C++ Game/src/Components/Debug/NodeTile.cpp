#include <iostream>
#include "NodeTile.h"
#include "SDL_ttf.h"
#include "../../Engine/Engine.h"
#include <sstream>
#include <iomanip>

NodeTile::NodeTile(const std::shared_ptr<engine::Node> node, const bool closed, const bool final, const int step) :
    engine::Renderable(node->get_column(), node->get_row(), TILE_SIZE, TILE_SIZE, true),
    node_(node), closed(closed), final(final), step(step)
{
    NodeTile::build_texture();
}

NodeTile::NodeTile(const std::shared_ptr<engine::Node> node) : NodeTile(node, false, false, -1)
{
}

NodeTile::~NodeTile()
{
    SDL_DestroyTexture(texture);
}

SDL_Surface* NodeTile::build_surface()
{
    auto surface = SDL_CreateRGBSurfaceWithFormat(0, width, height, 32, SDL_PIXELFORMAT_RGBA32);
    if (surface == nullptr)
    {
        std::cerr << "Could not create surface" << std::endl;
        exit(1);
    }

    if (closed)
    {
        SDL_Color border = {0, 191, 255, 255}; //blue

        if (final)
        {
            border = {0, 100, 0, 255}; //green
        }
        else if (node_->get_h_cost() <= 0)
        {
            border = {255, 0, 0, 255}; //red
        }

        SDL_Rect rect = {3, 3, width - 6, height - 6};

        SDL_FillRect(surface, nullptr, SDL_MapRGBA(surface->format, border.r, border.g, border.b, border.a));
        SDL_FillRect(surface, &rect, SDL_MapRGBA(surface->format, 0, 0, 0, 0));
    }

    auto font_name = std::string(FONT);
    const auto font = Engine::instance()->load_font(font_name, 10);

    const auto step_text = TTF_RenderText_Solid(font, format_text(step).c_str(), {0, 0, 0, 0});
    const auto fscore_text = TTF_RenderText_Solid(font, format_text(node_->get_f_score()).c_str(), {0, 255, 0, 0});
    const auto gcost_text = TTF_RenderText_Solid(font, format_text(node_->get_g_cost()).c_str(), {255, 0, 0, 0});
    const auto hcost_text = TTF_RenderText_Solid(font, format_text(node_->get_h_cost()).c_str(), {0, 0, 255, 0});

    SDL_Rect step_rect = {width - step_text->w - 5, 5, 0, 0};
    SDL_Rect score_rect = {5, 5, 0, 0};
    SDL_Rect gcost_rect = {5, height - gcost_text->h - 5, 0, 0};
    SDL_Rect hcost_rect = {width - hcost_text->w - 5, height - hcost_text->h - 5, 0, 0};

    if (step != -1)
    {
        SDL_BlitSurface(step_text, nullptr, surface, &step_rect);
    }

    SDL_BlitSurface(fscore_text, nullptr, surface, &score_rect);
    SDL_BlitSurface(gcost_text, nullptr, surface, &gcost_rect);
    SDL_BlitSurface(hcost_text, nullptr, surface, &hcost_rect);

    SDL_FreeSurface(step_text);
    SDL_FreeSurface(fscore_text);
    SDL_FreeSurface(gcost_text);
    SDL_FreeSurface(hcost_text);

    return surface;
}

void NodeTile::build_texture()
{
    const auto surface = build_surface();

    SDL_DestroyTexture(texture);

    texture = SDL_CreateTextureFromSurface(Engine::instance()->get_renderer(), surface);

    SDL_FreeSurface(surface);
}

std::string NodeTile::format_text(const int value)
{
    std::stringstream ss;
    ss << std::fixed << std::setprecision(2) << value;
    return ss.str();
}

std::string NodeTile::format_text(const double value)
{
    std::stringstream ss;
    ss << std::fixed << std::setprecision(2) << value;
    return ss.str();
}
