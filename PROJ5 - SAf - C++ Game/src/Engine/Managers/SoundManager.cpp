#include <iostream>
#include <SDL.h>
#include "SoundManager.h"

#define SDL_Flags (SDL_INIT_AUDIO)

engine::SoundManager::SoundManager()
{
    if (SDL_InitSubSystem(SDL_Flags) != 0)
    {
        printf("SDL_Init: %s\n", SDL_GetError());
        exit(1);
    }

    if (Mix_OpenAudio(MIX_DEFAULT_FREQUENCY, MIX_DEFAULT_FORMAT, 2, 512) < 0)
    {
        std::cout << "Mix_OpenAudio: " << Mix_GetError() << std::endl;
    }
}

engine::SoundManager::~SoundManager()
{
    clear_sounds();

    SDL_QuitSubSystem(SDL_Flags);
}

Mix_Chunk* engine::SoundManager::add_sound(std::string& path)
{
    if (has_sound(path))
    {
        return sounds.find(path)->second;
    }

    auto resource = Mix_LoadWAV(path.c_str());

    if (resource == nullptr)
    {
        std::cerr << "Could not load music from " << path << " " << Mix_GetError() << std::endl;
        exit(1);
    }

    sounds.insert(std::pair<std::string, Mix_Chunk*>(path, resource));

    return resource;
}

bool engine::SoundManager::has_sound(std::string& path)
{
    auto it = sounds.find(path);

    return it != sounds.end() && it->second != nullptr;
}

engine::Sound engine::SoundManager::load_sound(const char* path)
{
    std::string str(path);

    return load_sound(str);
}

engine::Sound engine::SoundManager::load_sound(std::string& path)
{
    if (!has_sound(path))
    {
        return Sound(add_sound(path));
    }

    return Sound(sounds.find(path)->second);
}

void engine::SoundManager::clear_sounds()
{
    for (const auto& item : sounds)
    {
        Mix_FreeChunk(item.second);
    }

    sounds.clear();
}
