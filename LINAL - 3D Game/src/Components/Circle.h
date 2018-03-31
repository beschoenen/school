#ifndef LINAL_CIRCLE_H
#define LINAL_CIRCLE_H

#include "../Framework/Component.h"
#include <vector>
#include "Line.h"

#define SEGMENTS 20 // max is 360

class Circle : public Component
{
    float radius_;

    std::shared_ptr<Line> left_ = nullptr;
    std::shared_ptr<Line> right_ = nullptr;

    std::vector<std::shared_ptr<Line>> lines_;

public:
    Circle(Point center, float radius);

    explicit Circle(std::vector<std::shared_ptr<Line>> lines);

    Point rotation_point() override;

    std::shared_ptr<Matrix> to_matrix(Dimensions dimension = Two) override;

    void rotate(Axis axis, float degrees, Point r) override;

    void rotate(Axis axis, float degrees) override;

    void scale(float size, Point r) override;

    void scale(float size) override;

    void move(Vector& vector) override;

    void draw(Window& window, Vector& camera, Vector& target, bool show_origin) override;

    void set_color(SDL_Color color) override;

    std::vector<std::shared_ptr<Line>> get_lines();

    Point get_north();

    Point get_south();

    float get_radius();
};

#endif //LINAL_CIRCLE_H
