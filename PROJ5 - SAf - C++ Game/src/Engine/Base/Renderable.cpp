#include "Renderable.h"
#include "../Engine.h"

engine::Renderable::Renderable(int x, int y, int width, int height, bool tile_based) :
    x(x), y(y), width(width), height(height), tile_based(tile_based)
{
}

engine::Renderable::Renderable(int x, int y, bool tile_based) : Renderable(x, y, -1, -1, tile_based)
{
}

SDL_Surface* engine::Renderable::build_surface()
{
    throw "Method not implemented";
}

void engine::Renderable::build_texture()
{
    throw "Method not implemented";
}

std::shared_ptr<engine::RenderData> engine::Renderable::render()
{
    return std::shared_ptr<RenderData>(new RenderData(texture));
}

int engine::Renderable::get_width()
{
    return width;
}

void engine::Renderable::set_width(int w)
{
    width = w;
    build_texture();
}

int engine::Renderable::get_height()
{
    return height;
}

void engine::Renderable::set_height(int h)
{
    height = h;
    build_texture();
}

int engine::Renderable::get_x()
{
    return x;
}

void engine::Renderable::set_x(int _x)
{
    x = _x;
}

int engine::Renderable::get_y()
{
    return y;
}

void engine::Renderable::set_y(int _y)
{
    y = _y;
}

bool engine::Renderable::get_tile_based()
{
    return tile_based;
}

bool engine::Renderable::will_be_visible()
{
    auto size = Engine::instance()->get_size();

    if (width < 0 || height < 0)
    {
        SDL_QueryTexture(texture, nullptr, nullptr, &calculated_width, &calculated_height);
    }

    auto rect = get_rect();

    return !(rect.y + rect.h < 0 ||
             rect.y > size.h ||
             rect.x + rect.w < 0 ||
             rect.x > size.w);
}

Position engine::Renderable::get_anchor()
{
    return anchor;
}

void engine::Renderable::set_anchor(Position pos)
{
    anchor = pos;
}

bool engine::Renderable::get_y_sorted()
{
    return y_sorted;
}

void engine::Renderable::set_y_sorted(bool sorted)
{
    y_sorted = sorted;
}

DrawLayer engine::Renderable::get_layer()
{
    return layer;
}

void engine::Renderable::set_layer(DrawLayer new_layer)
{
    layer = new_layer;
}

SDL_Rect engine::Renderable::get_rect()
{
    SDL_Rect rect =
    {
        x,
        y,
        width > 0 ? width : calculated_width,
        height > 0 ? height : calculated_height
    };

    if (get_tile_based())
    {
        auto map = Engine::instance()->get_map();
        auto size = Engine::instance()->get_size();

        rect.x = -(map->get_total_width() / 2) + (size.w / 2) + (rect.x * TILE_SIZE) - map->x;
        rect.y = -(map->get_total_height() / 2) + (size.h / 2) + (rect.y * TILE_SIZE) - map->y;

        return rect;
    }

    return transform_on_anchor(rect);
}

SDL_Rect engine::Renderable::transform_on_anchor(SDL_Rect rect)
{
    switch (get_anchor())
    {
        case POS_TOP_CENTER:
            rect.x += Engine::instance()->get_size().w / 2 - rect.w / 2;
            break;
        case POS_TOP_RIGHT:
            rect.x += Engine::instance()->get_size().w - rect.w;
            break;
        case POS_CENTER_LEFT:
            rect.y += Engine::instance()->get_size().h / 2 - rect.h / 2;
            break;
        case POS_CENTER_CENTER:
            rect.x += Engine::instance()->get_size().w / 2 - rect.w / 2;
            rect.y += Engine::instance()->get_size().h / 2 - rect.h / 2;
            break;
        case POS_CENTER_RIGHT:
            rect.x += Engine::instance()->get_size().w - rect.w;
            rect.y += Engine::instance()->get_size().h / 2 - rect.h / 2;
            break;
        case POS_BOTTOM_LEFT:
            rect.y += Engine::instance()->get_size().h - rect.h;
            break;
        case POS_BOTTOM_CENTER:
            rect.x += Engine::instance()->get_size().w / 2 - rect.w / 2;
            rect.y += Engine::instance()->get_size().h - rect.h;
            break;
        case POS_BOTTOM_RIGHT:
            rect.x += Engine::instance()->get_size().w - rect.w;
            rect.y += Engine::instance()->get_size().h - rect.h;
            break;
        case POS_TOP_LEFT:
        default:
            break;
    }

    return rect;
}
