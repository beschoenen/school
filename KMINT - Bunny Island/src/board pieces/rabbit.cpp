#include <iostream>
#include "rabbit.h"
#include "../entities.h"

namespace kmint
{
    rabbit::rabbit(point location) : rabbit(location, 0, 0, 0, 0, 0)
    {
    }

    rabbit::rabbit(point location, float attraction_to_schaap, float attraction_to_water,
                   float cohesion, float separation, float alignment) :
        moving_entity{location, 5, RABBIT_COLOR, attraction_to_schaap,
                      attraction_to_water, cohesion, separation, alignment}
    {
        _steering_behaviors = new kmint::steering_behaviors(this);
        _genetics = new genetics(this);

        _max_speed = MAX_SPEED;
        _mass = MASS;
        _max_force = MAX_FORCE;
    }

    rabbit::rabbit(const rabbit& rab) : moving_entity{point{rab.location()}, 5, RABBIT_COLOR,
                                                      rab._attraction_to_schaap, rab._attraction_to_water,
                                                      rab._cohesion, rab._separation, rab._alignment}
    {
        _steering_behaviors = new steering_behaviors{*rab._steering_behaviors};
        _genetics = new genetics{*rab._genetics};
        _time_alive = rab._time_alive;

        _max_speed = MAX_SPEED;
        _mass = MASS;
        _max_force = MAX_FORCE;
    }

    rabbit::~rabbit()
    {
        delete _steering_behaviors;
        delete _genetics;
    }

    void rabbit::update(float delta_time)
    {
        if (entities::instance()->graph->get_node(location().to_node()) < 0)
        {
            _state = drowned;
            std::cout << "Drowned - " << location().x() << ", " << location().y() << std::endl;
            return;
        }

        _time_alive += delta_time;

        point steering_force = _steering_behaviors->calculate();

        point acceleration = steering_force / _mass;

        _velocity += acceleration * delta_time;

        _velocity.truncate(_max_speed);

        _location += _velocity * delta_time;

        if (_velocity.length() > 0.0000000001f)
        {
            _heading = _velocity.normalized();
            _side = _heading.perpendicular();
        }

        auto rabbits = entities::instance()->rabbit_population->get_individuals();

        enforce_non_penetration_constraint(rabbits);
    }

    genetics& rabbit::get_genetics()
    {
        return *_genetics;
    }

    void rabbit::set_chromesomes(std::vector<float> new_chromesomes)
    {
        attraction_to_schaap(new_chromesomes[0]);
        attraction_to_water(new_chromesomes[1]);
        cohesion(new_chromesomes[2]);
        separation(new_chromesomes[3]);
        alignment(new_chromesomes[4]);
    }

    bool rabbit::operator==(rabbit& right)
    {
        return this->location() == right.location()
               && this->attraction_to_schaap() == right.attraction_to_schaap()
               && this->attraction_to_water() == right.attraction_to_water()
               && this->cohesion() == right.cohesion()
               && this->separation() == right.separation()
               && this->alignment() == right.alignment()
               && this->get_genetics().get_generations_alive() == right.get_genetics().get_generations_alive()
               && this->get_genetics().get_time_alive() == right.time_alive();
    }

    rabbit* rabbit::null_object()
    {
        auto min_float = std::numeric_limits<float>::lowest();

        auto rabbit = new kmint::rabbit{
            {min_float, min_float},
            min_float,
            min_float,
            min_float,
            min_float,
            min_float
        };

        rabbit->_genetics->_generations_alive = -1;
        rabbit->_time_alive = -1;

        return rabbit;
    }
}
