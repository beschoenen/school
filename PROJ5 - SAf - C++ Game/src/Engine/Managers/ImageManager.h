#ifndef GAME_RESOURCEMANAGER_H
#define GAME_RESOURCEMANAGER_H

#include <string>
#include <SDL_render.h>
#include <map>

namespace engine
{
    class ImageManager
    {
        SDL_Renderer* renderer = nullptr;

        std::map<std::string, SDL_Texture*> images;

        SDL_Texture* add_image(std::string& path);

        bool has_image(std::string& path);

    protected:
        explicit ImageManager(SDL_Renderer* renderer);

        virtual ~ImageManager();

    public:
        SDL_Texture* load_image(const char* path);

        SDL_Texture* load_image(std::string& path);

        void clear_images();
    };
}


#endif //GAME_RESOURCEMANAGER_H
