#include <iostream>
#include "Cube.h"

Cube::Cube(Point center, float size) :
    Cube({center.x() - (size / 2), center.y() + (size / 2), center.z() + (size / 2)},
         {center.x() + (size / 2), center.y() - (size / 2), center.z() - (size / 2)})
{
}

Cube::Cube(Point front_top_left, Point back_bottom_right)
{
    Point front_bottom_left =
    {
        front_top_left.x(),
        back_bottom_right.y(),
        front_top_left.z()
    };

    Point front_top_right =
    {
        back_bottom_right.x(),
        front_top_left.y(),
        front_top_left.z()
    };

    Point front_bottom_right =
    {
        back_bottom_right.x(),
        back_bottom_right.y(),
        front_top_left.z()
    };

    Point back_top_left =
    {
        front_top_left.x(),
        front_top_left.y(),
        back_bottom_right.z()
    };

    auto front = std::make_shared<Square>(front_top_left, front_bottom_right);
    auto left = std::make_shared<Square>(back_top_left, front_bottom_left);
    auto back = std::make_shared<Square>(back_top_left, back_bottom_right);
    auto right = std::make_shared<Square>(front_top_right, back_bottom_right);

    squares_ = {front, left, back, right};

    origin_ = std::make_shared<Origin>(Point{rotation_point()});
}

Point Cube::rotation_point()
{
    auto front = squares_[0]->rotation_point();
    auto back = squares_[2]->rotation_point();

    return front.center_to(back);
}

std::shared_ptr<Matrix> Cube::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Cube::rotate(Axis axis, float degrees, Point r)
{
    for (auto& square : squares_)
    {
        square->rotate(axis, degrees, r);
    }

    origin_->rotate(axis, degrees, r);
}

void Cube::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Cube::scale(float size, Point r)
{
    scale_ *= size;

    for (const auto& square : squares_)
    {
        square->scale(size, r);
    }
}

void Cube::scale(float size)
{
    scale(size, rotation_point());
}

void Cube::move(Vector& vector)
{
    for (const auto& square : squares_)
    {
        square->move(vector);
    }

    origin_->move(vector);
}

void Cube::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (auto& square : squares_)
    {
        square->draw(window, camera, target, false);
    }

    if (show_origin)
    {
        origin_->draw(window, camera, target, false);
    }
}

void Cube::set_color(SDL_Color color)
{
    color_ = color;
    for (const auto& square : squares_)
    {
        square->set_color(color);
    }
}

std::vector<std::shared_ptr<Square>> Cube::get_squares()
{
    return squares_;
}
