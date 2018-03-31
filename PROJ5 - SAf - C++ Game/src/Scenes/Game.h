#ifndef GAME_GAME_H
#define GAME_GAME_H

#include "../Engine/Base/Scene.h"
#include "../aliases.h"
#include "../defines_mission.h"
#include "../defines_item.h"
#include "../Engine/Base/Map.h"
#include "../Components/Player.h"
#include "../Components/Debug/CurrentTiles.h"
#include "../Components/Mob.h"
#include "../Components/Game/DungeonProgressText.h"
#include "../Components/Game/MissionText.h"
#include "../Components/Game/MobHitText.h"
#include "../Components/Hud.h"
#include "../Components/Game/Level/LightOverlay.h"
#include "../Components/Game/Level/Campfire.h"
#include "../Components/Game/Level/Portal.h"
#include "../Components/Game/NavigationArrow.h"
#include "../Engine/Models/Sound.h"
#include "../Components/Item.h"
#include "../Components/Mission.h"

namespace scenes
{
    class Game : public engine::Scene
    {
        int ticker = 0;
        int portal_ticker = -1;

        int level = 1;
        int round = 0;
        int score = 0;
        int score_start_round = 0;

        int game_speed = GAME_SPEED;

        bool is_resuming = false;

        int item_health_potions = 0;
        int item_armor_potions = 0;
        int item_attack_potions = 0;
        int item_food = 0;
        int item_armor = 0;

        bool countdown_checked = false;
        bool countdown_started = false;
        bool portal_opened = false;
        bool is_going_to_next_level = false;

        int count_down = COUNT_DOWN;
        int count_down_ticks = COUNT_DOWN_TICKS;
        int show_progress_counter = COUNT_ROUND_TICKS;
        int mission_count_down_ticks = MISSION_COUNT_DOWN_TICKS;
        int attack_text_ticker = ATTACK_COUNT_DOWN_TICKS;

        bool attack = false;
        bool move_up = false;
        bool move_down = false;
        bool move_left = false;
        bool move_right = false;

        std::shared_ptr<MissionText> mission_text;
        std::shared_ptr<MobHitText> attack_text;
        std::shared_ptr<DungeonProgressText> dungeon_progress_text;
        std::shared_ptr<engine::Map> map;
        std::shared_ptr<LightOverlay> light_overlay;
        std::shared_ptr<Hud> hud;
        std::shared_ptr<Player> player;
        std::shared_ptr<Campfire> camp_fire;
        std::shared_ptr<NavigationArrow> arrow;

        std::shared_ptr<Mission> mission;
        bool mission_accomplished = false;
        std::vector<std::shared_ptr<Mob>> enemies;
        std::vector<std::shared_ptr<Item>> items;

        std::shared_ptr<Portal> portal;

        engine::Sound sound_bottle = engine::Sound(nullptr);
        engine::Sound sound_cloth = engine::Sound(nullptr);
        engine::Sound sound_eating = engine::Sound(nullptr);
        engine::Sound sound_ambience = engine::Sound(nullptr);
        engine::Sound sound_low_health = engine::Sound(nullptr);
        engine::Sound sound_footstep = engine::Sound(nullptr);
        engine::Sound sound_attack_sword = engine::Sound(nullptr);
        engine::Sound sound_mob_hit = engine::Sound(nullptr);
        engine::Sound sound_mob_dead = engine::Sound(nullptr);
        engine::Sound sound_portal = engine::Sound(nullptr);
        engine::Sound sound_achievement = engine::Sound(nullptr);
        
        bool low_health_sound_playing = false;

    public:
        Game();

        explicit Game(std::shared_ptr<engine::Scene> old_scene);

        void init_game();

        void set_ambience();

        void create_game();

        void load_game();

        void enter_scene() override;

        void leave_scene() override;

        void update() override;

        int get_level() const;

        int get_round() const;

        int get_score() const;

        int get_game_speed() const;

        void set_game_speed(int newspeed);

        int get_score_start_round() const;

        std::shared_ptr<engine::Map> get_map() const;

        void input(InputCollection input) override;

        void handle_count_down_and_rounds();

        bool check_if_count_down_can_start();

        void start_new_round();

        void show_rounds();

        bool open_portal_if_needed();

        void handle_going_through_portal();

        // Player
        std::shared_ptr<Player> get_player() const;

        void move_player();

        void player_attack();

        void handle_action();

        void check_health();

        // Enemies
        std::vector<std::shared_ptr<Mob>> get_enemies() const;

        void set_enemies();

        void remove_enemies();

        void clear_enemies();

        void spawn_enemies(int amount);

        void attack_mob(std::shared_ptr<Mob> mob, int damage);

        int calculate_amount_of_enemies();

        // Items
        void set_items();

        void remove_items();

        void remove_item(std::shared_ptr<Item> item);

        void spawn_items();

        void spawn_health_potions(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                  LevelMatrix map, int height, int width, int amount);

        void spawn_armor_potions(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                 LevelMatrix map, int height, int width, int amount);

        void spawn_attack_potions(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                  LevelMatrix map, int height, int width, int amount);

        void spawn_food(std::vector<std::shared_ptr<level::GridTile>> tiles,
                        LevelMatrix map, int height, int width, int amount);

        void spawn_armor(std::vector<std::shared_ptr<level::GridTile>> tiles,
                         LevelMatrix map, int height, int width, int amount);

        std::tuple<int*, int*> spawn_item_location(std::vector<std::shared_ptr<level::GridTile>> tiles,
                                                   LevelMatrix map, int height, int width);

        std::shared_ptr<Item> can_pickup_item();

        void pickup_item(std::shared_ptr<Item> item);

        // Mission
        void spawn_mission(std::vector<std::shared_ptr<level::GridTile>> tiles,
                           LevelMatrix map, int height, int width, std::string image);

        bool round_has_mission();

        int choose_mission();

        void create_mission(std::string image, std::string text);

        void remove_mission();

        void finish_mission();

        void set_mission_text(std::string text);

        bool can_pickup_mission();

    };
}

#endif //GAME_GAME_H
