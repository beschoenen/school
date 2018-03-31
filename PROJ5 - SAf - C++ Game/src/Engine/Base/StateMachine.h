#ifndef GAME_STATEMACHINE_H
#define GAME_STATEMACHINE_H

#include <memory>

namespace engine
{
    class State;
}

namespace engine
{
    class StateMachine
    {
        std::shared_ptr<State> global_state;
        std::shared_ptr<State> current_state;

    public:
        StateMachine();

        std::shared_ptr<State> get_global_state() const;

        std::shared_ptr<State> get_current_state() const;

        void set_global_state(std::shared_ptr<State> global_state);

        void change_state(std::shared_ptr<State> new_state);

        void update() const;
    };
}


#endif //GAME_STATEMACHINE_H
