#ifndef KMINT_SCHAAP_HUNTING_STATE_H
#define KMINT_SCHAAP_HUNTING_STATE_H


#include "../base/state.h"
#include "../../board pieces/schaap.h"
#include "../../astar.h"

namespace kmint
{
    class schaap_hunting_state : public state<schaap>
    {
        int ticker;
        int last_known_location = -1;

        kmint::astar* astar;
        std::vector<node_id> nodes = std::vector<node_id>();

    public:
        static schaap_hunting_state* instance();

        void enter(kmint::schaap* schaap) override;

        void leave(kmint::schaap* schaap) override;

        void update(kmint::schaap* schaap) override;
    };
}


#endif //KMINT_SCHAAP_HUNTING_STATE_H
