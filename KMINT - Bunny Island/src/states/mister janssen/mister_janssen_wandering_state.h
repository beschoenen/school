#ifndef KMINT_MISTER_JANSSEN_WANDERING_STATE_H
#define KMINT_MISTER_JANSSEN_WANDERING_STATE_H


#include "../../board pieces/mister_janssen.h"

namespace kmint
{
    class mister_janssen_wandering_state : public state<mister_janssen>
    {
        int ticker = 0;

    public:
        static mister_janssen_wandering_state* instance();

        void enter(kmint::mister_janssen* mister_janssen) override;

        void leave(kmint::mister_janssen* mister_janssen) override;

        void update(kmint::mister_janssen* mister_janssen) override;
    };
}


#endif //KMINT_MISTER_JANSSEN_WANDERING_STATE_H
