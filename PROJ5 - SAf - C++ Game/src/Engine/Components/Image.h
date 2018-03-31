#ifndef SDL_PROJECT_C_IMAGE_H
#define SDL_PROJECT_C_IMAGE_H

#include "../Base/Renderable.h"
#include <string>

namespace engine
{
    class Image : public Renderable
    {
    protected:
        std::string image;

        SDL_RendererFlip flip;

        double angle = 0;

        Image(std::string image, int x, int y, int width, int height);

        Image(std::string image, int x, int y);

        SDL_Surface* build_surface() override;

        void build_texture() override;

    public:
        std::shared_ptr<engine::RenderData> render() override;

        std::string get_image();

        void set_image(std::string path);

        SDL_RendererFlip get_flip();

        void set_flip(SDL_RendererFlip flip);

        double get_angle();

        void set_angle(double new_angle);
    };
}

#endif //SDL_PROJECT_C_IMAGE_H
