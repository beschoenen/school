// CPPPirate.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include <iostream>
#include "game.h"

str* get_action()
{
    char action[100];

    std::cout << "-> ";
    std::cin.getline(action, 100);
    std::cout << std::endl;

    return new str(action);
}

int main()
{
    _CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);

    while (true)
    {
        system("cls");

        std::cout << "Quit game, type q or quit." << std::endl;
        std::cout << "Start game, press any key." << std::endl;

        const auto input = get_action();

        if (*input == "q" || *input == "quit")
        {
            delete input;
            break;
        }

        delete input;

        const auto game_instance = new game(new port("Roatan"));
        while (!game_instance->over)
        {
            system("cls");

            // Header
            std::cout
                << "======================================================================" << std::endl
                << " Gold: " << game_instance->gold << "\t"
                << " HP: " << game_instance->current_ship->current_health << "/" << game_instance->current_ship->health << "\t"
                << " Cannons: " << game_instance->current_ship->cannon_count() << "/" << game_instance->current_ship->cannon_space << "\t"
                << " Inventory: " << game_instance->current_ship->commodity_count() << "/" << game_instance->current_ship->space << std::endl
                << "======================================================================" << std::endl << std::endl;

            // Render
            const auto render = game_instance->render();
            std::cout << render->data() << std::endl << std::endl;
            delete render;

            // Check game over
            if (!game_instance->over)
            {
                game_instance->over = game_instance->current_ship->current_health <= 0 || game_instance->gold >= 1000000;
            }

            if (game_instance->over)
            {
                std::cout << "======================" << std::endl
                          << "      Game Over!" << std::endl
                          << "======================" << std::endl
                          << "You made " << game_instance->gold << " Gold." << std::endl << std::endl;

                delete get_action();
                break;
            }

            // Handle input
            const auto action = get_action();
            game_instance->handle_input(action);
            delete action;            
        }

        delete game_instance;

        _CrtDumpMemoryLeaks();
    }

    _CrtDumpMemoryLeaks();

    return 0;
}
