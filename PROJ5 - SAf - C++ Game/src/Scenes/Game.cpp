#include "Game.h"
#include "MainMenu.h"
#include "../Engine/Level/RandomLevel.h"
#include "../Engine/Engine.h"
#include "../Utilities/Random.h"
#include "../defines.h"
#include "WinScreen.h"
#include "LoseScreen.h"
#include <ctime>
#include <iostream>
#include "GamePause.h"
#include "LoadScreen.h"
#include <sstream>
#include <algorithm>
#include "../States/Mob/MobFollowing.h"
#include "../Engine/Level/FileLevel.h"
#include "../States/Player/PlayerNull.h"
#include "LoseScreen.h"
#include <algorithm>
#include <ctime>

scenes::Game::Game() : Game(nullptr)
{
}

scenes::Game::Game(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    player = std::make_shared<Player>(map, 100, 100, 5, 8);
    player->set_state(std::make_shared<PlayerNull>());
    player->set_entity(player);

    light_overlay = std::make_shared<LightOverlay>(LIGHT_PERCENTAGE);
    hud = std::make_shared<Hud>();

    init_game();

    // Campfire
    auto location_campfire = map->campfire_location();
    camp_fire = std::make_shared<Campfire>(location_campfire.x, location_campfire.y);

    // Portal
    auto location_portal = map->portal_location();
    portal = std::make_shared<Portal>(location_portal.x, location_portal.y);

    // Progress
    dungeon_progress_text = std::make_shared<DungeonProgressText>("Round 1");

    // Attack
    attack_text = std::make_shared<MobHitText>("0");

    // Direction Arrow
    arrow = std::make_shared<NavigationArrow>(map->x, map->y);

    // Mission
    mission_text = std::make_shared<MissionText>("null");
    mission = std::shared_ptr<Mission>(nullptr);

    // Sounds
    sound_bottle = Engine::instance()->load_sound(SOUND_BOTTLE);
    sound_cloth = Engine::instance()->load_sound(SOUND_CLOTH);
    sound_eating = Engine::instance()->load_sound(SOUND_EATING);
    sound_low_health = Engine::instance()->load_sound(SOUND_LOW_HEALTH);
    sound_footstep = Engine::instance()->load_sound(SOUND_FOOTSTEP);
    sound_attack_sword = Engine::instance()->load_sound(SOUND_SWORD_ATTACK);
    sound_mob_hit = Engine::instance()->load_sound(SOUND_MOB_HIT);
    sound_mob_dead = Engine::instance()->load_sound(SOUND_MOB_DEAD);
    sound_portal = Engine::instance()->load_sound(SOUND_PORTAL);
    sound_achievement = Engine::instance()->load_sound(SOUND_ACHIEVEMENT);

    set_ambience();

    sound_mob_hit.set_volume(40);
    sound_mob_dead.set_volume(40);
}

void scenes::Game::init_game()
{
    auto load_screen = std::dynamic_pointer_cast<LoadScreen>(old_scene);
    if (load_screen == nullptr)
    {
        // New game
        create_game();
        hud->set_round(round + 1);
        player = std::make_shared<Player>(Player(map, 100, 100, 5, 8));
        return;
    }

    // Save file
    std::shared_ptr<engine::SaveFileData> save_file_data = Engine::instance()->get_save_file_data();
    level = save_file_data->get_level();
    round = save_file_data->get_round() - 1;
    score = save_file_data->get_score();
    map = save_file_data->get_map();
    player = save_file_data->get_player();
    player->set_map(map);

    // Hud
    hud->set_level(level);
    hud->set_round((round + 1));
    hud->set_score(score);
    hud->set_health(player->get_health_points());
    hud->set_max_health(player->get_max_health_points());

    load_game();
}

void scenes::Game::set_ambience()
{
    int number = Random::instance().get(1, 100);

    if (number < 25)
    {
        sound_ambience = Engine::instance()->load_sound(SOUND_AMBIENCE_CAVE_0);
    }
    else if (number > 25 && number < 50)
    {
        sound_ambience = Engine::instance()->load_sound(SOUND_AMBIENCE_CAVE_1);
    }
    else if (number > 50 && number < 75)
    {
        sound_ambience = Engine::instance()->load_sound(SOUND_AMBIENCE_CAVE_2);
    }
    else
    {
        sound_ambience = Engine::instance()->load_sound(SOUND_AMBIENCE_CAVE_3);
    }
}

int scenes::Game::get_level() const
{
    return level;
}

int scenes::Game::get_round() const
{
    return round;
}

int scenes::Game::get_score() const
{
    return score;
}

int scenes::Game::get_game_speed() const
{
    return game_speed;
}

void scenes::Game::set_game_speed(int newspeed)
{
    game_speed = newspeed;
}

int scenes::Game::get_score_start_round() const
{
    return score_start_round;
}

std::shared_ptr<engine::Map> scenes::Game::get_map() const
{
    return map;
}

void scenes::Game::create_game()
{
    // Round and countdown
    round = 0;
    count_down = COUNT_DOWN;
    count_down_ticks = COUNT_DOWN_TICKS;
    show_progress_counter = COUNT_ROUND_TICKS;

    // Map
    auto mainmenu = std::dynamic_pointer_cast<MainMenu>(old_scene);
    if (mainmenu == nullptr || mainmenu->get_level_loader() == nullptr)
    {
        int layers = 1 + level;
        auto random_level = std::make_shared<engine::RandomLevel>();

        map = std::make_shared<engine::Map>(random_level, layers);
    }
    else
    {
        map = std::make_shared<engine::Map>(mainmenu->get_level_loader());
    }

    if (level > 1)
    {
        player->set_map(map);

        auto location_campfire = map->campfire_location();
        camp_fire->set_x(location_campfire.x);
        camp_fire->set_y(location_campfire.y);

        // Portal
        auto location_portal = map->portal_location();
        portal->set_x(location_portal.x);
        portal->set_y(location_portal.y);
    }
}

void scenes::Game::load_game()
{
    count_down = COUNT_DOWN;
    count_down_ticks = COUNT_DOWN_TICKS;
    show_progress_counter = COUNT_ROUND_TICKS;

    int light = std::max(LIGHT_PERCENTAGE_MIN, LIGHT_PERCENTAGE - (LIGHT_PERCENTAGE_CHANGE * (level - 1)));
    light_overlay->set_radius_percentage(light);

    if (level > 1)
    {
        player->set_map(map);
    }
}

void scenes::Game::enter_scene()
{
    sound_ambience.play();

    if (is_going_to_next_level)
    {
        level++;
        round = 0;
        is_going_to_next_level = false;
        portal_opened = false;
        Engine::instance()->erase(portal);
        create_game();
    }

    if (is_resuming)
    {
        set_enemies();
        set_items();
    }

    Engine::instance()->set_map(map);
    Engine::instance()->draw(camp_fire);

    set_enemies();

    Engine::instance()->draw(player);
    Engine::instance()->draw(attack_text);

    if (countdown_checked || countdown_started || portal_opened || show_progress_counter > 0)
    {
        Engine::instance()->draw(dungeon_progress_text);
    }

    if (mission != nullptr)
    {
        Engine::instance()->draw(mission);
    }

    if (portal_opened)
    {
        Engine::instance()->draw(portal);
    }

    Engine::instance()->draw(light_overlay);
    Engine::instance()->draw(arrow);

    hud->draw();
}

void scenes::Game::leave_scene()
{
    if (mission_accomplished)
    {
        sound_achievement.stop();
    }

    sound_ambience.stop();

    Engine::instance()->remove_map();
    Engine::instance()->erase(camp_fire);

    remove_enemies();
    remove_items();

    Engine::instance()->erase(std::dynamic_pointer_cast<engine::Renderable>(player));
    Engine::instance()->erase(dungeon_progress_text);
    Engine::instance()->erase(mission_text);
    Engine::instance()->erase(portal);
    Engine::instance()->erase(attack_text);
    Engine::instance()->erase(light_overlay);
    Engine::instance()->erase(arrow);
    Engine::instance()->erase(mission);

    hud->erase();
}

void scenes::Game::update()
{
    ticker++;
    attack_text_ticker++;

    if (attack_text_ticker == ATTACK_COUNT_DOWN_TICKS)
    {
        attack_text->clear_mob();
    }

    if (ticker % 5 == 0)
    {
        if (attack)
        {
            player_attack();
        }
    }

    if (ticker % 15 == 0)
    {
        player->next_frame();

        for (const auto& enemy : enemies)
        {
            enemy->next_frame();
        }

        ticker = 0;
    }

    for (int i = 0; i < game_speed; i++)
    {
        move_player();

        player->update();

        for (auto& enemy : enemies)
        {
            enemy->go_to_position(player->get_current_tiles()[0]);
            enemy->update();

            if (player->get_health_points() == 0)
            {
                // Player died
                Engine::instance()->go_to_scene<LoseScreen>();
                return;
            }

            if (enemy == attack_text->get_mob())
            {
                attack_text->set_x(enemy->get_x());
                attack_text->set_y(enemy->get_y() - 30);
                attack_text->set_mob(enemy);
            }
        }
    }

    if (portal_ticker >= 0)
    {
        if (portal_ticker < 60)
        {
            portal_ticker++;
        }
        else
        {
            portal_ticker = -1;
            Engine::instance()->go_to_scene<WinScreen>();
        }
    }

    if (mission_count_down_ticks > 0)
    {
        mission_count_down_ticks--;
    }
    else
    {
        Engine::instance()->erase(mission_text);
    }

    handle_count_down_and_rounds();
    show_rounds();
    check_health();
}

void scenes::Game::input(InputCollection input)
{
    if (input[engine::Buttons::menu])
    {
        is_resuming = true;
        Engine::instance()->go_to_scene<GamePause>();
        return;
    }

    if (input[engine::Buttons::action])
    {
        handle_action();
    }
    else
    {
        attack = false;
    }

    move_up = input[engine::Buttons::up];
    if (move_up)
    {
        if (player->go_to_walking_phase())
        {
            ticker = 0;
        }
    }

    move_down = input[engine::Buttons::down];
    if (move_down)
    {
        if (player->go_to_walking_phase())
        {
            ticker = 0;
        }
    }

    move_left = input[engine::Buttons::left];
    if (move_left)
    {
        if (player->go_to_walking_phase())
        {
            ticker = 0;
        }
    }

    move_right = input[engine::Buttons::right];
    if (move_right)
    {
        if (player->go_to_walking_phase())
        {
            ticker = 0;
        }
    }

    if (!(move_down || move_left || move_right || move_up))
    {
        if (!attack && player->go_to_looking_phase())
        {
            ticker = 0;
        }
    }

    for (const auto& enemy : enemies)
    {
        enemy->go_to_looking_phase();
    }

    //cheats
    bool cheat_kill_enemies = input[engine::Buttons::cheat_kill_enemies];
    bool cheat_health = input[engine::Buttons::cheat_health];
    bool cheat_armor = input[engine::Buttons::cheat_armor];
    bool cheat_game_over = input[engine::Buttons::cheat_game_over];

    if (cheat_kill_enemies)
    {
        remove_enemies();
        enemies.clear();
    }

    if (cheat_health)
    {
        int new_health = player->get_max_health_points();
        player->set_health_point(new_health);
        hud->set_health(new_health);
    }

    if (cheat_armor)
    {
        player->add_armor();
    }

    if (cheat_game_over)
    {
        int new_health = player->get_health_points();
        player->take_damage(new_health);
        hud->set_health(new_health);
        Engine::instance()->go_to_scene<LoseScreen>();
    }
}

void scenes::Game::handle_count_down_and_rounds()
{
    // Check and open portal if needed
    if (open_portal_if_needed()) return;

    // Check round
    if (round >= GAME_ROUNDS) return;

    // Check if there are any enemies
    if (!enemies.empty()) return;

    // Check if player is in the camp or not
    if (!countdown_started && !check_if_count_down_can_start()) return;

    // Count down
    if (count_down_ticks > 0)
    {
        count_down_ticks--;
    }
    else
    {
        count_down--;

        if (count_down > 0)
        {
            // Continue count down
            dungeon_progress_text->set_text(std::to_string(count_down));
            count_down_ticks = COUNT_DOWN_TICKS;
        }
        else
        {
            // Count down finished, start new round
            start_new_round();
        }
    }
}

bool scenes::Game::check_if_count_down_can_start()
{
    if (!countdown_started)
    {
        if (!countdown_checked)
        {
            Engine::instance()->draw(dungeon_progress_text);
        }

        if (player->is_in_camp())
        {
            // Player is in the camp, start count down
            countdown_checked = false;
            countdown_started = true;
            dungeon_progress_text->set_text(std::to_string(count_down));
            dungeon_progress_text->set_font(FONT, 50);
        }
        else
        {
            // Player is not in the camp, player must return to the camp first
            if (!countdown_checked)
            {
                dungeon_progress_text->set_text("Return to the camp!");
                dungeon_progress_text->set_font(FONT, 21);
            }
            countdown_checked = true;
            return false;
        }
    }

    return true;
}

void scenes::Game::start_new_round()
{
    is_resuming = false;

    // Count down check reset
    countdown_started = false;

    // Increase round
    round++;
    score_start_round = score;
    player->set_health_points_start_round(player->get_health_points());

    // Update text
    dungeon_progress_text->set_text("Round " + std::to_string(round));
    hud->set_level(level);
    hud->set_round(round);

    // Reset count down and timers
    count_down = COUNT_DOWN;
    count_down_ticks = COUNT_DOWN_TICKS;
    show_progress_counter = COUNT_ROUND_TICKS;

    // Reset potions
    player->reset_attack_potion();

    // Spawn and set enemies
    spawn_enemies(calculate_amount_of_enemies());
    set_enemies();

    // Spawn and set items
    remove_items();
    items.clear();
    spawn_items();
    set_items();
    remove_mission();

    // Mission
    bool do_mission = round_has_mission();
    if (do_mission)
    {
        int mission = choose_mission();
        if (mission == 1)
        {
            create_mission(MISSION_ONE, MISSION_TEXT_ONE);
        }
        else if (mission == 2)
        {
            create_mission(MISSION_TWO, MISSION_TEXT_TWO);
        }
        else if (mission == 3)
        {
            create_mission(MISSION_THREE, MISSION_TEXT_THREE);
        }
        else if (mission == 4)
        {
            create_mission(MISSION_FOUR, MISSION_TEXT_FOUR);
        }
        else if (mission == 5)
        {
            create_mission(MISSION_FIVE, MISSION_TEXT_FIVE);
        }
        else if (mission == 6)
        {
            create_mission(MISSION_SIX, MISSION_TEXT_SIX);
        }
    }
}

void scenes::Game::show_rounds()
{
    if (enemies.empty()) return;

    if (show_progress_counter > 0)
    {
        show_progress_counter--;
    }
    else if (show_progress_counter == 0)
    {
        Engine::instance()->erase(dungeon_progress_text);
        show_progress_counter = -1;
    }
}

bool scenes::Game::open_portal_if_needed()
{
    // Check if portal should be opened
    if (portal_opened || !enemies.empty() || round != GAME_ROUNDS) return false;

    // Open portal
    dungeon_progress_text->set_text("The enemies were defeated! A mysterious portal has appeared in the camp!");
    dungeon_progress_text->set_font(FONT, 16);
    Engine::instance()->draw(dungeon_progress_text);
    Engine::instance()->draw(portal);
    portal_opened = true;

    return true;
}

void scenes::Game::handle_going_through_portal()
{
    std::vector<std::shared_ptr<level::GridTile>> current_tiles{player->get_current_tiles()};

    for (const auto& current_tile : current_tiles)
    {
        if (current_tile->get_grid_tile_type() == GridTileTypes::portal)
        {
            sound_portal.play_once();
            is_going_to_next_level = true;
            hud->set_level(level);
            hud->set_round(round);
            portal_opened = false;

            if (portal_ticker == -1)
            {
                portal_ticker = 0;
            }

            break;
        }
    }
}
