#ifndef KMINT_SCHAAP_THIRSTY_STATE_H
#define KMINT_SCHAAP_THIRSTY_STATE_H


#include "../../board pieces/schaap.h"
#include "../../board pieces/base/person.h"

namespace kmint
{
    class schaap_thirsty_state : public state<schaap>
    {
        int ticker = 0;

        kmint::person* target;
        std::vector<node_id> nodes = std::vector<node_id>();

    public:
        static schaap_thirsty_state* instance();

        void enter(kmint::schaap* schaap) override;

        void leave(kmint::schaap* schaap) override;

        void update(kmint::schaap* schaap) override;
    };
}


#endif //KMINT_SCHAAP_THIRSTY_STATE_H
