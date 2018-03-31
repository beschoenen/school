#ifndef KMINT_MOVING_ENTITY_H
#define KMINT_MOVING_ENTITY_H


#include "../models/point.h"
#include "../board pieces/base/free_roaming_board_piece.h"
#include "../population/base/population.h"

namespace kmint
{
    class rabbit;

    class moving_entity : public free_roaming_board_piece
    {
    protected:
        point _velocity = {0, 0};
        point _heading = {1, 1};
        point _side = {-1, 1};

        float _mass = 0;
        float _max_speed = 0;
        float _max_force = 0;

        float _attraction_to_schaap;
        float _attraction_to_water;
        float _cohesion;
        float _separation;
        float _alignment;

        float _bounding_radius = 3.f;
        float _radius = 50.f;

        float _time_alive = 0;
        entity_state _state = alive;

        bool _tagged = false;

    public:
        moving_entity(point location, int size, const color& color, float attraction_to_schaap,
                      float attraction_to_water, float cohesion, float separation, float alignment);

        point calculate_water_force();

        void tag_neighbors(std::vector<rabbit*>& entities, double radius);

        void enforce_non_penetration_constraint(std::vector<rabbit*>& entities);

        void velocity(point v);

        point velocity();

        void heading(point v);

        point heading();

        void side(point v);

        point side();

        void mass(float mass);

        float mass();

        void max_speed(float max_speed);

        float max_speed();

        void max_force(float max_force);

        float max_force();
        
        float attraction_to_schaap();
        
        void attraction_to_schaap(float attraction);
        
        float attraction_to_water();
        
        void attraction_to_water(float attraction);

        float cohesion();

        void cohesion(float cohesion);

        float separation();

        void separation(float separation);

        float alignment();

        void alignment(float alignment);

        float time_alive();

        entity_state state();

        void state(entity_state state);

        float bounding_radius();

        void bounding_radius(float bounding_radius);

        float radius();

        void radius(float radius);

        bool is_tagged();

        void tag();

        void untag();

        bool operator==(moving_entity* right);
    };
}


#endif //KMINT_MOVING_ENTITY_H
