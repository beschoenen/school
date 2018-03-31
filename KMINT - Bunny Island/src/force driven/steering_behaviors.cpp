#include <iostream>
#include "steering_behaviors.h"
#include "../random.h"
#include "../models/matrix2d.h"
#include "../entities.h"

namespace kmint
{
    steering_behaviors::steering_behaviors(moving_entity* owner) : _owner{owner}
    {
        float theta = random::instance().getf(-1, 1) * static_cast<float>(M_PI * 2);

        _wander_target = {_wander_radius * cos(theta), _wander_radius * sin(theta)};
    }

    bool steering_behaviors::on(behavior_type bt)
    {
        return (flags & bt) == bt;
    }

    void steering_behaviors::set_on(behavior_type bt)
    {
        flags |= bt;
    }

    void steering_behaviors::set_off(behavior_type bt)
    {
        flags ^= bt;
    }

    point steering_behaviors::seek(kmint::point& point)
    {
        auto velocity = (point - _owner->location()).normalized() * _owner->max_speed();

        return velocity - _owner->velocity();
    }

    point steering_behaviors::flee(kmint::point& point, float panic_distance)
    {
        if (_owner->location().distance_squared(point) > panic_distance)
        {
            return kmint::point(0, 0);
        }

        auto velocity = (_owner->location() - point).normalized() * _owner->max_speed();

        return velocity - _owner->velocity();
    }

    point steering_behaviors::arrive(kmint::point& point, kmint::deceleration deceleration)
    {
        auto target = point - _owner->location();

        auto distance = target.length();

        if (distance <= 0) return kmint::point(0, 0);

        auto deceleration_tweaker = 0.3;

        float speed = distance / (deceleration * deceleration_tweaker);

        speed = std::min(speed, _owner->max_speed());

        auto d_v = target * speed / distance;

        return d_v - _owner->velocity();
    }

    point steering_behaviors::wander()
    {
        float jitter_time_slice = _wander_jitter * _owner->time_alive();

        _wander_target += point(
            random::instance().getf(-1, 1) * jitter_time_slice,
            random::instance().getf(-1, 1) * jitter_time_slice
        );

        _wander_target.normalize();

        _wander_target *= _wander_radius;

        auto target_local = _wander_target + point(_wander_distance, 0);

        auto target_world = point_to_world_space(target_local, _owner->heading(), _owner->side(), _owner->location());

        return target_world - _owner->location();
    }

    point steering_behaviors::separation(std::vector<rabbit*>& neighbors)
    {
        point force;

        for (rabbit* neighbor : neighbors)
        {
            if (neighbor == _owner) continue;

            if (neighbor->is_tagged())
            {
                point to = _owner->location() - neighbor->location();

                force += to.normalized() / to.length();
            }
        }

        return force;
    }

    point steering_behaviors::alignment(std::vector<rabbit*>& neighbors)
    {
        point average_heading;

        int neighbor_count = 0;

        for (rabbit* neighbor : neighbors)
        {
            if (neighbor == _owner) continue;

            if (neighbor->is_tagged())
            {
                average_heading += neighbor->heading();

                ++neighbor_count;
            }
        }

        if (neighbor_count > 0)
        {
            average_heading /= neighbor_count;
            average_heading -= _owner->heading();
        }

        return average_heading;
    }

    point steering_behaviors::cohesion(std::vector<rabbit*>& neighbors)
    {
        point center_of_mass, force;

        int neighbor_count = 0;

        for (rabbit* neighbor : neighbors)
        {
            if (neighbor == _owner) continue;

            if (neighbor->is_tagged())
            {
                center_of_mass += neighbor->location();

                ++neighbor_count;
            }
        }

        if (neighbor_count > 0)
        {
            center_of_mass /= neighbor_count;
            force = seek(center_of_mass) / 8;
        }

        return force;
    }

    point steering_behaviors::calculate()
    {
        auto neighbors = entities::instance()->rabbit_population->get_individuals();

        _owner->tag_neighbors(neighbors, 30);

        point force, steering_force;

        force += separation(neighbors) * _owner->separation();
        if (!accumulate_force(steering_force, force)) return steering_force;

        force += cohesion(neighbors) * _owner->cohesion();
        if (!accumulate_force(steering_force, force)) return steering_force;

        force += alignment(neighbors) * _owner->alignment();
        if (!accumulate_force(steering_force, force)) return steering_force;

        force += wander() * 2.f;
        if (!accumulate_force(steering_force, force)) return steering_force;

        auto schaap_location = entities::instance()->schaap->location();
        if (_owner->attraction_to_schaap() >= 0)
        {
            steering_force += seek(schaap_location) * _owner->attraction_to_schaap();
        }
        else
        {
            steering_force += flee(schaap_location, 30.f * 30.f) * -_owner->attraction_to_schaap();
        }

        steering_force += _owner->calculate_water_force() * -_owner->attraction_to_water();

        return steering_force.truncated(_owner->max_force());
    }

    kmint::point steering_behaviors::point_to_world_space(kmint::point point, kmint::point heading,
                                                          kmint::point side, kmint::point location)
    {
        matrix2d mat_transform;
        kmint::point trans_point = point;

        mat_transform.rotate(heading, side);
        mat_transform.translate(location.x(), location.y());
        mat_transform.transform(trans_point);

        return trans_point;
    }

    bool steering_behaviors::accumulate_force(kmint::point &sf, kmint::point force_to_add)
    {
        //first calculate how much steering force we have left to use
        double magnitude_so_far = sf.length();

        double magnitude_remaining = _owner->max_force() - magnitude_so_far;

        //return false if there is no more force left to use
        if (magnitude_remaining <= 0.0) return false;

        //calculate the magnitude of the force we want to add
        double magnitude_to_add = force_to_add.length();

        //now calculate how much of the force we can really add
        if (magnitude_to_add > magnitude_remaining)
        {
            magnitude_to_add = magnitude_remaining;
        }

        //add it to the steering force
        sf += (force_to_add.normalized() * magnitude_to_add);

        return true;
    }
}
