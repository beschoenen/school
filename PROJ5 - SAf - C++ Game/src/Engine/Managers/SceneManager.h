#ifndef GAME_SCENEMANAGER_H
#define GAME_SCENEMANAGER_H

#include "../Base/Scene.h"

namespace engine
{
    class SceneManager
    {
    protected:
        std::shared_ptr<Scene> current_scene = nullptr;

        virtual ~SceneManager();

        void update();

        void input(std::map<Buttons, bool> input);

    public:
        template<class T>
        void go_to_scene(bool clean = false);

        void exit_current_scene(std::shared_ptr<Scene> old_scene);
    };

    template<class T>
    void engine::SceneManager::go_to_scene(bool clean)
    {
        if (current_scene != nullptr)
        {
            current_scene->leave_scene();
        }

        if (clean)
        {
            current_scene = nullptr;
        }

        auto new_scene = std::shared_ptr<Scene>(new T(current_scene));
        current_scene = new_scene;

        if (current_scene != nullptr)
        {
            current_scene->enter_scene();
        }
    }
}

#endif //GAME_SCENEMANAGER_H
