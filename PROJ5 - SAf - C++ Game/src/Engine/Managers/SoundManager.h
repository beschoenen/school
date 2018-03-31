#ifndef GAME_SOUNDMANAGER_H
#define GAME_SOUNDMANAGER_H

#include <SDL_mixer.h>
#include <string>
#include <map>
#include "../Models/Sound.h"

namespace engine
{
    class SoundManager
    {
        std::map<std::string, Mix_Chunk*> sounds;

        Mix_Chunk* add_sound(std::string& path);

        bool has_sound(std::string& path);

    protected:
        SoundManager();

        virtual ~SoundManager();

    public:
        Sound load_sound(const char* path);

        engine::Sound load_sound(std::string& path);

        void clear_sounds();
    };
}


#endif //GAME_SOUNDMANAGER_H
