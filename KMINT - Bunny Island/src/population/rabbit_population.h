#ifndef KMINT_RABBIT_POPULATION_H
#define KMINT_RABBIT_POPULATION_H


#include "../board pieces/rabbit.h"
#include "base/population.h"
#include "../board pieces/base/static_board_piece.h"
#include "../drawables/rabbit_drawable.h"

namespace kmint
{
    class rabbit_population : public population<rabbit>, public static_board_piece
    {
        rabbit_drawable _drawable;
        int generations = 0;
        int amount_mutated = 0;
        rabbit* father = rabbit::null_object();
        rabbit* mother = rabbit::null_object();

    public:
        rabbit_population();

        const drawable& get_drawable() const override { return _drawable; }

        void update(float delta_time) override;

        void remove_rabbits();

        void select_parents();

        void generate_population();

        void reproduce_offspring();

        rabbit* generate_rabbit();

        point get_new_location();

        int get_generations() const;
    };
}


#endif //KMINT_RABBIT_POPULATION_H
