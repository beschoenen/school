#ifndef SDL_PROJECT_INPUTMANAGER_H
#define SDL_PROJECT_INPUTMANAGER_H

namespace engine
{
    class Button;
}

#include "../Input/Mapping.h"
#include <memory>
#include <vector>
#include "../Components/Button.h"
#include "../Models/Sound.h"

namespace engine
{
    class InputManager
    {
        SDL_JoystickID joystick_id = -1;
        std::map<Buttons, bool> map_pressed;
        std::vector<Button*> button_callbacks;

        MouseButtons get_mouse_key(Uint8 code);

        KeyButtons get_keyboard_key(SDL_Keycode code);

        KeyButtons get_joy_key(Uint8 code, const std::string& type);

    protected:
        Sound button_click_effect = engine::Sound(nullptr);
        std::unique_ptr<Mapping> mapper;

        InputManager();

        virtual ~InputManager();

        void handle_event(SDL_Event& event);

        void set_mapping(Mapping mapping);

        std::map<Buttons, bool> get_input();

        void update_mouse_position(SDL_MouseMotionEvent event);

        void load_keyboard_bindings();

        // Toggles

        void keyboard_set_true(SDL_Keycode keycode);

        void keyboard_set_false(SDL_Keycode keycode);

        void mouse_set_true(Uint8 mouse_button_code);

        void mouse_set_false(Uint8 mouse_button_code);

        void joy_toggle(SDL_JoyButtonEvent event);

        void joy_hat_toggle(SDL_JoyHatEvent event);

        void joy_controller_toggle(SDL_ControllerButtonEvent event);

        void reset_joy_hats();

        // Button callback

        void check_for_item_clicks(SDL_MouseButtonEvent event);

        void register_callback(Button* button);

        void remove_callback(Button* button);

        // Joysticks connectors / misc

        void add_joystick_device(SDL_JoyDeviceEvent event);

        void remove_joystick_device(SDL_JoyDeviceEvent event);

        void handle_joystick(SDL_Event& event);

        std::string get_current_joystick_id();

    public:
        void save_key_bindings();

        void set_keyboard_key(Buttons buttons, SDL_Keycode i);

        void set_joystick_key(Buttons button, std::string& type, SDL_Keycode code);
    };
}

#endif //SDL_PROJECT_INPUTMANAGER_H
