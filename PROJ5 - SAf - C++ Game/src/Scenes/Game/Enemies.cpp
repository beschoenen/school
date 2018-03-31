#include "../Game.h"
#include "../../Engine/Engine.h"
#include "../../Utilities/Random.h"
#include "../../States/Mob/MobWandering.h"

std::vector<std::shared_ptr<Mob>> scenes::Game::get_enemies() const
{
    return enemies;
}

void scenes::Game::set_enemies()
{
    auto total_width = map->get_total_width();
    auto total_height = map->get_total_height();

    for (const auto& enemy : enemies)
    {
        if (!is_resuming)
        {
            enemy->set_x(-(total_width / 2) + (TILE_SIZE / 2) + (enemy->get_x() * TILE_SIZE) - map->x);
            enemy->set_y(-(total_height / 2) + (TILE_SIZE / 2) + (enemy->get_y() * TILE_SIZE) - map->y);
        }

        Engine::instance()->draw(enemy);
    }
}

void scenes::Game::remove_enemies()
{
    for (const auto& enemy : enemies)
    {
        Engine::instance()->erase(enemy);
    }
}

void scenes::Game::clear_enemies()
{
    enemies.clear();
}

void scenes::Game::spawn_enemies(int amount)
{
    std::vector<std::shared_ptr<level::GridTile>> tiles;
    std::shared_ptr<level::Dungeon> dungeon = map->get_dungeon();

    if (dungeon == nullptr) return;

    LevelMatrix map = dungeon->get_map();

    auto height = static_cast<int>(map.size());
    auto width = static_cast<int>(map.at(0).size());

    int row_index = 0;
    int column_index = 0;

    while (enemies.size() < amount)
    {
        row_index = Random::instance().get(0, height - 1);
        column_index = Random::instance().get(0, width - 1);

        std::shared_ptr<level::GridTile> tile = map.at(row_index).at(column_index);

        auto difference_row = abs(((height / 2) - row_index));
        auto difference_column = abs(((width / 2) - column_index));

        if (tile->get_grid_tile_type() == ground && difference_row > 1 && difference_column > 1 &&
            std::find(tiles.begin(), tiles.end(), tile) == tiles.end())
        {
            tiles.push_back(tile);

            int x_position = tile->get_column();
            int y_position = tile->get_row();

            std::shared_ptr<Mob> mob = std::make_shared<Mob>(this->map, x_position, y_position, 100, 3, 5, 10);
            mob->set_state(std::shared_ptr<MobWandering>(new MobWandering()));
            mob->set_entity(mob);
            mob->set_player(player);
            enemies.push_back(mob);
        }
    }
}

void scenes::Game::attack_mob(std::shared_ptr<Mob> mob, const int damage)
{
    sound_mob_hit.play_once();
    mob->take_damage(damage);
    attack_text_ticker = 0;

    attack_text->set_mob(mob);
    attack_text->set_text(std::to_string(mob->get_health_points()));

    if (mob->get_health_points() <= 0)
    {
        enemies.erase(std::remove(enemies.begin(), enemies.end(), mob), enemies.end());
        sound_mob_dead.play_once();

        attack_text->clear_mob();
        Engine::instance()->erase(mob);

        score += ATTACK_DAMAGE_DEAD;
        hud->set_score(score);
    }
}

int scenes::Game::calculate_amount_of_enemies()
{
    int layers = 1 + level;
    int tiles = 0;

    for (int i = 1; i <= layers; i++)
    {
        tiles += (i * 8) + 4;
    }

    return static_cast<int>((0.5 * tiles) + (((round - 2) * 0.1) * tiles));
}
