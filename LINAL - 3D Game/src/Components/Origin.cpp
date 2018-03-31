#include "Origin.h"

Origin::Origin(Point center, int size)
{
    auto line_x = std::make_shared<Line>(center, Vector{size, 0});
    line_x->set_color({255, 0, 0, 0});

    auto line_y = std::make_shared<Line>(center, Vector{0, size});
    line_y->set_color({0, 255, 0, 0});

    auto line_z = std::make_shared<Line>(center, Vector{0, 0, size});
    line_z->set_color({0, 0, 255, 0});

    lines_ = {line_x, line_y, line_z};
}

Point Origin::rotation_point()
{
    return lines_[0]->get_start();
}

std::shared_ptr<Matrix> Origin::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Origin::rotate(Axis axis, float degrees, Point r)
{
    for (auto& line : lines_)
    {
        line->rotate(axis, degrees, r);
    }
}

void Origin::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Origin::scale(float size, Point r)
{
    scale_ *= size;

    for (auto& line : lines_)
    {
        line->scale(size, r);
    }
}

void Origin::scale(float size)
{
    scale(size, rotation_point());
}

void Origin::move(Vector& vector)
{
    for (auto& line : lines_)
    {
        line->move(vector);
    }
}

void Origin::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (auto& line : lines_)
    {
        line->draw(window, camera, target, false);
    }
}

void Origin::set_color(SDL_Color color)
{
    //
}

std::vector<std::shared_ptr<Line>> Origin::get_lines()
{
    return lines_;
}
