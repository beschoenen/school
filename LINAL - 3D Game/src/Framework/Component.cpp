#include "Component.h"

std::shared_ptr<Matrix> Component::to_origin_matrix(Point r_point, Dimensions dimension)
{
    float values[3] = {-r_point.x(), -r_point.y(), -r_point.z()};

    return Matrix::translation(dimension, values);
}

std::shared_ptr<Matrix> Component::from_origin_matrix(Point r_point, Dimensions dimension)
{
    float values[3] = {r_point.x(), r_point.y(), r_point.z()};

    return Matrix::translation(dimension, values);
}

Point Component::calculate_3d_location(Point location, Vector& camera, Vector& target)
{
    auto matrix = location.to_4d_matrix();
    auto look_at = Matrix::look_at(camera, target);
    auto perspective = Matrix::perspective(5, 200, 90);

    auto result1 = look_at->multiply(*matrix);
    auto result2 = perspective->multiply(result1);

    auto screen_d2 = SCREEN_WIDTH / 2;
    auto screen_t2 = SCREEN_WIDTH * 0.5;

    auto main_x = (result2->get(0, 0) + 1) / result2->get(3, 0);
    auto main_y = (result2->get(1, 0) + 1) / result2->get(3, 0);

    auto x = float(screen_d2 + main_x * screen_t2);
    auto y = float(screen_d2 + main_y * screen_t2);
    auto w = result2->get(3, 0);

    return {x, y, w};
}

SDL_Color Component::get_color()
{
    return color_;
}

float Component::get_scale()
{
    return scale_;
}
