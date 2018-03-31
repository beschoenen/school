#ifndef KMINT_GENETICS_H
#define KMINT_GENETICS_H

#include "../board pieces/rabbit.h"

namespace kmint
{
    class rabbit;

    enum gender
    {
        male,
        female
    };

    class genetics
    {
        rabbit* _base_rabbit;

    public:
        int _generations_alive = 0;
        gender _gender;

        explicit genetics(rabbit* rabbit);

        void generate();

        bool generate(rabbit& father, rabbit& mother);

        void crossover(rabbit& father, rabbit& mother);

        bool mutate(rabbit& father, rabbit& mother);

        gender get_gender();

        int get_generations_alive();

        void increase_generation_alive();

        float get_time_alive();
    };
}

#endif //KMINT_GENETICS_H
