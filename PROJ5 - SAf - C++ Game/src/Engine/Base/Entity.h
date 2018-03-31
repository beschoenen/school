#ifndef GAME_ENTITY_H
#define GAME_ENTITY_H
#include "StateMachine.h"

namespace engine
{
    class State;
}

#include "../Components/AnimatedImage.h"
#include "State.h"
#include "../../Level/Tiles/GridTile.h"
#include "Map.h"
#include "../../defines_item.h"
#include "../../defines_mission.h"

namespace engine
{
    class Entity : public AnimatedImage
    {
        std::string phase_direction = "down";

        int max_health_points;
        int max_armor_points;

        int health_points_start_round;
        int health_points;
        int armor_points;

        int minimum_damage;
        int maximum_damage;

        bool attack_potion_effect = false;
        bool stronger_sword_effect = false;

        std::shared_ptr<engine::Map> map;

        std::vector<std::shared_ptr<level::GridTile>> current_tiles;

        void set_current_tiles();

    protected:
        StateMachine state_machine;

    public:
        Entity(std::shared_ptr<engine::Map> map, std::string image, int x, int y, int width, int height,
               int max_health_points, int health_points,
               int max_armor_points, int armor_points,
               int minimum_damage, int maximum_damage);

        // Getters

        int get_health_points() const;

        void set_health_point(int health);

        int get_health_points_start_round() const;

        int get_max_health_points() const;

        void set_max_health_points(int health);

        int get_armor_points() const;

        void set_armor_points(int armor);

        int get_max_armor_points() const;

        void set_max_armor_points(int armor);

        int get_minimum_damage() const;

        int get_maximum_damage() const;

        void set_map(std::shared_ptr<engine::Map> map);

        std::shared_ptr<engine::Map> get_map();

        int attack();

        void set_health_points_start_round(int health_points);

        void take_damage(int damage);

        void add_armor();

        void eat_food(int points = ADD_FOOD_HEALTH);

        void health_potion();

        void armor_potion(int points = ADD_ARMOR_POINTS);

        void reset_armor();

        void attack_potion();

        void stronger_sword();

        void reset_attack_potion();

        virtual void update();

        // Moving

        bool can_move_up();

        bool can_move_down();

        bool can_move_left();

        bool can_move_right();

        void move_up();

        void move_down();

        void move_left();

        void move_right();

        // Phases

        void set_direction(std::string direction);

        bool go_to_looking_phase();

        bool go_to_walking_phase();

        bool go_to_attacking_phase();

        void clear_cached_tiles();

        // Current tiles

        std::vector<std::shared_ptr<level::GridTile>> get_current_tiles();

        // States

        void set_entity(std::shared_ptr<Entity> entity);

        void set_state(std::shared_ptr<State> state);

        void set_global_state(std::shared_ptr<State> state);
    };
}

#endif //GAME_ENTITY_H
