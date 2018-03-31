#ifndef LINAL_LINE_H
#define LINAL_LINE_H

#include "../Models/Vector.h"
#include "../Models/Point.h"
#include "../Framework/Component.h"

class Line : public Component
{
    Point start_;
    Vector direction_;

public:
    Line(Point start, Point end);

    Line(Point start, Vector direction);

    Point rotation_point() override;

    std::shared_ptr<Matrix> to_matrix(Dimensions dimension = Two) override;

    void rotate(Axis axis, float degrees, Point r) override;

    void rotate(Axis axis, float degrees) override;

    void scale(float size, Point r) override;

    void scale(float size) override;

    void move(Vector& vector) override;

    void set_color(SDL_Color color) override;

    void draw(Window& window, Vector& camera, Vector& target, bool show_origin) override;

    Point get_start();

    Vector get_direction();
};

#endif
