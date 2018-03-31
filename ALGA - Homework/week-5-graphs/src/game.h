//
// Created by Kevin Richter on 27/09/2017.
//

#ifndef WEEK_5_GRAPHS_GAME_H
#define WEEK_5_GRAPHS_GAME_H


#include <iostream>
#include <string>

class game {
public:
    std::string input;

    explicit game(int size);

    void ask_for_input();
    void render_map();

private:
    void generate_map(int size);
};


#endif //WEEK_5_GRAPHS_GAME_H
