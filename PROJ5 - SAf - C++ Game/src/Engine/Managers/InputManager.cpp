#include "InputManager.h"
#include "../../Utilities/File.h"
#include "../../aliases.h"
#include <algorithm>

#define SDL_Flags (SDL_INIT_GAMECONTROLLER | SDL_INIT_JOYSTICK | SDL_INIT_EVENTS)

engine::InputManager::InputManager()
{
    if (SDL_InitSubSystem(SDL_Flags) != 0)
    {
        printf("SDL_Init: %s\n", SDL_GetError());
        exit(1);
    }

    set_mapping(Mapping());

    load_keyboard_bindings();
}

engine::InputManager::~InputManager()
{
    SDL_QuitSubSystem(SDL_Flags);
}

void engine::InputManager::handle_event(SDL_Event& event)
{
    switch (event.type)
    {
        case SDL_JOYAXISMOTION:
            handle_joystick(event);
            break;
        case SDL_JOYHATMOTION:
            joy_hat_toggle(event.jhat);
            break;
        case SDL_JOYBUTTONUP:
        case SDL_JOYBUTTONDOWN:
            joy_toggle(event.jbutton);
            break;
        case SDL_CONTROLLERBUTTONDOWN:
        case SDL_CONTROLLERBUTTONUP:
            joy_controller_toggle(event.cbutton);
            break;
        case SDL_JOYDEVICEADDED:
            add_joystick_device(event.jdevice);
            break;
        case SDL_JOYDEVICEREMOVED:
            remove_joystick_device(event.jdevice);
            break;
        case SDL_KEYDOWN:
            keyboard_set_true(event.key.keysym.sym);
            break;
        case SDL_KEYUP:
            keyboard_set_false(event.key.keysym.sym);
            break;
        case SDL_MOUSEBUTTONDOWN:
            mouse_set_true(event.button.button);
            break;
        case SDL_MOUSEBUTTONUP:
            check_for_item_clicks(event.button);
            mouse_set_false(event.button.button);
            break;
        case SDL_MOUSEMOTION:
            update_mouse_position(event.motion);
            break;
        default:
            break;
    }
}

void engine::InputManager::set_mapping(Mapping mapping)
{
    mapper = std::make_unique<Mapping>(mapping);

    for (const auto& binding : mapping.get_keyboard_mapping())
    {
        map_pressed[(Buttons) binding.first] = false;
    }
}

InputCollection engine::InputManager::get_input()
{
    return map_pressed;
}

void engine::InputManager::load_keyboard_bindings()
{
    auto file = "keyboard.json";
    if (File::has_file(KEYBINDINGS_DIR, file))
    {
        mapper->set_keyboard_mapping_from_json(File::read_file(KEYBINDINGS_DIR, file));
    }
}

void engine::InputManager::update_mouse_position(SDL_MouseMotionEvent event)
{
    // TODO
}

void engine::InputManager::keyboard_set_true(SDL_Keycode keycode)
{
    KeyButtons key = get_keyboard_key(keycode);
    if (key != key_none)
    {
        map_pressed[(Buttons) key] = true;
    }
}

void engine::InputManager::keyboard_set_false(SDL_Keycode keycode)
{
    KeyButtons key = get_keyboard_key(keycode);
    if (key != key_none)
    {
        map_pressed[(Buttons) key] = false;
    }
}

void engine::InputManager::mouse_set_true(Uint8 mouse_button_code)
{
    MouseButtons key = get_mouse_key(mouse_button_code);
    if (key != mouse_none)
    {
        map_pressed[(Buttons) key] = true;
    }
}

void engine::InputManager::mouse_set_false(Uint8 mouse_button_code)
{
    MouseButtons key = get_mouse_key(mouse_button_code);
    if (key != mouse_none)
    {
        map_pressed[(Buttons) key] = false;
    }
}

void engine::InputManager::joy_controller_toggle(SDL_ControllerButtonEvent event)
{
    KeyButtons key = get_joy_key(event.button, CBUTTON);
    if (key != key_none)
    {
        map_pressed[(Buttons) key] = event.type == SDL_PRESSED;
    }
}

void engine::InputManager::joy_toggle(SDL_JoyButtonEvent event)
{
    KeyButtons key = get_joy_key(event.button, JBUTTON);
    if (key != key_none)
    {
        map_pressed[(Buttons) key] = event.state == SDL_PRESSED;
    }
}

void engine::InputManager::joy_hat_toggle(SDL_JoyHatEvent event)
{
    reset_joy_hats();

    KeyButtons key = get_joy_key(event.value, JOY_HAT);
    if (key != key_none)
    {
        map_pressed[(Buttons) key] = true;
    }
}

void engine::InputManager::reset_joy_hats()
{
    for (const auto& map : mapper->get_joystick_mapping())
    {
        if (map.second.first == JOY_HAT)
        {
            map_pressed[(Buttons) map.first] = false;
        }
    }
}

void engine::InputManager::check_for_item_clicks(SDL_MouseButtonEvent event)
{
    for (const auto& button : button_callbacks)
    {
        SDL_Rect rect = button->get_rect();

        if (event.x >= rect.x && event.x <= rect.x + rect.w && event.y >= rect.y && event.y <= rect.y + rect.h)
        {
            button_click_effect.play_once();
            button->click();
            break;
        }
    }
}

void engine::InputManager::register_callback(engine::Button* button)
{
    button_callbacks.push_back(button);
}

void engine::InputManager::remove_callback(engine::Button* button)
{
    button_callbacks.erase(
        std::remove(button_callbacks.begin(), button_callbacks.end(), button),
        button_callbacks.end()
    );
}

engine::MouseButtons engine::InputManager::get_mouse_key(Uint8 code)
{
    switch (code)
    {
        case SDL_BUTTON_LEFT:
            return MouseButtons::mouse_primary;
        case SDL_BUTTON_RIGHT:
            return MouseButtons::mouse_secondary;
        default:
            return MouseButtons::mouse_none;
    }
}

engine::KeyButtons engine::InputManager::get_keyboard_key(SDL_Keycode code)
{
    KeyButtons key = key_none;
    for (const auto& pair : mapper->get_keyboard_mapping())
    {
        if (pair.second == code)
        {
            key = pair.first;
            break;
        }
    }

    return key;
}

engine::KeyButtons engine::InputManager::get_joy_key(Uint8 code, const std::string& type)
{
    KeyButtons key = key_none;
    for (const auto& pair : mapper->get_joystick_mapping())
    {
        if (pair.second.first == type && pair.second.second == code)
        {
            key = pair.first;
            break;
        }
    }

    return key;
}

void engine::InputManager::add_joystick_device(SDL_JoyDeviceEvent event)
{
    if (joystick_id >= 0) return;

    joystick_id = SDL_JoystickInstanceID(SDL_JoystickOpen(event.which));

    // Load key bindings

    auto id = get_current_joystick_id();

    if (id.empty()) return;

    auto filename = id + ".json";
    if (File::has_file(KEYBINDINGS_DIR, filename))
    {
        mapper->set_joystick_mapping_from_json(File::read_file(KEYBINDINGS_DIR, filename));
    }
}

void engine::InputManager::remove_joystick_device(SDL_JoyDeviceEvent event)
{
    if (joystick_id >= 0 && joystick_id != event.which) return;

    auto joystick = SDL_JoystickFromInstanceID(joystick_id);
    if (joystick_id >= 0 && SDL_JoystickGetAttached(joystick))
    {
        SDL_JoystickClose(joystick);
    }

    joystick_id = -1;
}

void engine::InputManager::handle_joystick(SDL_Event& event)
{
    if (event.jaxis.axis == 0)
    {
        keyboard_set_false(SDLK_LEFT);
        keyboard_set_false(SDLK_RIGHT);

        if (event.jaxis.value <= -DEAD_ZONE)
        {
            keyboard_set_true(SDLK_LEFT);
        }
        else if (event.jaxis.value >= DEAD_ZONE)
        {
            keyboard_set_true(SDLK_RIGHT);
        }
    }
    else if (event.jaxis.axis == 1)
    {
        keyboard_set_false(SDLK_UP);
        keyboard_set_false(SDLK_DOWN);

        if (event.jaxis.value <= -DEAD_ZONE)
        {
            keyboard_set_true(SDLK_UP);
        }
        else if (event.jaxis.value >= DEAD_ZONE)
        {
            keyboard_set_true(SDLK_DOWN);
        }
    }
}

std::string engine::InputManager::get_current_joystick_id()
{
    if (joystick_id < 0) return "";

    auto guid = SDL_JoystickGetGUID(SDL_JoystickFromInstanceID(joystick_id));

    char buffer[16];

    SDL_JoystickGetGUIDString(guid, buffer, 16);

    return std::string(buffer);
}

void engine::InputManager::save_key_bindings()
{
    if (!File::has_sub_directory(KEYBINDINGS_DIR))
    {
        File::create_sub_directory(KEYBINDINGS_DIR);
    }

    auto id = get_current_joystick_id();
    if (!id.empty())
    {
        auto joystick_map = mapper->get_joystick_mapping_as_json();
        File::write_file(KEYBINDINGS_DIR, id + ".json", joystick_map);
    }

    auto keyboard_map = mapper->get_keyboard_mapping_as_json();
    File::write_file(KEYBINDINGS_DIR, "keyboard.json", keyboard_map);
}

void engine::InputManager::set_keyboard_key(Buttons button, SDL_Keycode code)
{
    mapper->set_keyboard_key((engine::KeyButtons) button, code);
}

void engine::InputManager::set_joystick_key(Buttons button, std::string& type, SDL_Keycode code)
{
    mapper->set_joystick_key((engine::KeyButtons) button, type, code);
}

