#include "entities.h"

namespace kmint
{
    entities* entities::instance()
    {
        static entities instance;

        return &instance;
    }
}
