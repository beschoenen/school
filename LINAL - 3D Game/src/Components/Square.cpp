#include <iostream>
#include "Square.h"

Square::Square(Point top_left, Point bottom_right)
{
    bool flat = bottom_right.y() == top_left.y();

    auto width = flat ? 0 : bottom_right.x() - top_left.x();
    auto height = flat ? bottom_right.y() - top_left.y() : 0;
    auto depth = bottom_right.z() - top_left.z();

    Point top_right =
    {
        top_left.x() + width,
        top_left.y() + height,
        top_left.z() + depth
    };

    Point bottom_left =
    {
        bottom_right.x() - width,
        bottom_right.y() - height,
        bottom_right.z() - depth
    };

    auto top = std::make_shared<Line>(top_left, top_right);
    auto left = std::make_shared<Line>(bottom_left, top_left);
    auto bottom = std::make_shared<Line>(bottom_right, bottom_left);
    auto right = std::make_shared<Line>(top_right, bottom_right);

    lines_ = {top, left, bottom, right};
}

Point Square::rotation_point()
{
    auto top_left = lines_[0]->get_start();
    auto bottom_right = lines_[2]->get_start();

    return top_left.center_to(bottom_right);
}

std::shared_ptr<Matrix> Square::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Square::rotate(Axis axis, float degrees, Point r)
{
    for (auto& line : lines_)
    {
        line->rotate(axis, degrees, r);
    }
}

void Square::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Square::scale(float size, Point r)
{
    scale_ *= size;

    for (const auto& line : lines_)
    {
        line->scale(size, r);
    }
}

void Square::scale(float size)
{
    scale(size, rotation_point());
}

void Square::move(Vector& vector)
{
    for (const auto& line : lines_)
    {
        line->move(vector);
    }
}

void Square::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (auto& line : lines_)
    {
        line->draw(window, camera, target, false);
    }
}

void Square::set_color(SDL_Color color)
{
    color_ = color;
    for (const auto& line : lines_)
    {
        line->set_color(color);
    }
}

std::vector<std::shared_ptr<Line>> Square::get_lines()
{
    return lines_;
}
