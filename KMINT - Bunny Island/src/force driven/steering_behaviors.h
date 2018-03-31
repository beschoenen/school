#ifndef KMINT_STEERING_BEHAVIORS_H
#define KMINT_STEERING_BEHAVIORS_H


#include "../models/point.h"
#include "moving_entity.h"

namespace kmint
{
    class rabbit;

    enum deceleration
    {
        slow = 3,
        normal = 2,
        fast = 1
    };

    enum behavior_type
    {
        none = 0x0000,
        seek = 0x0001,
        arrive = 0x0002,
        separation = 0x0004,
        cohesion = 0x0008,
        alignment = 0x0010,
        wander = 0x0040,
    };

    class steering_behaviors
    {
        moving_entity* _owner;

        int flags = none;

        point _wander_target;
        float _wander_jitter = 360.f;
        float _wander_radius = 100.f;
        float _wander_distance = 0.001f;

    public:
        explicit steering_behaviors(moving_entity* owner);

        bool on(behavior_type bt);

        void set_on(behavior_type bt);

        void set_off(behavior_type bt);

        point seek(point& point);

        point flee(point& point, float panic_distance);

        point arrive(point& point, deceleration deceleration);

        point wander();

        point separation(std::vector<rabbit*>& neighbors);

        point alignment(std::vector<rabbit*>& neighbors);

        point cohesion(std::vector<rabbit*>& neighbors);

        point calculate();

        kmint::point point_to_world_space(kmint::point point, kmint::point heading,
                                          kmint::point side, kmint::point location);

        bool accumulate_force(point& sf, point force_to_add);
    };
}


#endif //KMINT_STEERING_BEHAVIORS_H
