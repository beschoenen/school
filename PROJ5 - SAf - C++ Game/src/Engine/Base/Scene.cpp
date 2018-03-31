#include "Scene.h"

engine::Scene::Scene(std::shared_ptr<Scene> old_scene) : old_scene(std::move(old_scene))
{
}

engine::Scene::Scene() : Scene(nullptr)
{
}

void engine::Scene::input(InputCollection input)
{
    throw "Method not implemented";
}

void engine::Scene::update()
{
    throw "Method not implemented";
}

void engine::Scene::cleanup()
{
    if (old_scene != nullptr)
    {
        old_scene->cleanup();
    }
}

std::shared_ptr<engine::Scene> engine::Scene::get_old_scene() const
{
    return old_scene;
}

void engine::Scene::handle_file_drop(std::string path)
{
    // Do nothing
}
