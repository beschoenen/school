#ifndef GAME_SOUND_H
#define GAME_SOUND_H

#include <SDL_mixer.h>

namespace engine
{
    class Sound
    {
        int channel;
        Mix_Chunk* file = nullptr;

    public:
        explicit Sound(Mix_Chunk* file);

        bool play(int times = -1);

        bool play_once();

        bool pause();

        bool stop();

        void set_volume(int volume);
    };
}


#endif //GAME_SOUND_H
