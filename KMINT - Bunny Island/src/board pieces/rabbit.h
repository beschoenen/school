#ifndef KMINT_RABBIT_H
#define KMINT_RABBIT_H

#include "base/free_roaming_board_piece.h"
#include "../force driven/steering_behaviors.h"
#include "../population/genetics.h"

#define RABBIT_COLOR {0xff, 0xff, 0xff}
#define MAX_SPEED 100.f
#define MASS 5.f
#define MAX_FORCE 400.f

namespace kmint
{
    class genetics;

    class rabbit : public moving_entity
    {
        steering_behaviors* _steering_behaviors;
        genetics * _genetics;

    public:
        explicit rabbit(point location);

        explicit rabbit(point location, float attraction_to_schaap, float attraction_to_water,
                        float cohesion, float separation, float alignment);

        explicit rabbit(const rabbit& rab);

        ~rabbit();

        void update(float delta_time) override;

        genetics& get_genetics();

        void set_chromesomes(std::vector<float> new_chromesomes);

        bool operator==(rabbit& right);

        static rabbit* null_object();
    };
}

#endif //KMINT_RABBIT_H
