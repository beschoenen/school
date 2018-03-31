#ifndef LINAL_SQUARE_H
#define LINAL_SQUARE_H

#include <vector>
#include "../Framework/Component.h"
#include "../Models/Vector.h"
#include "Line.h"
#include "Origin.h"

class Square : public Component
{
    std::vector<std::shared_ptr<Line>> lines_;

public:
    Square(Point top_left, Point bottom_right);

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


#endif //LINAL_SQUARE_H
