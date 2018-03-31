#include <iostream>
#include "src/game.h"
#include "src/tools.h"

int main() {
    int size;

    std::cout << "How large will you map be?" << std::endl;
    tools::ask_for_input(&size);

    game c_game = game(size);

    while (c_game.input != "q") {
        c_game.render_map();

        c_game.ask_for_input();
    }

    return 0;
}