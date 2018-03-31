#include "LoadGameButton.h"

LoadGameButton::LoadGameButton(const std::string& text, std::function<void(std::string)> button_callback,
                               std::string save_file_name, int x, int y, Position anchor) :
    MenuButton(text, std::bind(&LoadGameButton::button_clicked, this), x, y, anchor),
    button_callback(std::move(button_callback)), save_file_name(std::move(save_file_name))
{
}

void LoadGameButton::button_clicked()
{
    button_callback(save_file_name);
}
