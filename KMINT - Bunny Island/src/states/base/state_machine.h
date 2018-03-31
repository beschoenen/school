#ifndef KMINT_STATE_MACHINE_H
#define KMINT_STATE_MACHINE_H


#include <typeinfo>
#include "state.h"

namespace kmint
{
    template<class piece_type>
    class state_machine
    {
        piece_type* _owner;

        state<piece_type>* _current_state = nullptr;
        state<piece_type>* _previous_state = nullptr;
        state<piece_type>* _global_state = nullptr;

    public:
        explicit state_machine(piece_type* owner) : _owner{owner} {}

        void set_global_state(state<piece_type>* state)
        {
            _global_state = state;
        }

        state<piece_type>* get_global_state()
        {
            return _global_state;
        }

        void set_current_state(state<piece_type>* state)
        {
            _current_state = state;

            if (_current_state != nullptr)
            {
                _current_state->enter(_owner);
            }
        }

        state<piece_type>* get_current_state()
        {
            return _current_state;
        }

        void set_previous_state(state<piece_type>* state)
        {
            _previous_state = state;

            if (_previous_state != nullptr)
            {
                _previous_state->leave(_owner);
            }
        }

        state<piece_type>* get_previous_state()
        {
            return _previous_state;
        }

        void update() const
        {
            if (_global_state != nullptr) _global_state->update(_owner);

            if (_current_state != nullptr) _current_state->update(_owner);
        }

        void go_to_state(state<piece_type>* new_state)
        {
            if (is_in_state(new_state)) return;

            set_previous_state(_current_state);
            set_current_state(new_state);
        }

        void go_to_previous_state()
        {
            go_to_state(_previous_state);
        }

        bool is_in_state(const state<piece_type>* state) const
        {
            return typeid(*state) == typeid(*_current_state);
        }
    };
}


#endif //KMINT_STATE_MACHINE_H
