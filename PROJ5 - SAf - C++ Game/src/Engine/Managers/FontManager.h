#ifndef GAME_FONTMANAGER_H
#define GAME_FONTMANAGER_H

#include <string>
#include <map>

namespace engine
{
    class FontManager
    {
        std::map<std::string, TTF_Font*> fonts;

        TTF_Font* add_font(std::string& path, int size);

        bool has_font(std::string& path);

        std::string make_key(std::string& path, int size);

    protected:
        FontManager();

        virtual ~FontManager();

    public:
        TTF_Font* load_font(std::string& path, int size);

        void clear_fonts();
    };
}


#endif //GAME_FONTMANAGER_H
