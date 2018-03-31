//
// Created by Kevin Richter on 27/09/2017.
//

#include <iostream>
#include "tools.h"

void tools::ask_for_input(std::string *input) {
    std::cout << std::endl << "-> ";
    std::cin >> *input;
}

void tools::ask_for_input(int *input) {
    std::string user_input = std::string();

    while (!tools::is_int(user_input)) {
        if (user_input != std::string()) {
            std::cout << "invalid input, please try again" << std::endl;
        }

        tools::ask_for_input(&user_input);
    }

    *input = std::stoi(user_input);
}

bool tools::is_int(std::string input) {
    try
    {
        std::stoi(input);
        return true;
    }
    catch(std::exception& exception)
    {
        return false;
    }
}
