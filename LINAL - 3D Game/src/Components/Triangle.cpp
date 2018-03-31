#include "Triangle.h"

Triangle::Triangle(Point top, Point left, Point right)
{
    auto left_side = std::make_shared<Line>(top, left);
    auto right_side = std::make_shared<Line>(top, right);
    auto bottom = std::make_shared<Line>(left, right);

    lines_ = {left_side, right_side, bottom};
}

Point Triangle::rotation_point()
{
    auto top = lines_[0]->get_start();
    auto left = lines_[0]->get_direction().point_from(top);
    auto right = lines_[1]->get_direction().point_from(top);

    return top.barycentric(left, right);
}

std::shared_ptr<Matrix> Triangle::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Triangle::rotate(Axis axis, float degrees, Point r)
{
    for (const auto& line : lines_)
    {
        line->rotate(axis, degrees, r);
    }
}

void Triangle::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Triangle::scale(float size, Point r)
{
    scale_ *= size;

    for (const auto& line : lines_)
    {
        line->scale(size, r);
    }
}

void Triangle::move(Vector& vector)
{
    for (const auto& line : lines_)
    {
        line->move(vector);
    }
}

void Triangle::scale(float size)
{
    scale(size, rotation_point());
}

void Triangle::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (const auto& line : lines_)
    {
        line->draw(window, camera, target, false);
    }
}

void Triangle::set_color(SDL_Color color)
{
    color_ = color;
    for (const auto& line : lines_)
    {
        line->set_color(color);
    }
}

std::vector<std::shared_ptr<Line>> Triangle::get_lines()
{
    return lines_;
}
