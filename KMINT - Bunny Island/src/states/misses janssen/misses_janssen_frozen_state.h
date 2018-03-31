#ifndef KMINT_MISSES_JANSSEN_FROZEN_STATE_H
#define KMINT_MISSES_JANSSEN_FROZEN_STATE_H


#include "../../board pieces/misses_janssen.h"

namespace kmint
{
    class misses_janssen_frozen_state : public state<misses_janssen>
    {
    public:
        static misses_janssen_frozen_state* instance();

        void enter(kmint::misses_janssen* misses_janssen) override;

        void leave(kmint::misses_janssen* misses_janssen) override;

        void update(kmint::misses_janssen* misses_janssen) override;
    };
}


#endif //KMINT_MISSES_JANSSEN_FROZEN_STATE_H
