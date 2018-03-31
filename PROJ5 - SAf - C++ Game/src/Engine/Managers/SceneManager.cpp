#include "SceneManager.h"

engine::SceneManager::~SceneManager()
{
    if (current_scene != nullptr)
    {
        current_scene->cleanup();
    }
}

void engine::SceneManager::exit_current_scene(std::shared_ptr<engine::Scene> old_scene)
{
    if (old_scene != nullptr)
    {
        current_scene->leave_scene();
        current_scene = old_scene;
        current_scene->enter_scene();
    }
}

void engine::SceneManager::update()
{
    if (current_scene != nullptr)
    {
        current_scene->update();
    }
}

void engine::SceneManager::input(std::map<engine::Buttons, bool> input)
{
    if (current_scene != nullptr)
    {
        current_scene->input(std::move(input));
    }
}
