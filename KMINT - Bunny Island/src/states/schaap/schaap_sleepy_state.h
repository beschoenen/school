#ifndef KMINT_SCHAAP_SLEEPY_STATE_H
#define KMINT_SCHAAP_SLEEPY_STATE_H


#include "../../board pieces/schaap.h"
#include "../../board pieces/cave.h"

namespace kmint
{
    class schaap_sleepy_state : public state<schaap>
    {
        int ticker = 0;

        std::vector<node_id> nodes = std::vector<node_id>();

    public:
        static schaap_sleepy_state* instance();

        void enter(kmint::schaap* schaap) override;

        void leave(kmint::schaap* schaap) override;

        void update(kmint::schaap* schaap) override;
    };
}


#endif //KMINT_SCHAAP_SLEEPY_STATE_H
