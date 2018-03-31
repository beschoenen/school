#include <SDL_image.h>
#include <iostream>
#include "ImageManager.h"

engine::ImageManager::ImageManager(SDL_Renderer* renderer) : renderer(renderer)
{
    if (IMG_Init(IMG_INIT_PNG) != IMG_INIT_PNG)
    {
        printf("IMG_Init: %s\n", IMG_GetError());
        exit(1);
    }
}

engine::ImageManager::~ImageManager()
{
    clear_images();

    IMG_Quit();
}

SDL_Texture* engine::ImageManager::add_image(std::string& path)
{
    if (has_image(path))
    {
        return images.find(path)->second;
    }

    auto resource = IMG_LoadTexture(renderer, path.c_str());

    if (resource == nullptr)
    {
        std::cerr << "Could not create texture from " << path << std::endl;
        exit(1);
    }

    images.insert(std::pair<std::string, SDL_Texture*>(path, resource));

    return resource;
}

bool engine::ImageManager::has_image(std::string& path)
{
    auto it = images.find(path);

    return it != images.end() && it->second != nullptr;
}

SDL_Texture* engine::ImageManager::load_image(std::string& path)
{
    if (!has_image(path))
    {
        return add_image(path);
    }

    return images.find(path)->second;
}

SDL_Texture* engine::ImageManager::load_image(const char* path)
{
    std::string str(path);

    return load_image(str);
}

void engine::ImageManager::clear_images()
{
    for (const auto& image : images)
    {
        SDL_DestroyTexture(image.second);
    }

    images.clear();
}
