#ifndef SDL_PROJECT_MAPPING_H
#define SDL_PROJECT_MAPPING_H

#include "SDL.h"
#include <map>
#include <string>

namespace engine
{
    enum Buttons
    {
        none = 0,
        up = 1,
        down = 2,
        left = 3,
        right = 4,
        action = 5,
        menu = 6,
        cancel = 7,
        confirm = 8,
        primary = 9,
        secondary = 10,
        cheat_health = 11,
        cheat_kill_enemies = 12,
        cheat_armor = 13,
        cheat_game_over = 14
    };

    enum KeyButtons
    {
        key_none = 0,
        key_up = 1,
        key_down = 2,
        key_left = 3,
        key_right = 4,
        key_action = 5,
        key_menu = 6,
        key_cancel = 7,
        key_confirm = 8,
        key_cheat_health = 11,
        key_cheat_dead_enemy = 12,
        key_cheat_armor = 13,
        key_cheat_game_over = 14
    };

    enum MouseButtons
    {
        mouse_none = 0,
        mouse_primary = 9,
        mouse_secondary = 10,
    };

    static const std::string EnumKeys[] = {
        "none",
        "up",
        "down",
        "left",
        "right",
        "action",
        "menu",
        "cancel",
        "confirm",
        "primary",
        "secondary",
    };

    class Mapping
    {
        std::map<KeyButtons, SDL_Keycode> keyboard_mapping;
        std::map<KeyButtons, std::pair<std::string, SDL_Keycode>> joystick_mapping;
        std::map<MouseButtons, SDL_Keycode> mouse_mapping;

    public:
        Mapping();

        // Keyboard

        std::map<KeyButtons, SDL_Keycode> get_keyboard_mapping() const;

        std::string get_keyboard_mapping_as_json();

        void set_keyboard_key(KeyButtons action, SDL_Keycode keycode);

        void set_keyboard_mapping_from_json(const std::string& string);

        // Joystick

        std::map<KeyButtons, std::pair<std::string, SDL_Keycode>> get_joystick_mapping() const;

        std::string get_joystick_mapping_as_json();

        void set_joystick_key(KeyButtons action, std::string type, SDL_Keycode keycode);

        void set_joystick_mapping_from_json(const std::string& string);
    };
}

#endif
