#ifndef KMINT_MISTER_JANSSEN_H
#define KMINT_MISTER_JANSSEN_H


#include "base/person.h"

namespace kmint
{
    class mister_janssen : public person
    {
        state_machine<mister_janssen>* _state_machine;

    public:
        explicit mister_janssen(graph* graph);

        ~mister_janssen() override;

        void update(float delta_time) override;

        kmint::state_machine<mister_janssen>* get_state_machine();

        int give_water() override;
    };
}


#endif //KMINT_MISTER_JANSSEN_H
