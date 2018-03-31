#ifndef KMINT_STATE_H
#define KMINT_STATE_H

#include <string>

namespace kmint
{
    template <class piece_type>
    class state
    {
    public:
        virtual void enter(piece_type*) = 0;

        virtual void leave(piece_type*) = 0;

        virtual void update(piece_type*) = 0;
    };
}


#endif //KMINT_STATE_H
