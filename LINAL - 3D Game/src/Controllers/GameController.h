#ifndef LINAL_TESTCONTROLLER_H
#define LINAL_TESTCONTROLLER_H


#include <map>
#include "../Framework/Controller.h"
#include "../Components/Square.h"
#include "../Components/Cube.h"
#include "../Components/Triangle.h"
#include "../Components/Spaceship.h"
#include "../Components/Sphere.h"
#include "../Models/Camera.h"

class GameController : public Controller
{
    std::map<std::shared_ptr<Sphere>, bool> pulsing_planets;
    std::vector<std::shared_ptr<Component>> components_;

    std::shared_ptr<Spaceship> spaceship_;
    std::shared_ptr<Origin> origin_;

    Camera camera_;

    bool show_origin_ = false;
    Uint32 mouse_down_ = 0;

public:
    GameController();

    void generate_planets();

    void handle_input(SDL_Event& event) override;

    void update(float delta_time, std::map<SDL_Keycode, bool>& keys) override;

    void render(Window& window) override;
};


#endif //LINAL_TESTCONTROLLER_H
