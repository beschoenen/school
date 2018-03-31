#ifndef SDL_PROJECT_BUTTON_H
#define SDL_PROJECT_BUTTON_H

#include "Text.h"
#include <functional>

namespace engine
{
    class Button : public Text
    {
    protected:
        int text_padding;
        int border_size;

        SDL_Color background;
        SDL_Rect button_rect;

        std::function<void()> callback;

        Button(std::string text, std::function<void()> callback, int x, int y, int width, int height,
               SDL_Color foreground, SDL_Color background, int font_size, int text_padding, int border_size);

        Button(std::string text, std::function<void()> callback, int x, int y, SDL_Color foreground,
               SDL_Color background, int font_size, int text_padding, int border_size);

        SDL_Surface* build_surface() override;

        void build_texture() override;

    public:
        virtual void click();

        // Getters / setters

        int get_padding();

        void set_padding(int padding);

        int get_border_width();

        void set_border_width(int size);

        SDL_Color get_background_color();

        void set_background_color(SDL_Color color);

        SDL_Rect get_rect() override;
    };
}

#endif //SDL_PROJECT_BUTTON_H
