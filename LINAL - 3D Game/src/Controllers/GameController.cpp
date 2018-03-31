#include <iostream>
#include <algorithm>
#include "GameController.h"
#include "../Components/Bullet.h"
#include "../Framework/Random.h"
#include "../Components/Origin.h"

GameController::GameController() : spaceship_{std::make_shared<Spaceship>(Point{45, 0, 45})},
                                   origin_{std::make_shared<Origin>(Point{0, 0, 0}, 50)},
                                   camera_{spaceship_->rotation_point()}
{
    camera_.follow(true);

    generate_planets();

    components_.push_back(std::make_shared<Cube>(Point{300, 100, 300}, Point{5, 0, 5}));
    components_.push_back(spaceship_);
}

void GameController::generate_planets()
{
    for (auto i = 0; i < 5; i++)
    {
        auto x = Random::instance().get(20, 260);
        auto y = Random::instance().get(20, 80);
        auto z = Random::instance().get(20, 260);
        float rad = Random::instance().get(5, 20);

        Uint8 r = Random::instance().get(255);
        Uint8 g = Random::instance().get(255);
        Uint8 b = Random::instance().get(255);

        auto planet = std::make_shared<Sphere>(Point{x, y, z}, rad);
        planet->set_color({r, g, b});

        if (i == 0)
        {
            pulsing_planets.emplace(planet, false);
        }

        components_.push_back(planet);
    }
}

void GameController::handle_input(SDL_Event& event)
{
    if (event.type == SDL_KEYUP)
    {
        if (event.key.keysym.sym == SDLK_SPACE)
        {
            spaceship_->shoot();
        }

        if (event.key.keysym.sym == SDLK_m)
        {
            show_origin_ = !show_origin_;
        }
    }

    if (event.wheel.y == 1) // scroll up
    {
        camera_.zoom_in(20);
    }
    else if (event.wheel.y == -1) // scroll down
    {
        camera_.zoom_out(20);
    }

    if (event.type == SDL_MOUSEBUTTONDOWN)
    {
        mouse_down_ = event.button.button;
    }
    else if (event.type == SDL_MOUSEBUTTONUP)
    {
        mouse_down_ = 0;
    }

    if (event.type == SDL_MOUSEMOTION)
    {
        if (mouse_down_ == SDL_BUTTON_RIGHT)
        {
            camera_.look(event.motion.xrel, event.motion.yrel);
        }
        else if (mouse_down_ == SDL_BUTTON_LEFT)
        {
            camera_.move(event.motion.xrel, event.motion.yrel);
        }
    }
}

void GameController::update(float delta_time, std::map<SDL_Keycode, bool>& keys)
{
    //////////////////
    /// Move spaceship

    if (keys[SDLK_w])
    {
        spaceship_->dive();
    }

    if (keys[SDLK_s])
    {
        spaceship_->rise();
    }

    if (keys[SDLK_a])
    {
        spaceship_->dive_left();
    }

    if (keys[SDLK_d])
    {
        spaceship_->dive_right();
    }

    if (keys[SDLK_q])
    {
        spaceship_->rotate_left();
    }

    if (keys[SDLK_e])
    {
        spaceship_->rotate_right();
    }

    if (keys[SDLK_LSHIFT])
    {
        spaceship_->accelerate(delta_time);
    }
    else if (keys[SDLK_RSHIFT])
    {
        spaceship_->reverse(delta_time);
    }
    else
    {
        spaceship_->free(delta_time);
    }

    ///////////////
    /// Move camera

    if (keys[SDLK_i])
    {
        camera_.move_forward();
    }

    if (keys[SDLK_k])
    {
        camera_.move_backward();
    }

    if (keys[SDLK_j])
    {
        camera_.move_left();
    }

    if (keys[SDLK_l])
    {
        camera_.move_right();
    }

    if (keys[SDLK_u])
    {
        camera_.move_up();
    }

    if (keys[SDLK_o])
    {
        camera_.move_down();
    }

    if (keys[SDLK_p])
    {
        camera_.follow(true);
    }

    if (keys[SDLK_UP])
    {
        camera_.look_up();
    }

    if (keys[SDLK_DOWN])
    {
        camera_.look_down();
    }

    if (keys[SDLK_LEFT])
    {
        camera_.look_left();
    }

    if (keys[SDLK_RIGHT])
    {
        camera_.look_right();
    }

    if (keys[SDLK_EQUALS] || keys[SDLK_PLUS])
    {
        camera_.zoom_in();
    }

    if (keys[SDLK_UNDERSCORE] || keys[SDLK_MINUS])
    {
        camera_.zoom_out();
    }

    ///////////////
    /// Move camera
    auto old_rp = spaceship_->rotation_point();

    spaceship_->update(delta_time);

    auto diff = old_rp.vector_to(spaceship_->rotation_point());

    if (camera_.follow())
    {
        camera_.move(diff);
    }

    /////////////////
    /// Hit detection
    auto it = pulsing_planets.begin();
    while (it != pulsing_planets.end())
    {
        auto hit = false;

        if (it->first != nullptr)
        {
            for (const auto& bullet : spaceship_->get_bullets())
            {
                if (bullet == nullptr) continue;

                auto point = bullet->get_start();

                if (point.distance(it->first->rotation_point()) <= it->first->get_radius())
                {
                    components_.erase(std::remove(components_.begin(), components_.end(), it->first));
                    pulsing_planets.erase(it, pulsing_planets.end());
                    spaceship_->remove_bullet(*bullet);

                    hit = true;
                    break;
                }
            }
        }

        if (hit) break;

        ++it;
    }

    /////////////////
    /// Pulse objects
    for (auto& item : pulsing_planets)
    {
        item.first->scale(item.second ? 1.02f : 0.98f);

        auto scale = item.first->get_scale();
        if (scale <= 0.5)
        {
            item.second = true;
        }
        else if (scale >= 2)
        {
            item.second = false;
        }
    }
}

void GameController::render(Window& window)
{
    Vector target = camera_.get_target();
    Vector camera = camera_.get_location();

    for (const auto& component : components_)
    {
        component->draw(window, camera, target, show_origin_);
    }

    if (show_origin_)
    {
        origin_->draw(window, camera, target, show_origin_);
    }
}
