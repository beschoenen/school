#include "../Game.h"
#include "../../Engine/Engine.h"
#include "../../Utilities/Random.h"

void scenes::Game::set_items()
{
    for (const auto& item : items)
    {
        Engine::instance()->draw(item);
    }
}

void scenes::Game::remove_items()
{
    for (const auto& item : items)
    {
        Engine::instance()->erase(item);
    }
}

void scenes::Game::remove_item(std::shared_ptr<Item> item)
{
    items.erase(std::remove(items.begin(), items.end(), item), items.end());
    Engine::instance()->erase(item);
}

void scenes::Game::spawn_items()
{
    // Reset values
    item_health_potions = 0;
    item_armor_potions = 0;
    item_attack_potions = 0;
    item_food = 0;
    item_armor = 0;

    // Get map details
    const std::vector<std::shared_ptr<level::GridTile>> tiles;
    const std::shared_ptr<level::Dungeon> dungeon = map->get_dungeon();

    if (dungeon == nullptr) return;

    LevelMatrix map = dungeon->get_map();

    const auto height = static_cast<int>(map.size());
    const auto width = static_cast<int>(map.at(0).size());
    const int amount = level;

    // Spawn potions
    spawn_health_potions(tiles, map, height, width, amount);
    spawn_armor_potions(tiles, map, height, width, amount);
    spawn_attack_potions(tiles, map, height, width, amount);

    // Spawn food
    spawn_food(tiles, map, height, width, amount);

    // Spawn armor
    spawn_armor(tiles, map, height, width, amount);
}

void scenes::Game::spawn_health_potions(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                        LevelMatrix map, int height, int width, int amount)
{
    while (item_health_potions < amount)
    {
        int* x_position;
        int* y_position;
        std::tie(x_position, y_position) = spawn_item_location(tiles, map, height, width);

        if (x_position != nullptr && y_position != nullptr)
        {
            items.push_back(std::make_shared<Item>(POTION_RED, *x_position, *y_position, 50, 50));
            item_health_potions++;
        }

        delete x_position;
        delete y_position;
    }
}

void scenes::Game::spawn_armor_potions(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                       LevelMatrix map, int height, int width, int amount)
{
    while (item_armor_potions < amount)
    {
        int* x_position;
        int* y_position;
        std::tie(x_position, y_position) = spawn_item_location(tiles, map, height, width);

        if (x_position != nullptr && y_position != nullptr)
        {
            items.push_back(std::make_shared<Item>(POTION_BLUE, *x_position, *y_position, 50, 50));
            item_armor_potions++;
        }

        delete x_position;
        delete y_position;
    }
}

void scenes::Game::spawn_attack_potions(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                        LevelMatrix map, int height, int width, int amount)
{
    while (item_attack_potions < amount)
    {
        int* x_position;
        int* y_position;
        std::tie(x_position, y_position) = spawn_item_location(tiles, map, height, width);

        if (x_position != nullptr && y_position != nullptr)
        {
            items.push_back(std::make_shared<Item>(POTION_GRAY, *x_position, *y_position, 50, 50));
            item_attack_potions++;
        }

        delete x_position;
        delete y_position;
    }
}

void scenes::Game::spawn_food(std::vector<std::shared_ptr<level::GridTile>> tiles,
                              LevelMatrix map, int height, int width, int amount)
{
    while (item_food < amount)
    {
        int* x_position;
        int* y_position;
        std::tie(x_position, y_position) = spawn_item_location(tiles, map, height, width);

        if (x_position != nullptr && y_position != nullptr)
        {
            items.push_back(std::make_shared<Item>(FOOD, *x_position, *y_position, 50, 50));
            item_food++;
        }

        delete x_position;
        delete y_position;
    }
}

void scenes::Game::spawn_armor(std::vector<std::shared_ptr<level::GridTile>> tiles,
                               LevelMatrix map, int height, int width, int amount)
{
    while (item_armor < amount)
    {
        int* x_position;
        int* y_position;
        std::tie(x_position, y_position) = spawn_item_location(tiles, map, height, width);

        if (x_position != nullptr && y_position != nullptr)
        {
            items.push_back(std::make_shared<Item>(ARMOR, *x_position, *y_position, 50, 50));
            item_armor++;
        }

        delete x_position;
        delete y_position;
    }
}

std::tuple<int*, int*> scenes::Game::spawn_item_location(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                                         LevelMatrix map, const int height, const int width)
{
    const int row_index = Random::instance().get(0, height - 1);
    const int column_index = Random::instance().get(0, width - 1);

    const std::shared_ptr<level::GridTile> tile = map.at(row_index).at(column_index);

    const auto difference_row = abs((height / 2) - row_index);
    const auto difference_column = abs((width / 2) - column_index);

    if (tile->get_grid_tile_type() == ground && difference_row > 1 && difference_column > 1 &&
        std::find(tiles.begin(), tiles.end(), tile) == tiles.end())
    {
        tiles.push_back(tile);

        auto* x_position = new int(tile->get_column());
        auto* y_position = new int(tile->get_row());

        return std::make_tuple(x_position, y_position);
    }

    return std::tuple<int*, int*>(nullptr, nullptr);
}

std::shared_ptr<Item> scenes::Game::can_pickup_item()
{
    std::vector<std::shared_ptr<level::GridTile>> player_current_tiles{player->get_current_tiles()};

    for (auto& item : items)
    {
        if (!item->will_be_visible()) continue;

        const auto rect = item->get_rect();
        const auto size = Engine::instance()->get_size();

        const auto total_x = map->get_total_width();
        const auto total_y = map->get_total_height();

        const auto center_x = size.w / 2;
        const auto center_y = size.h / 2;

        const int index_x = ((total_x / 2) + (map->x - (center_x - rect.x))) / TILE_SIZE;
        const int index_y = ((total_y / 2) + (map->y - (center_y - rect.y))) / TILE_SIZE;

        auto grid = map->get_map();

        const auto i_tile = grid.at(index_y).at(index_x);

        for (auto& p_tile : player_current_tiles)
        {
            if (p_tile == i_tile)
            {
                return item;
            }
        }
    }

    return nullptr;
}

void scenes::Game::pickup_item(std::shared_ptr<Item> item)
{
    if (item->get_image() == FOOD)
    {
        sound_eating.play_once();
        player->eat_food(ADD_FOOD_HEALTH);
    }
    else if (item->get_image() == ARMOR)
    {
        sound_cloth.play_once();
        player->add_armor();
    }
    else if (item->get_image() == POTION_GRAY)
    {
        sound_bottle.play_once();
        player->attack_potion();
    }
    else if (item->get_image() == POTION_RED)
    {
        sound_bottle.play_once();
        player->health_potion();
    }
    else if (item->get_image() == POTION_BLUE)
    {
        sound_bottle.play_once();
        player->armor_potion();
    }

    remove_item(item);
}
