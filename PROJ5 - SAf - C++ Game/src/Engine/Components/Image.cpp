#include <string>
#include "Image.h"
#include "../Engine.h"

engine::Image::Image(std::string image, int x, int y, int width, int height) :
    Renderable(x, y, width, height),
    image(std::move(image)), flip(SDL_FLIP_NONE)
{
    build_texture();
}

engine::Image::Image(std::string image, int x, int y) : Image(std::move(image), x, y, -1, -1)
{
}

SDL_Surface* engine::Image::build_surface()
{
    return nullptr;
}

void engine::Image::build_texture()
{
    texture = Engine::instance()->load_image(image);
}

std::shared_ptr<engine::RenderData> engine::Image::render()
{
    return std::shared_ptr<RenderData>(new RenderData(texture, flip, angle));
}

std::string engine::Image::get_image()
{
    return image;
}

void engine::Image::set_image(std::string path)
{
    image = std::move(path);
    build_texture();
}

SDL_RendererFlip engine::Image::get_flip()
{
    return flip;
}

void engine::Image::set_flip(SDL_RendererFlip image_flip)
{
    flip = image_flip;
}

double engine::Image::get_angle()
{
    return angle;
}

void engine::Image::set_angle(double new_angle)
{
    angle = new_angle;
}
