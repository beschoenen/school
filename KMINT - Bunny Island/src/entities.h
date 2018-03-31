#ifndef KMINT_ENTITIES_H
#define KMINT_ENTITIES_H

#include "board pieces/mister_janssen.h"
#include "board pieces/misses_janssen.h"
#include "board pieces/schaap.h"
#include "population/rabbit_population.h"
#include "board pieces/cave.h"

namespace kmint
{
    class entities
    {
    public:
        kmint::mister_janssen* mister_janssen;
        kmint::misses_janssen* misses_janssen;
        kmint::schaap* schaap;
        kmint::cave* cave;
        kmint::rabbit_population* rabbit_population;
        kmint::graph* graph;

        static entities* instance();
    };
}


#endif //KMINT_ENTITIES_H
