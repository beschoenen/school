#ifndef LINAL_ORIGIN_H
#define LINAL_ORIGIN_H


#include <vector>
#include "../Framework/Component.h"
#include "Line.h"

class Origin : public Component
{
    std::vector<std::shared_ptr<Line>> lines_;

public:
    Origin(Point center, int size = 30);

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
};


#endif //LINAL_ORIGIN_H
