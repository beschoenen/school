//
// Created by Kevin Richter on 27/09/2017.
//

#include "game.h"
#include "tools.h"

game::game(int size) {
    this->generate_map(size);
}

void game::ask_for_input() {

    tools::ask_for_input(&input);
}

void game::render_map() {

}

void game::generate_map(int size) {

}
