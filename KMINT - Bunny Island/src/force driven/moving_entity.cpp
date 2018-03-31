#include <iostream>
#include "moving_entity.h"
#include "../entities.h"

namespace kmint
{
    moving_entity::moving_entity(const point location, int size, const color& color,
                                 float attraction_to_schaap, float attraction_to_water, float cohesion,
                                 float separation, float alignment) :
        free_roaming_board_piece(location, size, color),
        _attraction_to_schaap{attraction_to_schaap}, _attraction_to_water{attraction_to_water},
        _cohesion{cohesion}, _separation{separation}, _alignment{alignment}
    {
    }

    point moving_entity::calculate_water_force()
    {
        std::vector<point> directions =
            {
                {0,   -20}, // Top
                {20,  -20}, // Top right
                {20,  0},   // Right
                {20,  20},  // Bottom right
                {0,   20},  // Bottom
                {-20, 20},  // Bottom left
                {-20, 0},   // Left
                {-20, -20}  // Top left
            };

        auto x = static_cast<int>(location().x());
        auto y = static_cast<int>(location().y());

        point pos = {10.f + (x - x % 20), 10.f + (y - y % 20)};

        point force = {0, 0};

        auto graph = entities::instance()->graph;

        for (auto& direction : directions)
        {
            auto new_location = pos + direction;

            auto node_id = graph->get_node(new_location);

            if (node_id >= 0) continue;

            auto d_v = (location() - new_location).normalized() * max_speed();

            force += d_v - velocity();
        }

        return force;
    }

    void moving_entity::tag_neighbors(std::vector<rabbit*>& entities, double radius)
    {
        // iterate through all entities checking for range
        for (moving_entity* entity : entities)
        {
            entity->untag();

            if (entity == this || entity->state() != alive || !_location.in_fov(_heading, entity->location(), 270)) continue;

            // first clear any current tag (*curEntity)->UnTag();
            point to = entity->location() - location();

            // the bounding radius of the other is taken into account by adding it to the range
            double range = radius + entity->radius();

            // if entity within range, tag for further consideration. (working in distance-squared space to avoid sqrts)
            if (to.dot() < range * range)
            {
                entity->tag();
            }
        }
    }

    void moving_entity::enforce_non_penetration_constraint(std::vector<rabbit*>& entities)
    {
        // iterate through all entities checking for any overlap of bounding radii
        for (rabbit* entity : entities)
        {
            // make sure we don't check against the individual
            if (entity == this || entity->state() != alive) continue;

            // calculate the distance between the positions of the entities
            point to = _location - entity->location();

            float distance = to.length();

            // if this distance is smaller than the sum of their radii then this
            // entity must be moved away in the direction parallel to the
            // ToEntity vector
            float amount_of_overlap = bounding_radius() + entity->bounding_radius() - distance;

            if (amount_of_overlap >= 0)
            {
                // move the entity a distance away equivalent to the amount of overlap.
                _location += (to / distance) * amount_of_overlap;
            }
        }
    }

    void moving_entity::velocity(point v)
    {
        _velocity = v;
    }

    point moving_entity::velocity()
    {
        return _velocity;
    }

    void moving_entity::heading(point v)
    {
        _heading = v;
    }

    point moving_entity::heading()
    {
        return _heading;
    }

    void moving_entity::side(point v)
    {
        _side = v;
    }

    point moving_entity::side()
    {
        return _side;
    }

    void moving_entity::mass(float mass)
    {
        _mass = mass;
    }

    float moving_entity::mass()
    {
        return _mass;
    }

    void moving_entity::max_speed(float max_speed)
    {
        _max_speed = max_speed;
    }

    float moving_entity::max_speed()
    {
        return _max_speed;
    }

    void moving_entity::max_force(float max_force)
    {
        _max_force = max_force;
    }

    float moving_entity::max_force()
    {
        return _max_force;
    }

    float moving_entity::attraction_to_schaap()
    {
        return _attraction_to_schaap;
    }

    void moving_entity::attraction_to_schaap(float attraction)
    {
        _attraction_to_schaap = attraction;
    }

    float moving_entity::attraction_to_water()
    {
        return _attraction_to_water;
    }

    void moving_entity::attraction_to_water(float attraction)
    {
        _attraction_to_water = attraction;
    }

    float moving_entity::cohesion()
    {
        return _cohesion;
    }

    void moving_entity::cohesion(float cohesion)
    {
        _cohesion = cohesion;
    }

    float moving_entity::separation()
    {
        return _separation;
    }

    void moving_entity::separation(float separation)
    {
        _separation = separation;
    }

    float moving_entity::alignment()
    {
        return _alignment;
    }

    void moving_entity::alignment(float alignment)
    {
        _alignment = alignment;
    }

    entity_state moving_entity::state()
    {
        return _state;
    }

    void moving_entity::state(entity_state state)
    {
        _state = state;
    }

    float moving_entity::time_alive()
    {
        return _time_alive;
    }

    float moving_entity::bounding_radius()
    {
        return _bounding_radius;
    }

    void moving_entity::bounding_radius(float bounding_radius)
    {
        _bounding_radius = bounding_radius;
    }

    float moving_entity::radius()
    {
        return _radius;
    }

    void moving_entity::radius(float radius)
    {
        _radius = radius;
    }

    bool moving_entity::is_tagged()
    {
        return _tagged;
    }

    void moving_entity::tag()
    {
        _tagged = true;
    }

    void moving_entity::untag()
    {
        _tagged = false;
    }

    bool moving_entity::operator==(moving_entity* right)
    {
        return location() == right->location() &&
               _attraction_to_schaap == right->attraction_to_schaap() &&
               _attraction_to_water == right->attraction_to_water() &&
               _cohesion == right->cohesion() &&
               _separation == right->separation() &&
               _alignment == right->alignment();
    }
}
