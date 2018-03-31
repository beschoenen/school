#include <iostream>
#include <SDL_ttf.h>
#include "FontManager.h"

engine::FontManager::FontManager()
{
    if (TTF_Init() != 0)
    {
        printf("TTF_Init: %s\n", TTF_GetError());
        exit(1);
    }
}

engine::FontManager::~FontManager()
{
    clear_fonts();

    TTF_Quit();
}

std::string engine::FontManager::make_key(std::string& path, int size)
{
    return path + std::to_string(size);
}

TTF_Font* engine::FontManager::add_font(std::string& path, int size)
{
    auto key = make_key(path, size);

    if (has_font(key))
    {
        return fonts.find(key)->second;
    }

    auto resource = TTF_OpenFont(path.c_str(), size);

    if (resource == nullptr)
    {
        std::cerr << "Could not load font from " << path << std::endl;
        exit(1);
    }

    fonts.insert(std::pair<std::string, TTF_Font*>(key, resource));

    return resource;
}

bool engine::FontManager::has_font(std::string& key)
{
    auto it = fonts.find(key);

    return it != fonts.end() && it->second != nullptr;
}

TTF_Font* engine::FontManager::load_font(std::string& path, int size)
{
    auto key = make_key(path, size);

    if (!has_font(key))
    {
        return add_font(path, size);
    }

    return fonts.find(key)->second;
}

void engine::FontManager::clear_fonts()
{
    for (const auto& font : fonts)
    {
        TTF_CloseFont(font.second);
    }

    fonts.clear();
}
