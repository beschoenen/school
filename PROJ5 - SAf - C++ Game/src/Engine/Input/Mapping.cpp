#include "Mapping.h"
#include "../../../lib/json/src/json.hpp"
#include "../../defines.h"

engine::Mapping::Mapping()
{
    keyboard_mapping[engine::key_up] = SDLK_UP;
    keyboard_mapping[engine::key_down] = SDLK_DOWN;
    keyboard_mapping[engine::key_left] = SDLK_LEFT;
    keyboard_mapping[engine::key_right] = SDLK_RIGHT;
    keyboard_mapping[engine::key_action] = SDLK_SPACE;
    keyboard_mapping[engine::key_menu] = SDLK_ESCAPE;
    keyboard_mapping[engine::key_cancel] = SDLK_BACKSPACE;
    keyboard_mapping[engine::key_confirm] = SDLK_RETURN;

    keyboard_mapping[engine::key_cheat_dead_enemy] = SDLK_7;
    keyboard_mapping[engine::key_cheat_armor] = SDLK_8;
    keyboard_mapping[engine::key_cheat_health] = SDLK_9;
    keyboard_mapping[engine::key_cheat_game_over] = SDLK_0;

    mouse_mapping[engine::mouse_primary] = SDL_BUTTON_LEFT;
    mouse_mapping[engine::mouse_secondary] = SDL_BUTTON_RIGHT;

    joystick_mapping[engine::key_up] = std::pair<std::string, SDL_Keycode>(JOY_HAT, SDL_HAT_UP);
    joystick_mapping[engine::key_down] = std::pair<std::string, SDL_Keycode>(JOY_HAT, SDL_HAT_DOWN);
    joystick_mapping[engine::key_left] = std::pair<std::string, SDL_Keycode>(JOY_HAT, SDL_HAT_LEFT);
    joystick_mapping[engine::key_right] = std::pair<std::string, SDL_Keycode>(JOY_HAT, SDL_HAT_RIGHT);
    joystick_mapping[engine::key_action] = std::pair<std::string, SDL_Keycode>(JBUTTON, SDL_CONTROLLER_BUTTON_A);
    joystick_mapping[engine::key_menu] = std::pair<std::string, SDL_Keycode>(JBUTTON, SDL_CONTROLLER_BUTTON_START);
    joystick_mapping[engine::key_cancel] = std::pair<std::string, SDL_Keycode>(JBUTTON, SDL_CONTROLLER_BUTTON_B);
    joystick_mapping[engine::key_confirm] = std::pair<std::string, SDL_Keycode>(JBUTTON, SDL_CONTROLLER_BUTTON_GUIDE);
}


std::map<engine::KeyButtons, SDL_Keycode> engine::Mapping::get_keyboard_mapping() const
{
    return keyboard_mapping;
}

std::string engine::Mapping::get_keyboard_mapping_as_json()
{
    nlohmann::json json;

    json[EnumKeys[KeyButtons::key_up]] = keyboard_mapping[KeyButtons::key_up];
    json[EnumKeys[KeyButtons::key_down]] = keyboard_mapping[KeyButtons::key_down];
    json[EnumKeys[KeyButtons::key_left]] = keyboard_mapping[KeyButtons::key_left];
    json[EnumKeys[KeyButtons::key_right]] = keyboard_mapping[KeyButtons::key_right];
    json[EnumKeys[KeyButtons::key_action]] = keyboard_mapping[KeyButtons::key_action];
    json[EnumKeys[KeyButtons::key_menu]] = keyboard_mapping[KeyButtons::key_menu];
    json[EnumKeys[KeyButtons::key_cancel]] = keyboard_mapping[KeyButtons::key_cancel];
    json[EnumKeys[KeyButtons::key_confirm]] = keyboard_mapping[KeyButtons::key_confirm];

    return json.dump(2);
}

void engine::Mapping::set_keyboard_key(engine::KeyButtons action, SDL_Keycode keycode)
{
    keyboard_mapping[action] = keycode;
}

void engine::Mapping::set_keyboard_mapping_from_json(const std::string& string)
{
    auto json = nlohmann::json::parse(string);

    for (nlohmann::json::iterator it = json.begin(); it != json.end(); ++it)
    {
        auto code = -1;
        for (auto i = 0; i < sizeof(EnumKeys); i++)
        {
            if (EnumKeys[i] == it.key())
            {
                code = i;
                break;
            }
        }

        if (code < 0) throw "Invalid JSON";

        set_keyboard_key((KeyButtons) code, it.value());
    }
}

std::map<engine::KeyButtons, std::pair<std::string, SDL_Keycode>> engine::Mapping::get_joystick_mapping() const
{
    return joystick_mapping;
}

std::string engine::Mapping::get_joystick_mapping_as_json()
{
    nlohmann::json json;

    json[EnumKeys[KeyButtons::key_up]] = joystick_mapping[KeyButtons::key_up];
    json[EnumKeys[KeyButtons::key_down]] = joystick_mapping[KeyButtons::key_down];
    json[EnumKeys[KeyButtons::key_left]] = joystick_mapping[KeyButtons::key_left];
    json[EnumKeys[KeyButtons::key_right]] = joystick_mapping[KeyButtons::key_right];
    json[EnumKeys[KeyButtons::key_action]] = joystick_mapping[KeyButtons::key_action];
    json[EnumKeys[KeyButtons::key_menu]] = joystick_mapping[KeyButtons::key_menu];
    json[EnumKeys[KeyButtons::key_cancel]] = joystick_mapping[KeyButtons::key_cancel];
    json[EnumKeys[KeyButtons::key_confirm]] = joystick_mapping[KeyButtons::key_confirm];

    return json.dump(2);
}

void engine::Mapping::set_joystick_key(engine::KeyButtons action, std::string type, SDL_Keycode keycode)
{
    joystick_mapping[action] = std::pair<std::string, SDL_Keycode>(type, keycode);
}

void engine::Mapping::set_joystick_mapping_from_json(const std::string& string)
{
    auto json = nlohmann::json::parse(string);

    for (nlohmann::json::iterator it = json.begin(); it != json.end(); ++it)
    {
        auto code = -1;
        for (auto i = 0; i < sizeof(EnumKeys); i++)
        {
            if (EnumKeys[i] == it.key())
            {
                code = i;
                break;
            }
        }

        if (code < 0) throw "Invalid JSON";

        set_joystick_key((KeyButtons) code, it.value()[0], it.value()[1]);
    }
}
