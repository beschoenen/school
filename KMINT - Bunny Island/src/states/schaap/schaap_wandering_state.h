#ifndef KMINT_SCHAAP_WANDERING_STATE_H
#define KMINT_SCHAAP_WANDERING_STATE_H


#include "../../board pieces/schaap.h"

namespace kmint
{
    class schaap_wandering_state : public state<kmint::schaap>
    {
        int ticker = 0;

    public:
        static schaap_wandering_state* instance();

        void enter(kmint::schaap* schaap) override;

        void leave(kmint::schaap* schaap) override;

        void update(kmint::schaap* schaap) override;
    };
}


#endif //KMINT_SCHAAP_WANDERING_STATE_H
