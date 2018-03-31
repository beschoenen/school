#ifndef KMINT_MISTER_JANSSEN_WANDERING_STATE_H
#define KMINT_MISTER_JANSSEN_WANDERING_STATE_H


#include "../../board pieces/misses_janssen.h"

namespace kmint
{
    class misses_janssen_wandering_state : public state<misses_janssen>
    {
        int ticker = 0;

    public:
        static misses_janssen_wandering_state* instance();

        void enter(kmint::misses_janssen* misses_janssen) override;

        void leave(kmint::misses_janssen* misses_janssen) override;

        void update(kmint::misses_janssen* misses_janssen) override;
    };
}


#endif //KMINT_MISTER_JANSSEN_WANDERING_STATE_H
