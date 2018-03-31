#ifndef LINAL_SPACESHIP_H
#define LINAL_SPACESHIP_H

class Bullet;

#include <vector>
#include "../Framework/Component.h"
#include "../defines.h"
#include "Triangle.h"
#include "Origin.h"

#define MAX_SPEED 20
#define MASS 5
#define ROTATION_SPEED 2

class Spaceship : public Component
{
    std::vector<std::shared_ptr<Component>> components_;

    std::shared_ptr<Triangle> back_;
    std::shared_ptr<Triangle> bottom_;

    std::shared_ptr<Origin> origin_;

    std::vector<std::shared_ptr<Bullet>> bullets_;

    Vector velocity_ = {0, 0, 0};
    int heading_ = 0;

    int rotation_x_ = 0;
    int rotation_y_ = 0;
    int rotation_z_ = 0;

public:
    explicit Spaceship(Point front);

    Point rotation_point() override;

    std::shared_ptr<Matrix> to_matrix(Dimensions dimension = Two) override;

    void rotate(Axis axis, float degrees, Point r) override;

    void rotate(Axis axis, float degrees) override;

    void scale(float size, Point r) override;

    void scale(float size) override;

    void move(Vector& vector) override;

    // Spaceship methods

    void dive();

    void rise();

    void dive_left();

    void dive_right();

    void rotate_left();

    void rotate_right();

    void accelerate(float delta_time);

    void reverse(float delta_time);

    void free(float delta_time);

    void shoot();

    void remove_bullet(Bullet& bullet);

    void update(float delta_time);

    Vector direction();

    void draw(Window& window, Vector& camera, Vector& target, bool show_origin) override;

    void set_color(SDL_Color color) override;

    std::vector<std::shared_ptr<Bullet>> get_bullets();
};


#endif //LINAL_SPACESHIP_H
