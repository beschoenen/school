#ifndef SDL_PROJECT_TEXT_H
#define SDL_PROJECT_TEXT_H

#include <string>
#include <SDL_pixels.h>
#include <SDL_ttf.h>
#include "../Base/Renderable.h"

namespace engine
{
    class Text : public Renderable
    {
    protected:
        std::string text;
        SDL_Color color;

        std::string font_name;
        int font_size;

        TTF_Font* font;

        Text(std::string text, std::string font_name, int x, int y, int width, int height, SDL_Color color, int font_size);

        Text(std::string text, std::string font_name, int x, int y, SDL_Color color, int font_size);

        SDL_Surface* build_surface() override;

        void build_texture() override;

    public:
        ~Text();

        // Getters/setters

        std::string get_text();

        virtual void set_text(std::string string);

        void set_font(std::string font_name, int size);

        SDL_Color get_color();

        void set_color(SDL_Color c);
    };
}


#endif //SDL_PROJECT_TEXT_H
