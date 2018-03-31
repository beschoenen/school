#include "gtest/gtest.h"
#include "gmock/gmock.h"

#include "../src/Level/Tiles/GridTile.h"
#include "../src/Scenes/Game.h"
#include "../src/Level/Saves/SaveFileGenerator.h"
#include "../src/Engine/Managers/SaveFileManager.h"
#include "../src/Engine/Engine.h"
#include "../src/Scenes/GamePause.h"
#include "../src/Scenes/MainMenu.h"

TEST(GridTiles, TypesAndAccessibility)
{
    level::GridTile tile{GridTileTypes::ground};
    EXPECT_TRUE(tile.get_accessible());

    tile.set_grid_tile_type(GridTileTypes::camp);
    EXPECT_TRUE(tile.get_accessible());

    tile.set_grid_tile_type(GridTileTypes::campfire);
    EXPECT_FALSE(tile.get_accessible());

    tile.set_grid_tile_type(GridTileTypes::portal);
    EXPECT_TRUE(tile.get_accessible());

    tile.set_grid_tile_type(GridTileTypes::wall);
    EXPECT_FALSE(tile.get_accessible());

    EXPECT_EQ(GridTileTypes::wall, tile.get_grid_tile_type());
}

// Mock class
class MockGame : public scenes::Game
{
public:
    MockGame() : scenes::Game() {}
};

using ::testing::Return;
using ::testing::_;

TEST(GameTest, Round)
{
    MockGame game;
    EXPECT_EQ(0, game.get_round());
    game.remove_enemies();
    game.clear_enemies();

    for (int i = 1; i <= 10; i++)
    {
        game.start_new_round();

        if (i == 10)
        {
            EXPECT_FALSE(game.open_portal_if_needed());
        }

        game.remove_enemies();
        game.clear_enemies();
        EXPECT_EQ(i, game.get_round());
    }

    EXPECT_TRUE(game.open_portal_if_needed());
    game.create_game();
    EXPECT_EQ(0, game.get_round());
}

TEST(SaveFiles, CreateAndReadSaveFileDidNotFinishRound)
{
    std::string save_file_name{"UNIT_TEST_SAVE_FILE"};
    int expected_health = 80;

    auto engine = Engine::instance();

    // Create Save file
    level::SaveFileGenerator generator;
    std::shared_ptr<scenes::Game> game = std::make_shared<scenes::Game>(scenes::Game());
    game->enter_scene();

    for (int i = 1; i <= 4; i++)
    {
        game->start_new_round();

        if (i != 4)
        {
            game->remove_enemies();
            game->clear_enemies();
        }
    }

    game->get_player()->set_health_points_start_round(expected_health);
    game->get_player()->health_potion();

    std::string json_map{generator.get_map(game)};
    std::string json_info{generator.get_info(game)};

    engine->create_save_file(save_file_name, json_info, json_map);

    // Read Save file
    engine->load_save_file(save_file_name);
    std::shared_ptr<engine::SaveFileData> save_file_data{engine->get_save_file_data()};
    EXPECT_NE(nullptr, save_file_data);
    EXPECT_EQ(1, save_file_data->get_level());
    EXPECT_EQ(4, save_file_data->get_round());
    EXPECT_EQ(0, save_file_data->get_score());
    EXPECT_EQ(expected_health, save_file_data->get_player()->get_health_points());
}

TEST(SaveFiles, CreateAndReadSaveFileFinishedRound)
{
    std::string save_file_name{"UNIT_TEST_SAVE_FILE"};
    int expected_health = 100;

    auto engine = Engine::instance();

    // Create Save file
    level::SaveFileGenerator generator;
    std::shared_ptr<scenes::Game> game = std::make_shared<scenes::Game>(scenes::Game());
    game->enter_scene();

    for (int i = 1; i <= 4; i++)
    {
        game->start_new_round();
        game->remove_enemies();
        game->clear_enemies();
    }

    game->get_player()->set_health_points_start_round(80);
    game->get_player()->health_potion();

    std::string json_map{generator.get_map(game)};
    std::string json_info{generator.get_info(game)};

    engine->create_save_file(save_file_name, json_info, json_map);

    // Read Save file
    engine->load_save_file(save_file_name);
    std::shared_ptr<engine::SaveFileData> save_file_data{engine->get_save_file_data()};
    EXPECT_NE(nullptr, save_file_data);
    EXPECT_EQ(1, save_file_data->get_level());
    EXPECT_EQ(5, save_file_data->get_round());
    EXPECT_EQ(0, save_file_data->get_score());
    EXPECT_EQ(expected_health, save_file_data->get_player()->get_health_points());
}

TEST(SaveFiles, ItegritySaveFileException)
{
    std::string save_file_name{"UNIT_TEST_SAVE_FILE_WRONG"};

    auto engine = Engine::instance();

    // Create Save file
    level::SaveFileGenerator generator;
    std::shared_ptr<scenes::Game> game = std::make_shared<scenes::Game>(scenes::Game());
    game->enter_scene();

    for (int i = 1; i <= 4; i++)
    {
        game->start_new_round();

        if (i != 4)
        {
            game->remove_enemies();
            game->clear_enemies();
        }
    }

    std::string json_map{generator.get_map(game)};
    std::string json_info{generator.get_info(game)};
    std::string json_map_wrong = json_map + " ";

    engine->create_save_file(save_file_name + "_WRONG", json_info, json_map_wrong);

    // Read wrong Save file
    ASSERT_THROW(engine->load_save_file(save_file_name + "_WRONG"), std::string);
    try
    {
        engine->load_save_file(save_file_name + "_WRONG");
    }
    catch (std::string& e)
    {
        EXPECT_EQ(e, "Invalid save file");
    }

    std::shared_ptr<engine::SaveFileData> invalid_save_file_data{engine->get_save_file_data()};
    EXPECT_EQ(nullptr, invalid_save_file_data);
}

TEST(SaveFiles, ListOfSaveFiles)
{
    std::string save_file_name{"UNIT_TEST_SAVE_FILE"};

    auto engine = Engine::instance();
    engine->create_save_file(save_file_name, "info", "map");

    // List save files
    std::unordered_map<std::string, bool> save_files{engine->list_save_files()};
    auto found_save_file = save_files.find(save_file_name);

    EXPECT_NE(found_save_file, save_files.end());
}

TEST(PauseMenu, PauseAndContinue)
{
    std::shared_ptr<scenes::Game> game = std::make_shared<scenes::Game>(scenes::Game());
    game->enter_scene();

    for (int i = 1; i <= 4; i++)
    {
        game->start_new_round();

        if (i != 4)
        {
            game->remove_enemies();
            game->clear_enemies();
        }
    }

    std::vector<std::shared_ptr<Mob>> mobs_before{game->get_enemies()};

    Engine::instance()->go_to_scene<scenes::GamePause>();
    Engine::instance()->exit_current_scene(game);

    std::vector<std::shared_ptr<Mob>> mobs_after{game->get_enemies()};

    EXPECT_EQ(mobs_before.size(), mobs_after.size());

    game->leave_scene();
}

TEST(Debug, Test)
{
    auto engine = Engine::instance();
    delete engine;
    EXPECT_EQ(1, 1);
}

int main(int argc, char** argv)
{
    ::testing::InitGoogleMock(&argc, argv);
    return RUN_ALL_TESTS();
}
