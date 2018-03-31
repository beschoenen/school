#include "KeyMapper.h"
#include <utility>
#include "../Engine/Engine.h"

#define MAPPING_TEXT_IDLE "Click a button below to start mapping"

scenes::KeyMapper::KeyMapper() : KeyMapper(nullptr)
{
}

scenes::KeyMapper::KeyMapper(std::shared_ptr<engine::Scene> old_scene) : Scene(std::move(old_scene))
{
    wallpaper = std::shared_ptr<MenuWallpaper>(new MenuWallpaper());
    title = std::shared_ptr<TitleText>(new TitleText("Key Mapper"));

    mapping_text = std::shared_ptr<MenuText>(new MenuText(MAPPING_TEXT_IDLE, 50, 150));

    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Up", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::up), engine::up, 50, 220)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Down", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::down), engine::down, 50,
                            270)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Left", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::left), engine::left, 50,
                            320)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Right", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::right), engine::right,
                            50, 370)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Action", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::action), engine::action,
                            50, 420)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Menu", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::menu), engine::menu, 50,
                            470)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Cancel", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::cancel), engine::cancel,
                            50, 520)));
    bind_buttons.push_back(std::shared_ptr<KeyMapperButton>(
        new KeyMapperButton("Confirm", std::bind(&scenes::KeyMapper::set_bind_key, this, engine::confirm),
                            engine::confirm, 50, 570)));

    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 220)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 270)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 320)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 370)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 420)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 470)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 520)));
    pressed_text.push_back(std::shared_ptr<KeyMapperText>(new KeyMapperText(250, 570)));

    back_button = std::shared_ptr<MenuButton>(
        new MenuButton("Back", std::bind(&scenes::KeyMapper::back_button_clicked, this), 10, 0, POS_BOTTOM_LEFT));
}

void scenes::KeyMapper::enter_scene()
{
    Engine::instance()->draw(wallpaper);
    Engine::instance()->draw(title);
    Engine::instance()->draw(mapping_text);
    Engine::instance()->draw(back_button);

    for (const auto& button : bind_buttons)
    {
        Engine::instance()->draw(button);
    }

    for (const auto& text : pressed_text)
    {
        Engine::instance()->draw(text);
    }
}

void scenes::KeyMapper::leave_scene()
{
    Engine::instance()->erase(wallpaper);
    Engine::instance()->erase(title);
    Engine::instance()->erase(mapping_text);
    Engine::instance()->erase(back_button);

    for (const auto& button : bind_buttons)
    {
        Engine::instance()->erase(button);
    }

    for (const auto& text : pressed_text)
    {
        Engine::instance()->erase(text);
    }
}

void scenes::KeyMapper::update()
{
    //
}

void scenes::KeyMapper::input(InputCollection input)
{
    if (binding_button != engine::none)
    {
        set_button_on_mapper();
    }

    pressed_text[0]->set_pressed(input[engine::Buttons::up]);
    pressed_text[1]->set_pressed(input[engine::Buttons::down]);
    pressed_text[2]->set_pressed(input[engine::Buttons::left]);
    pressed_text[3]->set_pressed(input[engine::Buttons::right]);
    pressed_text[4]->set_pressed(input[engine::Buttons::action]);
    pressed_text[5]->set_pressed(input[engine::Buttons::menu]);
    pressed_text[6]->set_pressed(input[engine::Buttons::cancel]);
    pressed_text[7]->set_pressed(input[engine::Buttons::confirm]);
}

void scenes::KeyMapper::set_button_on_mapper()
{
    auto event = Engine::instance()->get_last_event();

    std::string type;
    SDL_Keycode code;

    switch (event.type)
    {
        case SDL_JOYHATMOTION:
            type = JOY_HAT;
            code = event.jhat.value;
            break;
        case SDL_JOYBUTTONUP:
            type = JBUTTON;
            code = event.jbutton.button;
            break;
        case SDL_CONTROLLERBUTTONUP:
            type = CBUTTON;
            code = event.jbutton.button;
            break;
        case SDL_KEYUP:
            type = KEYBOARD;
            code = event.key.keysym.sym;
            break;
        default:
            return;
    }

    if (type == JOY_HAT && event.jhat.value == 0) return;

    if (type == KEYBOARD)
    {
        Engine::instance()->set_keyboard_key(binding_button, code);
    }
    else
    {
        Engine::instance()->set_joystick_key(binding_button, type, code);
    }

    binding_button = engine::none;
    mapping_text->set_text(MAPPING_TEXT_IDLE);
}

void scenes::KeyMapper::back_button_clicked()
{
    Engine::instance()->save_key_bindings();

    Engine::instance()->exit_current_scene(old_scene);
}

void scenes::KeyMapper::set_bind_key(engine::Buttons button)
{
    binding_button = button;
    std::string type(engine::EnumKeys[button]);

    mapping_text->set_text("Press a button to map against " + type);
}
