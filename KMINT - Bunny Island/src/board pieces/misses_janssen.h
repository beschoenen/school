#ifndef KMINT_MISSES_JANSSEN_H
#define KMINT_MISSES_JANSSEN_H


#include "../graph.h"
#include "base/person.h"

namespace kmint
{
    class misses_janssen : public person
    {
        state_machine<misses_janssen>* _state_machine;

    public:
        explicit misses_janssen(graph* graph);

        ~misses_janssen() override;

        void update(float delta_time) override;

        kmint::state_machine<misses_janssen>* get_state_machine();

        int give_water() override;
    };
}


#endif //KMINT_MISSES_JANSSEN_H
