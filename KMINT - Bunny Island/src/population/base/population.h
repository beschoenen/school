#ifndef KMINT_POPULATION_H
#define KMINT_POPULATION_H


#include <vector>
#include "../../models/point.h"

namespace kmint
{
    enum entity_state
    {
        alive,
        drowned,
        eaten
    };

    template<class race>
    class population
    {
    protected:
        std::vector<race*> individuals;

    public:
        virtual std::vector<race*> get_individuals() const { return individuals; }
    };
}


#endif //KMINT_POPULATION_H
