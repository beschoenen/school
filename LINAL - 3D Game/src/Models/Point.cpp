#include <iostream>
#include "Point.h"

Point::Point() : Point(0, 0, 0)
{
}

Point::Point(Vector vector) : Point(vector.x(), vector.y(), vector.z())
{
}

Point::Point(const int x, const int y, const int z) : x_{float(x)}, y_{float(y)}, z_{float(z)}
{
}

Point::Point(const float x, const float y, const float z) : x_{x}, y_{y}, z_{z}
{
}

float Point::x() const
{
    return x_;
}

void Point::x(const float value)
{
    x_ = value;
}

float Point::y() const
{
    return y_;
}

void Point::y(const float value)
{
    y_ = value;
}

float Point::z() const
{
    return z_;
}

void Point::z(const float value)
{
    z_ = value;
}

Vector Point::vector_to(Point right)
{
    return {right.x() - x_, right.y() - y_, right.z() - z_};
}

std::shared_ptr<Matrix> Point::to_4d_matrix(int w)
{
    auto matrix = std::make_shared<Matrix>(4, 1);

    matrix->set(0, 0, x_);
    matrix->set(1, 0, y_);
    matrix->set(2, 0, z_);
    matrix->set(3, 0, w);

    return matrix;
}

Point Point::center_to(Point point)
{
    return
    {
        (x_ + point.x()) / 2,
        (y_ + point.y()) / 2,
        (z_ + point.z()) / 2
    };
}

Point Point::barycentric(Point left, Point right)
{
    const auto c_x = (left.x() + right.x() + x()) / 3;
    const auto c_y = (left.y() + right.y() + y()) / 3;

    return {c_x, c_y, z()};
}

float Point::distance(Point to)
{
    return sqrtf((to - *this).dot());
}

float Point::dot()
{
    return x_ * x_ + y_ * y_ + z_ * z_;
}

bool Point::is_zero()
{
    return x() == 0.f && y() == 0.f && z() == 0.f;
}

std::ostream& operator<<(std::ostream& ostream, Point& point)
{
    ostream << point.x() << ", " << point.y() << ", " << point.z();

    return ostream;
}

std::ostream& operator<<(std::ostream& ostream, Point point)
{
    ostream << point.x() << ", " << point.y() << ", " << point.z();

    return ostream;
}

Point Point::operator-(Point right)
{
    return
    {
        right.x() - x_,
        right.y() - y_,
        right.z() - z_
    };
}

void Point::operator+=(Point right)
{
    x_ += right.x();
    y_ += right.y();
    z_ += right.z();
}
