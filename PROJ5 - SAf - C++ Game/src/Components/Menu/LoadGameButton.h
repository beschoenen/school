#ifndef GAME_LOADGAMEBUTTON_H
#define GAME_LOADGAMEBUTTON_H

#include "MenuButton.h"

class LoadGameButton : public MenuButton
{
    std::string save_file_name;
    std::function<void(std::string)> button_callback;

public:
    LoadGameButton(const std::string& text, std::function<void(std::string)> button_callback,
                   std::string save_file_name, int x, int y, Position anchor = POS_TOP_LEFT);

    void button_clicked();
};


#endif //GAME_LOADGAMEBUTTON_H
