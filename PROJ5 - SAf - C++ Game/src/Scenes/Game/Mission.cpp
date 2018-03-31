#include <utility>
#include <ctime>
#include "../Game.h"
#include "../../Engine/Engine.h"
#include "../../Utilities/Random.h"

void
scenes::Game::spawn_mission(std::vector<std::shared_ptr<level::GridTile>> tiles,
                            LevelMatrix map, int height, int width, std::string image)
{
    bool success = false;
    while (!success)
    {
        int* x_position;
        int* y_position;
        std::tie(x_position, y_position) = spawn_item_location(tiles, map, height, width);

        if (x_position != nullptr && y_position != nullptr)
        {
            mission = std::shared_ptr<Mission>(new Mission(image, *x_position, *y_position, 50, 50));
            success = true;
        }

        delete x_position;
        delete y_position;
    }
}

bool scenes::Game::round_has_mission()
{
    std::default_random_engine generator;
    generator.seed(time(0));
    std::uniform_int_distribution<int> generate_number(1, 100);

    int number = generate_number(generator);

    return number <= 25;
}

int scenes::Game::choose_mission()
{
    std::default_random_engine generator;
    generator.seed(time(0));
    std::uniform_int_distribution<int> generate_number(1, 6);

    return generate_number(generator);
}

void scenes::Game::create_mission(std::string image, std::string text)
{
    // Get map details
    std::vector<std::shared_ptr<level::GridTile>> tiles;
    std::shared_ptr<level::Dungeon> dungeon = map->get_dungeon();

    if (dungeon == nullptr) return;

    LevelMatrix map = dungeon->get_map();

    auto height = static_cast<int>(map.size());
    auto width = static_cast<int>(map.at(0).size());

    // Place mission on map
    spawn_mission(tiles, map, height, width, std::move(image));
    Engine::instance()->draw(mission);

    // Show player
    set_mission_text(text);
}

void scenes::Game::finish_mission()
{
    sound_achievement = Engine::instance()->load_sound(SOUND_ACHIEVEMENT);

    if (mission->get_image() == MISSION_ONE)
    {
        score += MISSION_ONE_REWARD;
        set_mission_text(MISSION_ONE_REWARD_TEXT);
    }
    else if (mission->get_image() == MISSION_TWO)
    {
        score += MISSION_TWO_REWARD;
        set_mission_text(MISSION_TWO_REWARD_TEXT);
        player->stronger_sword();
    }
    else if (mission->get_image() == MISSION_THREE)
    {
        score += MISSION_THREE_REWARD;
        set_mission_text(MISSION_THREE_REWARD_TEXT);
    }
    else if (mission->get_image() == MISSION_FOUR)
    {
        score += MISSION_FOUR_REWARD;
        set_mission_text(MISSION_FOUR_REWARD_TEXT);
    }
    else if (mission->get_image() == MISSION_FIVE)
    {
        score += MISSION_FIVE_REWARD;
        set_mission_text(MISSION_FIVE_REWARD_TEXT);
    }
    else if (mission->get_image() == MISSION_SIX)
    {
        score -= MISSION_SIX_REWARD;
        set_mission_text(MISSION_SIX_REWARD_TEXT);
        sound_achievement = Engine::instance()->load_sound(SOUND_STRANGE_SOUND);
    }

    sound_achievement.play_once();
    hud->set_score(score);
    remove_mission();
    mission_accomplished = true;
}

void scenes::Game::remove_mission()
{
    Engine::instance()->erase(mission);
    mission = nullptr;
}

void scenes::Game::set_mission_text(std::string text)
{
    mission_count_down_ticks = MISSION_COUNT_DOWN_TICKS;

    mission_text->set_text(std::move(text));
    mission_text->set_y(100); // Should this be done an other way?
    mission_text->set_font(FONT, 30);
    Engine::instance()->draw(mission_text);
}

bool scenes::Game::can_pickup_mission()
{
    if (mission == nullptr) return false;

    std::vector<std::shared_ptr<level::GridTile>> player_current_tiles{player->get_current_tiles()};

    if (mission->will_be_visible())
    {
        // Get tile
        std::shared_ptr<level::GridTile> i_tile;
        auto rect = mission->get_rect();
        auto size = Engine::instance()->get_size();

        auto total_x = map->get_total_width();
        auto total_y = map->get_total_height();

        auto center_x = size.w / 2;
        auto center_y = size.h / 2;

        int index_x = ((total_x / 2) + (map->x - (center_x - rect.x))) / TILE_SIZE;
        int index_y = ((total_y / 2) + (map->y - (center_y - rect.y))) / TILE_SIZE;

        auto grid = map->get_map();

        i_tile = grid.at(index_y).at(index_x);

        for (auto& p_tile : player_current_tiles)
        {
            if (p_tile == i_tile)
            {
                return true;
            }
        }
    }

    return false;
}
