#include "State.h"

void engine::State::update()
{
    throw "Method not Implemented";
}

void engine::State::set_entity(std::shared_ptr<engine::Entity> new_entity)
{
    entity = std::move(new_entity);
}
