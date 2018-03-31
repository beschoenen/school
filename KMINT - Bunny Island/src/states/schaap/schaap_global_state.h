#ifndef KMINT_SCHAAP_GLOBAL_STATE_H
#define KMINT_SCHAAP_GLOBAL_STATE_H


#include "../../board pieces/schaap.h"
#include "schaap_wandering_state.h"

namespace kmint
{
    class schaap_global_state : public state<schaap>
    {
        int ticker = 0;

    public:
        static schaap_global_state* instance();

        void enter(kmint::schaap* schaap) override;

        void leave(kmint::schaap* schaap) override;

        void update(kmint::schaap* schaap) override;
    };
}


#endif //KMINT_SCHAAP_GLOBAL_STATE_H
