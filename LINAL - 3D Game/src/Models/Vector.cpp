#include <iostream>
#include "Vector.h"

Vector::Vector() : Vector(0, 0, 0)
{
}

Vector::Vector(const Point point) : Vector(point.x(), point.y(), point.z())
{
}

Vector::Vector(int x, int y, int z) : x_{float(x)}, y_{float(y)}, z_{float(z)}
{
}

Vector::Vector(const float x, const float y, const float z) : x_{x}, y_{y}, z_{z}
{
}

Vector::Vector(const Vector& copy) noexcept
{
    x_ = copy.x();
    y_ = copy.y();
    z_ = copy.z();
}

Vector::Vector(Vector&& move) noexcept
{
    x_ = move.x();
    y_ = move.y();
    z_ = move.z();

    move.x(0);
    move.y(0);
    move.z(0);
}

float Vector::x() const
{
    return x_;
}

void Vector::x(const float value)
{
    x_ = value;
}

float Vector::y() const
{
    return y_;
}

void Vector::y(const float value)
{
    y_ = value;
}

float Vector::z() const
{
    return z_;
}

void Vector::z(const float value)
{
    z_ = value;
}

Vector Vector::subtract(Vector& right)
{
    x_ -= right.x();
    y_ -= right.y();
    z_ -= right.z();

    return *this;
}

Vector Vector::subtracted(Vector& right) const
{
    return Vector(*this).subtract(right);
}

Vector Vector::add(Vector& right)
{
    x_ += right.x();
    y_ += right.y();
    z_ += right.z();

    return *this;
}

Vector Vector::added(Vector& right) const
{
    return Vector(*this).add(right);
}

Vector Vector::divide(const float scalar)
{
    x_ /= scalar;
    y_ /= scalar;
    z_ /= scalar;

    return *this;
}

Vector Vector::divided(const float scalar) const
{
    return Vector(*this).divide(scalar);
}

Vector Vector::multiply(const float scalar)
{
    x_ *= scalar;
    y_ *= scalar;
    z_ *= scalar;

    return *this;
}

Vector Vector::multiplied(const float scalar) const
{
    return Vector(*this).multiply(scalar);
}

float Vector::dot()
{
    return dot(*this);
}

float Vector::dot(Vector& right) const
{
    return x_ * right.x() + y_ * right.y() + z_ * right.z();
}

Vector Vector::cross(Vector& right)
{
    const auto temp_x = y_ * right.z() - z_ * right.y();
    const auto temp_y = z_ * right.x() - x_ * right.z();
    const auto temp_z = x_ * right.y() - y_ * right.x();

    x(temp_x);
    y(temp_y);
    z(temp_z);
    
    return *this;
}

Vector Vector::crossed(Vector& right) const
{
    return Vector(*this).cross(right);
}

Vector Vector::inverse()
{
    return this->multiply(-1);
}

Vector Vector::inversed() const
{
    return Vector(*this).inverse();
}

Vector Vector::normalize()
{
    const auto len = length();

    if (len < std::numeric_limits<float>::epsilon()) return *this;

    return divide(len);
}

Vector Vector::normalized() const
{
    return Vector(*this).normalize();
}

Vector Vector::reverse()
{
    x_ *= -1;
    y_ *= -1;
    z_ *= -1;

    return *this;
}

Vector Vector::reversed() const
{
    return Vector(*this).reverse();
}

float Vector::distance(Vector& to)
{
    return sqrtf(distance_squared(to));
}

float Vector::distance_squared(Vector& to)
{
    return to.subtracted(*this).dot();
}

float Vector::length()
{
    return sqrtf(length_squared());
}

float Vector::length_squared()
{
    return dot();
}

Vector Vector::perpendicular() const
{
    return Vector(-y_, x_, z_);
}

float Vector::angle(Vector& owner, Vector& target) const
{
    auto vec1 = subtracted(owner).normalize();
    auto vec2 = subtracted(target).normalize();

    return acos(vec1.dot(vec2));
}

Point Vector::point_from(Point& point)
{
    return {point.x() + x_, point.y() + y_, point.z() + z_};
}

void Vector::truncate(float max_velocity)
{
    if (length() <= max_velocity) return;

    *this *= max_velocity / length();
}

bool Vector::is_zero()
{
    return x_ == 0 && y_ == 0 && z_ == 0;
}

void Vector::zero()
{
    x_ = 0;
    y_ = 0;
    z_ = 0;
}

bool Vector::equals(const Vector& right) const
{
    return x_ == right.x() && y_ == right.y() && z_ == right.z();
}

bool Vector::not_equals(const Vector& right) const
{
    return x_ != right.x() || y_ != right.y() || z_ != right.z();
}

Vector Vector::operator+=(Vector& right)
{
    return add(right);
}

Vector Vector::operator-=(Vector& right)
{
    return subtract(right);
}

Vector Vector::operator*=(const float scalar)
{
    return multiply(scalar);
}

Vector Vector::operator/=(const float scalar)
{
    return divide(scalar);
}

Vector& Vector::operator=(Vector right)
{
    x_ = right.x();
    y_ = right.y();
    z_ = right.z();

    return *this;
}

bool Vector::operator==(const Vector& right) const
{
    return equals(right);
}

bool Vector::operator!=(const Vector& right) const
{
    return not_equals(right);
}

Vector Vector::operator+(Vector& right) const
{
    return added(right);
}

Vector Vector::operator-(Vector& right) const
{
    return subtracted(right);
}

Vector Vector::operator*(const float scalar) const
{
    return multiplied(scalar);
}

Vector Vector::operator/(const float scalar) const
{
    return divided(scalar);
}

std::ostream& operator<<(std::ostream& ostream, Vector& vector)
{
    ostream << vector.x() << ", " << vector.y() << ", " << vector.z();

    return ostream;
}
