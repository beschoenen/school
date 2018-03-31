#include <algorithm>
#include "Sound.h"

engine::Sound::Sound(Mix_Chunk* file) : file(file)
{
}

bool engine::Sound::play(int times)
{
    if (file == nullptr) return false;

    if (channel >= 0)
    {
        if (Mix_Paused(channel))
        {
            Mix_Resume(channel);
            return true;
        }
        else if (Mix_Playing(channel)) return false;
    }

    channel = Mix_PlayChannel(-1, file, times);
    if (channel == -1)
    {
        printf("Mix_PlayChannel: %s\n", Mix_GetError());

        return false;
    }

    return true;
}

bool engine::Sound::play_once()
{
    return play(0);
}

bool engine::Sound::pause()
{
    if (Mix_Paused(channel) == 1) return false;

    Mix_Pause(channel);

    return true;
}

bool engine::Sound::stop()
{
    Mix_HaltChannel(channel);

    return true;
}

void engine::Sound::set_volume(int volume)
{
    Mix_Volume(channel, std::max(0, std::min(volume, 100)));
}
