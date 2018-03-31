#ifndef LINAL_SPHERE_H
#define LINAL_SPHERE_H

#include "../Framework/Component.h"
#include <vector>
#include "Circle.h"
#include "Origin.h"

#define AMOUNT_OF_CIRCLES (SEGMENTS / 2) //max is 180

class Sphere : public Component
{
    std::vector<std::shared_ptr<Circle>> circles_;

    std::shared_ptr<Origin> origin_;

    float radius_;

public:
    Sphere(Point center, float radius);

    Point rotation_point() override;

    std::shared_ptr<Matrix> to_matrix(Dimensions dimension) override;

    void rotate(Axis axis, float degrees, Point rotation_point) override;

    void rotate(Axis axis, float degrees) override;

    void scale(float size, Point r) override;

    void scale(float size) override;

    void move(Vector& vector) override;

    void draw(Window& window, Vector& camera, Vector& target, bool show_origin) override;

    void set_color(SDL_Color color) override;

    std::vector<std::shared_ptr<Circle>> get_circles();

    float get_radius();
};

#endif //LINAL_SPHERE_H
