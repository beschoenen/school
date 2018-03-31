#ifndef GAME_ENTITYSTATE_H
#define GAME_ENTITYSTATE_H

namespace engine
{
    class Entity;
}

#include <memory>
#include "Entity.h"

namespace engine
{
    class State
    {
    protected:
        std::weak_ptr<Entity> entity;

        State() = default;

    public:
        virtual ~State() = default;

        virtual void enter_state() {}

        virtual void leave_state() {}

        virtual void update();

        virtual void set_entity(std::shared_ptr<Entity> new_entity);
    };
}


#endif //GAME_ENTITYSTATE_H
