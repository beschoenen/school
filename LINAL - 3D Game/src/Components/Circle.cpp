#include "Circle.h"
#include <utility>
#include <vector>
#include <iostream>

Circle::Circle(Point center, float radius) : radius_{radius}
{
    float step = (2 * M_PI) / SEGMENTS;

    Point very_first_point;
    Point first_point = {0, 0, 0};

    // - 0.00001 for rounding error.
    for (float theta = 0; theta < (2 * M_PI) - 0.00001; theta += step)
    {
        float x = center.x() + radius * cos(theta);
        float y = center.y() - radius * sin(theta);

        if (first_point.is_zero())
        {
            very_first_point = {x, y, center.z()};
            first_point = very_first_point;
            continue;
        }

        Point second_point = {x, y, center.z()};
        auto line = std::make_shared<Line>(first_point, second_point);
        lines_.emplace_back(line);
        first_point = second_point;

        if (left_ == nullptr)
        {
            left_ = line;
        }

        if (theta >= M_PI && right_ == nullptr)
        {
            right_ = line;
        }
    }

    lines_.emplace_back(std::make_shared<Line>(first_point, very_first_point));
}

Circle::Circle(std::vector<std::shared_ptr<Line>> lines) : lines_{std::move(lines)}
{
    radius_ = lines_[0]->get_start().distance(lines_[(lines_.size() / 2) - 1]->get_start()) / 2;
}

Point Circle::rotation_point()
{
    return get_north().center_to(get_south());
}

std::shared_ptr<Matrix> Circle::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Circle::rotate(Axis axis, float degrees, Point r)
{
    for (auto& line : lines_)
    {
        line->rotate(axis, degrees, r);
    }
}

void Circle::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Circle::scale(float size, Point r)
{
    scale_ *= size;

    for (auto& line : lines_)
    {
        line->scale(size, r);
    }
}

void Circle::scale(float size)
{
    scale(size, rotation_point());
}

void Circle::move(Vector& vector)
{
    for (auto& line : lines_)
    {
        line->move(vector);
    }
}

void Circle::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (auto& line : lines_)
    {
        line->draw(window, camera, target, false);
    }
}

void Circle::set_color(SDL_Color color)
{
    color_ = color;
    for (auto& line : lines_)
    {
        line->set_color(color);
    }
}

std::vector<std::shared_ptr<Line>> Circle::get_lines()
{
    return lines_;
}

Point Circle::get_north()
{
    return left_->get_start();
}

Point Circle::get_south()
{
    auto right = right_->get_start();

    return right_->get_direction().point_from(right);
}

float Circle::get_radius()
{
    return radius_;
}
