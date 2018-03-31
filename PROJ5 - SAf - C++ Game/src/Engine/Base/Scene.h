#ifndef SDL_PROJECT_STATE_H
#define SDL_PROJECT_STATE_H

#include "../../aliases.h"

namespace engine
{
    class Scene
    {
    protected:
        std::shared_ptr<Scene> old_scene;

        Scene();

        explicit Scene(std::shared_ptr<Scene> old_scene);

    public:
        virtual ~Scene() = default;

        virtual void enter_scene() {}

        virtual void leave_scene() {}

        virtual void update();

        virtual void input(InputCollection input);

        virtual void handle_file_drop(std::string path);

        void cleanup();

        std::shared_ptr<Scene> get_old_scene() const;
    };
}

#endif //SDL_PROJECT_STATE_H
