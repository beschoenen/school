#ifndef KMINT_SCHAAP_H
#define KMINT_SCHAAP_H


#include "base/graph_bound_board_piece.h"
#include "../states/base/state_machine.h"
#include "rabbit.h"
#include "../drawables/path_drawable.h"
#include "../drawables/base/combined_drawable.h"

namespace kmint
{
    class schaap : public graph_bound_board_piece
    {
        kmint::state_machine<schaap>* _state_machine;

        int _thirst = 0;
        int _no_drinks = 0;

        path_drawable _path_drawable;
        combined_drawable _combined_drawable;

    public:
        explicit schaap(graph* graph);

        ~schaap() override;

        void update(float delta_time) override;

        kmint::state_machine<schaap>* get_state_machine();

        const drawable& get_drawable() const override { return _combined_drawable; }

        void increment_thirst();

        void drink(int clench);

        bool is_thirsty();

        bool is_sleepy();

        void slept();

        void set_path(std::vector<node_id> path);
    };
}


#endif //KMINT_SCHAAP_H
