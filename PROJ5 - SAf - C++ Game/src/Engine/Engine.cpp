#include <iostream>
#include <utility>
#include "Engine.h"
#include "../Utilities/File.h"
#include <algorithm>

static Engine* instance_;

Engine::Engine() : engine::WindowManager(),
                   engine::FontManager(),
                   engine::ImageManager(engine::WindowManager::renderer),
                   engine::SoundManager(),
                   engine::InputManager(),
                   engine::SceneManager(),
                   engine::SaveFileManager(),
                   engine::FpsCounter()
{
    if (!File::has_settings_directory())
    {
        File::create_settings_directory();
    }

    button_click_effect = load_sound(SOUND_BUTTONCLICK);
}

Engine::~Engine()
{
    if (map != nullptr) remove_map();
}

Engine* Engine::instance()
{
    if (!instance_)
    {
        instance_ = new Engine();
    }

    return instance_;
}

void Engine::render()
{
    SDL_SetRenderDrawColor(engine::WindowManager::renderer, bgcolor.r, bgcolor.g, bgcolor.b, bgcolor.a);
    SDL_RenderClear(engine::WindowManager::renderer);

    if (map != nullptr) render_map();

    auto size = get_size();

    // Sort
    std::sort(renderables.begin(), renderables.end(),
              [](std::shared_ptr<engine::Renderable> l, std::shared_ptr<engine::Renderable> r) {
                  if (l->get_layer() != r->get_layer())
                  {
                      return l->get_layer() < r->get_layer();
                  }

                  return l->get_y() < r->get_y();
              });

    // Render
    for (const auto& it : renderables)
    {
        if (!it->will_be_visible()) continue;

        auto rect = it->get_rect();
        auto render = it->render();

        SDL_RenderCopyEx(
            engine::WindowManager::renderer,
            render->texture,
            render->src_rect,
            &rect,
            render->angle,
            nullptr,
            render->flip
        );
    }

    SDL_RenderPresent(engine::WindowManager::renderer);
}

void Engine::update()
{
    SceneManager::update();

    FpsCounter::tick();
}

bool Engine::input()
{
    while (SDL_PollEvent(&event) != 0)
    {
        if (event.type == SDL_QUIT) // Exit
        {
            return false;
        }
        else if (event.type == SDL_WINDOWEVENT && event.window.event == SDL_WINDOWEVENT_SIZE_CHANGED) // Change window size
        {
            update_size(event.window);
        }
        else if (event.type == SDL_DROPFILE) // Drop a file
        {
            current_scene->handle_file_drop(event.drop.file);
        }
        else if (event.type == SDL_KEYUP && event.key.keysym.sym == SDLK_COMMA) // Toggle FPS
        {
            FpsCounter::toggle();
        }

        handle_event(event);

        engine::SceneManager::input(get_input());
    }

    return true;
}

void Engine::draw(std::shared_ptr<engine::Renderable> item)
{
    if (auto button = std::dynamic_pointer_cast<engine::Button>(item))
    {
        register_callback(button.get());
    }

    renderables.push_back(item);
}

void Engine::erase(std::shared_ptr<engine::Renderable> item)
{
    if (auto button = std::dynamic_pointer_cast<engine::Button>(item))
    {
        remove_callback(button.get());
    }

    renderables.erase(std::remove(renderables.begin(), renderables.end(), item), renderables.end());
}

void Engine::render_map()
{
    int x = 0;
    int y = 0;

    auto total_width = map->get_total_width();
    auto total_height = map->get_total_height();

    auto size = get_size();

    for (const auto& row : grid)
    {
        auto center_y = -(total_height / 2) + (size.h / 2) + (y * TILE_SIZE) - map->y;

        x = 0;

        for (const auto& tile : row)
        {
            auto center_x = -(total_width / 2) + (size.w / 2) + (x * TILE_SIZE) - map->x;

            SDL_Rect rect{center_x, center_y, TILE_SIZE, TILE_SIZE};

            // Only draw items that will be visible on screen
            if (will_be_visible(rect))
            {
                SDL_RenderCopy(
                    engine::WindowManager::renderer,
                    tile->get_grid_tile_type() == GridTileTypes::wall
                    ? map->get_border_texture() : map->get_path_texture(),
                    nullptr,
                    &rect
                );
            }

            x++;
        }

        y++;
    }
}

void Engine::set_map(std::shared_ptr<engine::Map> map_)
{
    map = std::move(map_);
    grid = map->get_map();
}

std::shared_ptr<engine::Map> Engine::get_map()
{
    return map;
}

void Engine::remove_map()
{
    grid.clear();
    map = nullptr;
}

bool Engine::get_is_running()
{
    return is_running;
}

void Engine::quit()
{
    is_running = false;
}

SDL_Event Engine::get_last_event()
{
    return event;
}

bool Engine::will_be_visible(SDL_Rect& rect)
{
    auto size = get_size();

    return !(rect.y + rect.h < 0 ||
             rect.y > size.h ||
             rect.x + rect.w < 0 ||
             rect.x > size.w);
}
