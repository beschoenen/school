#ifndef LINAL_CUBE_H
#define LINAL_CUBE_H


#include <vector>
#include "../Framework/Component.h"
#include "Square.h"

class Cube : public Component
{
    std::vector<std::shared_ptr<Square>> squares_;

    std::shared_ptr<Origin> origin_;

public:
    Cube(Point center, float size);

    Cube(Point front_top_left, Point back_bottom_right);

    Point rotation_point() override;

    std::shared_ptr<Matrix> to_matrix(Dimensions dimension = Two) override;

    void rotate(Axis axis, float degrees, Point r) override;

    void rotate(Axis axis, float degrees) override;

    void scale(float size, Point r) override;

    void scale(float size) override;

    void move(Vector& vector) override;

    void draw(Window& window, Vector& camera, Vector& target, bool show_origin) override;

    void set_color(SDL_Color color) override;

    std::vector<std::shared_ptr<Square>> get_squares();
};


#endif //LINAL_CUBE_H
