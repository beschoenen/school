#include "Entity.h"
#include "../../Utilities/Random.h"
#include "../Engine.h"

engine::Entity::Entity(std::shared_ptr<engine::Map> map, std::string image,
                       int x, int y, int width, int height,
                       int max_health_points, int health_points,
                       int max_armor_points, int armor_points,
                       int minimum_damage, int maximum_damage) :
    AnimatedImage(std::move(image), x, y, width, height),
    map(std::move(map)),
    max_health_points(max_health_points), health_points(health_points),
    max_armor_points(max_armor_points), armor_points(armor_points),
    minimum_damage(minimum_damage), maximum_damage(maximum_damage)
{
    anchor = POS_CENTER_CENTER;
    y_sorted = true;
    layer = ENTITY_LAYER;
}

int engine::Entity::get_health_points() const
{
    return health_points;
}

void engine::Entity::set_health_point(const int health)
{
    health_points = health;
}

int engine::Entity::get_health_points_start_round() const
{
    return health_points_start_round;
}

int engine::Entity::get_max_health_points() const
{
    return max_health_points;
}

void engine::Entity::set_max_health_points(int health)
{
    max_health_points = health;
}

int engine::Entity::get_armor_points() const
{
    return armor_points;
}

void engine::Entity::set_armor_points(int armor)
{
    armor_points = armor;
}

int engine::Entity::get_max_armor_points() const
{
    return max_armor_points;
}

void engine::Entity::set_max_armor_points(int armor)
{
    max_armor_points = armor;
}

int engine::Entity::get_minimum_damage() const
{
    return minimum_damage;
}

int engine::Entity::get_maximum_damage() const
{
    return maximum_damage;
}

void engine::Entity::set_map(std::shared_ptr<engine::Map> map)
{
    this->map = std::move(map);
}


std::shared_ptr<engine::Map> engine::Entity::get_map()
{
    return this->map;
}

int engine::Entity::attack()
{
    int a = Random::instance().get(minimum_damage, maximum_damage);

    if (attack_potion_effect)
    {
        a += ADD_ATTACK_POINTS;
    }

    if (stronger_sword_effect)
    {
        a += STRONGER_SWORD_POINTS;
    }

    return a;
}

void engine::Entity::set_health_points_start_round(int health_points)
{
    health_points_start_round = health_points;
}

void engine::Entity::take_damage(int damage)
{
    if (armor_points > 0)
    {
        armor_points -= damage;
    }
    else
    {
        health_points -= damage;
    }

    if (armor_points < 0)
    {
        health_points += armor_points;
        armor_points = 0;
    }

    if (health_points < 0)
    {
        health_points = 0;
    }
}

void engine::Entity::update()
{
    state_machine.update();
}

void engine::Entity::set_entity(std::shared_ptr<Entity> entity)
{
    std::shared_ptr<State> current_state = state_machine.get_current_state();
    std::shared_ptr<State> global_state = state_machine.get_global_state();

    if (current_state != nullptr)
    {
        current_state->set_entity(entity);
    }

    if (global_state != nullptr)
    {
        global_state->set_entity(entity);
    }
}

void engine::Entity::set_state(std::shared_ptr<State> state)
{
    state_machine.change_state(state);
}

void engine::Entity::set_global_state(std::shared_ptr<State> state)
{
    state_machine.set_global_state(state);
}

void engine::Entity::add_armor()
{
    armor_points = max_armor_points;
}

void engine::Entity::eat_food(int points)
{
    health_points = std::min(health_points + points, max_health_points);
}

void engine::Entity::health_potion()
{
    health_points = max_health_points;
}

void engine::Entity::armor_potion(int points)
{
    armor_points = std::min(armor_points + points, max_armor_points);
}

void engine::Entity::reset_armor()
{
    armor_points = 0;
}

void engine::Entity::attack_potion()
{
    attack_potion_effect = true;
}

void engine::Entity::stronger_sword()
{
    stronger_sword_effect = true;
}

void engine::Entity::reset_attack_potion()
{
    attack_potion_effect = false;
}

void engine::Entity::set_current_tiles()
{
    if (!current_tiles.empty()) return;

    auto size = Engine::instance()->get_size();
    auto rect = get_rect();

    int width = 1;
    int height = 1;

    auto total_x = map->get_total_width();
    auto total_y = map->get_total_height();

    auto center_x = size.w / 2;
    auto center_y = size.h / 2;

    auto mod_x = (map->x - (center_x - rect.x)) % TILE_SIZE;
    auto mod_y = (map->y - (center_y - rect.y)) % TILE_SIZE;

    if (mod_x != 0)
    {
        width = 2;
    }

    if (mod_y != 0)
    {
        height = 2;
    }

    int index_x = ((total_x / 2) + (map->x - (center_x - rect.x))) / TILE_SIZE;
    int index_y = ((total_y / 2) + (map->y - (center_y - rect.y))) / TILE_SIZE;

    auto grid = map->get_map();

    for (int i = 0; i < height; ++i)
    {
        for (int j = 0; j < width; ++j)
        {
            current_tiles.push_back(grid.at(index_y + i).at(index_x + j));
        }
    }
}

bool engine::Entity::can_move_up()
{
    if ((map->y - ((Engine::instance()->get_size().h / 2) - get_rect().y)) % TILE_SIZE != 0) return true;

    set_current_tiles();

    if (current_tiles.size() == 1 ||
        (current_tiles.size() == 2 && current_tiles[0]->get_row() != current_tiles[1]->get_row()))
    {
        if (current_tiles[0]->get_north().expired()) return false;

        return current_tiles[0]->get_north().lock()->get_accessible();
    }
    else if (current_tiles.size() == 4 ||
             (current_tiles.size() == 2 && current_tiles[0]->get_column() != current_tiles[1]->get_column()))
    {
        if (current_tiles[0]->get_north().expired() || current_tiles[1]->get_north().expired()) return false;

        return current_tiles[0]->get_north().lock()->get_accessible() &&
               current_tiles[1]->get_north().lock()->get_accessible();
    }

    return true;
}

bool engine::Entity::can_move_down()
{
    if ((map->y - ((Engine::instance()->get_size().h / 2) - get_rect().y)) % TILE_SIZE != 0) return true;

    set_current_tiles();

    if (current_tiles.size() == 1 ||
        (current_tiles.size() == 2 && current_tiles[current_tiles.size() - 1]->get_row() !=
                                      current_tiles[current_tiles.size() - 2]->get_row()))
    {
        if (current_tiles[current_tiles.size() - 1]->get_south().expired()) return false;

        return current_tiles[current_tiles.size() - 1]->get_south().lock()->get_accessible();
    }
    else if (current_tiles.size() == 4 ||
             (current_tiles.size() == 2 && current_tiles[current_tiles.size() - 1]->get_column() !=
                                           current_tiles[current_tiles.size() - 2]->get_column()))
    {
        if (current_tiles[current_tiles.size() - 2]->get_south().expired() ||
            current_tiles[current_tiles.size() - 1]->get_south().expired()) return false;

        return current_tiles[current_tiles.size() - 2]->get_south().lock()->get_accessible() &&
               current_tiles[current_tiles.size() - 1]->get_south().lock()->get_accessible();
    }

    return true;
}

bool engine::Entity::can_move_left()
{
    if ((map->x - ((Engine::instance()->get_size().w / 2) - get_rect().x)) % TILE_SIZE != 0) return true;

    set_current_tiles();

    if (current_tiles.size() == 1 ||
        (current_tiles.size() == 2 && current_tiles[0]->get_column() != current_tiles[1]->get_column()))
    {
        if (current_tiles[0]->get_west().expired()) return false;

        return current_tiles[0]->get_west().lock()->get_accessible();
    }
    else if (current_tiles.size() == 2 && current_tiles[0]->get_row() != current_tiles[1]->get_row())
    {
        if (current_tiles[0]->get_west().expired() || current_tiles[1]->get_west().expired()) return false;

        return current_tiles[0]->get_west().lock()->get_accessible() &&
               current_tiles[1]->get_west().lock()->get_accessible();
    }
    else if (current_tiles.size() == 4)
    {
        if (current_tiles[0]->get_west().expired() || current_tiles[2]->get_west().expired()) return false;

        return current_tiles[0]->get_west().lock()->get_accessible() &&
               current_tiles[2]->get_west().lock()->get_accessible();
    }

    return true;
}

bool engine::Entity::can_move_right()
{
    if ((map->x - ((Engine::instance()->get_size().w / 2) - get_rect().x)) % TILE_SIZE != 0) return true;

    set_current_tiles();

    if (current_tiles.size() == 1)
    {
        if (current_tiles[0]->get_east().expired()) return false;

        return current_tiles[0]->get_east().lock()->get_accessible();
    }
    else if (current_tiles.size() == 2 && current_tiles[0]->get_column() != current_tiles[1]->get_column())
    {
        if (current_tiles[1]->get_east().expired()) return false;

        return current_tiles[1]->get_east().lock()->get_accessible();
    }
    else if (current_tiles.size() == 2 && current_tiles[0]->get_row() != current_tiles[1]->get_row())
    {
        if (current_tiles[0]->get_east().expired() || current_tiles[1]->get_east().expired()) return false;

        return current_tiles[0]->get_east().lock()->get_accessible() &&
               current_tiles[1]->get_east().lock()->get_accessible();
    }
    else if (current_tiles.size() == 4)
    {
        if (current_tiles[1]->get_east().expired() || current_tiles[3]->get_east().expired()) return false;

        return current_tiles[1]->get_east().lock()->get_accessible() &&
               current_tiles[3]->get_east().lock()->get_accessible();
    }

    return true;
}

void engine::Entity::clear_cached_tiles()
{
    current_tiles.clear();
}

void engine::Entity::move_up()
{
    y -= MOVE_DISTANCE;
    phase_direction = "up";
}

void engine::Entity::move_down()
{
    y += MOVE_DISTANCE;
    phase_direction = "down";
}

void engine::Entity::move_left()
{
    x -= MOVE_DISTANCE;
    phase_direction = "left";
}

void engine::Entity::move_right()
{
    x += MOVE_DISTANCE;
    phase_direction = "right";
}

void engine::Entity::set_direction(std::string direction)
{
    phase_direction = std::move(direction);
    go_to_walking_phase(); // TODO ugly
}

bool engine::Entity::go_to_looking_phase()
{
    return go_to_phase("looking-" + phase_direction);
}

bool engine::Entity::go_to_walking_phase()
{
    return go_to_phase("walking-" + phase_direction);
}

bool engine::Entity::go_to_attacking_phase()
{
    return go_to_phase("attacking-" + phase_direction);
}

std::vector<std::shared_ptr<level::GridTile>> engine::Entity::get_current_tiles()
{
    set_current_tiles();

    return current_tiles;
}
