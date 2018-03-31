#ifndef SDL_PROJECT_ENGINE_H
#define SDL_PROJECT_ENGINE_H

#include <vector>
#include "Base/Map.h"
#include "Base/Renderable.h"
#include "Managers/WindowManager.h"
#include "Managers/InputManager.h"
#include "Managers/ImageManager.h"
#include "Managers/FontManager.h"
#include "Managers/SceneManager.h"
#include "Managers/SoundManager.h"
#include "Managers/SaveFileManager.h"
#include "Extensions/FpsCounter.h"

               // ImageManager depends on WindowManager
class Engine : public engine::WindowManager,
               public engine::ImageManager,
               public engine::FontManager,
               public engine::SoundManager,
               public engine::InputManager,
               public engine::SceneManager,
               public engine::SaveFileManager,
               public engine::FpsCounter
{
    SDL_Event event;
    bool is_running = true;

    std::vector<std::shared_ptr<engine::Renderable>> renderables;

    std::shared_ptr<engine::Map> map = nullptr;
    LevelMatrix grid;

    Engine();

    bool will_be_visible(SDL_Rect& rect);

public:
    ~Engine() override;

    static Engine* instance();

    // Loop

    void render();

    void update();

    bool input();

    // Components

    void draw(std::shared_ptr<engine::Renderable> item);

    void erase(std::shared_ptr<engine::Renderable> item);

    // Map

    void render_map();

    void set_map(std::shared_ptr<engine::Map> map_);

    std::shared_ptr<engine::Map> get_map();

    void remove_map();

    // Getters

    SDL_Event get_last_event();

    bool get_is_running();

    void quit();
};

#endif //SDL_PROJECT_ENGINE_H
