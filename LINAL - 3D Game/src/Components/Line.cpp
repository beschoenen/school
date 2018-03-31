#include "Line.h"
#include "../defines.h"
#include <utility>
#include <iostream>

Line::Line(Point start, Point end) : Line(start, start.vector_to(end))
{
}

Line::Line(Point start, Vector direction) : start_{start}, direction_{std::move(direction)}
{
}

Point Line::rotation_point()
{
    return
    {
        start_.x() + (direction_.x() / 2),
        start_.y() + (direction_.y() / 2),
        start_.z() + (direction_.z() / 2)
    };
}

std::shared_ptr<Matrix> Line::to_matrix(Dimensions dimension)
{
    auto matrix = std::make_shared<Matrix>(dimension + 1, 2);

    matrix->set(0, 0, start_.x());
    matrix->set(1, 0, start_.y());
    matrix->set(0, 1, start_.x() + direction_.x());
    matrix->set(1, 1, start_.y() + direction_.y());

    if (dimension == Two)
    {
        matrix->set(2, 0, 1);
        matrix->set(2, 1, 1);
    }
    else
    {
        matrix->set(2, 0, start_.z());
        matrix->set(2, 1, start_.z() + direction_.z());
        matrix->set(3, 0, 1);
        matrix->set(3, 1, 1);
    }

    return matrix;
}

void Line::rotate(Axis axis, float degrees, Point r)
{
    auto matrix = to_matrix(Three);

    auto m_to = to_origin_matrix(r, Three); // Matrix to translate to origin
    auto m_from = from_origin_matrix(r, Three); // Matrix to translate from origin

//    auto t1 = atan2(r.z(), r.x());
//    auto t2 = atan2(r.y(), sqrtf(powf(r.x(), 2.f) + powf(r.z(), 2.f)));
//
//    auto from_3d = Matrix::rotationY(t1, true); // Matrix to translate from 3d plane to 2d plane
//    auto to_3d = Matrix::rotationY(t1); // Matrix to translate from 2d plane to 3d plane
//
//    auto rotate_to_axis = Matrix::rotationX(degrees * M_PI / 180, true); // Rotate to 2d point from plane
//    auto rotate_from_axis = Matrix::rotationZ(t2); // Rotate from 2d point to plane
//
//    auto rotate = Matrix::rotationX(degrees * M_PI / 180); // Rotation matrix
//
//    auto result = from_3d->multiply(m_to);
//    auto result1 = rotate_to_axis->multiply(result);
//    auto result2 = rotate->multiply(result1);
//    auto result3 = rotate_from_axis->multiply(result2);
//    auto result4 = to_3d->multiply(result3);
//    auto result5 = m_from->multiply(result4);
//    auto result6 = result5->multiply(matrix);

    auto rotation = Matrix::identity(Three);

    switch (axis)
    {
        case Z:
            rotation = Matrix::rotationZ(degrees * M_PI / 180, true);
            break;
        case X:
            rotation = Matrix::rotationX(degrees * M_PI / 180, true);
            break;
        case Y:
            rotation = Matrix::rotationY(degrees * M_PI / 180, true);
            break;
    }

    auto result1 = m_to->multiply(matrix);
    auto result2 = rotation->multiply(result1);
    auto result6 = m_from->multiply(result2);

    start_.x(result6->get(0, 0));
    start_.y(result6->get(1, 0));
    start_.z(result6->get(2, 0));
    direction_.x(result6->get(0, 1) - start_.x());
    direction_.y(result6->get(1, 1) - start_.y());
    direction_.z(result6->get(2, 1) - start_.z());
}

void Line::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Line::scale(float size, Point r)
{
    scale_ *= size;

    auto matrix = to_matrix(Three);
    auto m_to = to_origin_matrix(r, Three);
    auto m_from = from_origin_matrix(r, Three);

    auto scale = Matrix::scale(Three, size);

    auto result1 = m_to->multiply(matrix);
    auto result2 = scale->multiply(result1);
    auto result6 = m_from->multiply(result2);

    start_.x(result6->get(0, 0));
    start_.y(result6->get(1, 0));
    start_.z(result6->get(2, 0));
    direction_.x(result6->get(0, 1) - start_.x());
    direction_.y(result6->get(1, 1) - start_.y());
    direction_.z(result6->get(2, 1) - start_.z());
}

void Line::scale(float size)
{
    scale(size, rotation_point());
}

void Line::move(Vector& vector)
{
    auto matrix = to_matrix(Three);

    float values[3] = {vector.x(), vector.y(), vector.z()};
    auto translate = Matrix::translation(Three, values);

    auto result = translate->multiply(matrix);

    start_.x(result->get(0, 0));
    start_.y(result->get(1, 0));
    start_.z(result->get(2, 0));
    direction_.x(result->get(0, 1) - start_.x());
    direction_.y(result->get(1, 1) - start_.y());
    direction_.z(result->get(2, 1) - start_.z());
}

void Line::set_color(SDL_Color color)
{
    color_ = color;
}

void Line::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    auto height = 0;

    SDL_GetWindowSize(window.screen, nullptr, &height);

    auto start = calculate_3d_location(start_, camera, target);
    auto end = calculate_3d_location(direction_.point_from(start_), camera, target);

    if (start.z() < 0 || end.z() < 0) return;

    auto color = get_color();

    SDL_SetRenderDrawColor(window.renderer, color.r, color.g, color.b, color.a);
    SDL_RenderDrawLine(window.renderer, start.x(), height - start.y(), end.x(), height - end.y());
}

Point Line::get_start()
{
    return start_;
}

Vector Line::get_direction()
{
    return direction_;
}
