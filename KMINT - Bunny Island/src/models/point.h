#ifndef _KMINT_POINT_H
#define _KMINT_POINT_H

#define _USE_MATH_DEFINES
#include <istream>
#include <math.h>
#include <iostream>

namespace kmint
{
    class point
    {
        float _x;
        float _y;

    public:
        point() : _x{0}, _y{0} {}

        point(float x, float y) : _x{x}, _y{y} {}

        float x() noexcept { return _x; }

        void x(float newX) noexcept { _x = newX; }

        float y() noexcept { return _y; }

        void y(float newY) noexcept { _y = newY; }

        // Operators

        point operator-(point right) { return {_x - right.x(), _y - right.y()}; }

        void operator-=(point right)
        {
            _x -= right.x();
            _y -= right.y();
        }

        point operator+(point right) { return {_x + right.x(), _y + right.y()}; }

        void operator+=(point right)
        {
            _x += right.x();
            _y += right.y();
        }

        point operator*(float scalar) { return {_x * scalar, _y * scalar}; }

        void operator*=(float scalar)
        {
            _x *= scalar;
            _y *= scalar;
        }

        point operator/(float scalar) { return {_x / scalar, _y / scalar}; }

        void operator/=(float scalar)
        {
            _x /= scalar;
            _y /= scalar;
        }

        bool operator==(point right) { return _x == right.x() && _y == right.y(); }

        // Methods

        float dot(point b) { return _x * b.x() + _y * b.y(); }

        float dot() { return dot(*this); }

        float length() { return sqrtf(dot()); }

        point perpendicular() { return {-_y, _x}; }

        float distance(point to) { return sqrtf(distance_squared(to)); }

        float distance_squared(point to) { return (to - *this).dot(); }

        float angle(point to) { return 180.0f / M_PI * atan2f(to.y(), to.x()) + 90.0f; }

        point direction(point to) { return (*this - to).normalized(); }

        void truncate(float max_velocity)
        {
            if (length() <= max_velocity) return;

            *this *= max_velocity / length();
        }

        point truncated(float max_velocity)
        {
            if (length() <= max_velocity) return *this;

            auto ratio = max_velocity / length();

            return {_x * ratio, _y * ratio};
        }

        void reverse() { *this *= -1; }

        point reversed() { return point{_x * -1, _y * -1}; }

        void normalize()
        {
            auto l = length();

            if (l == 0) return;

            _x /= l;
            _y /= l;
        }

        point normalized()
        {
            if (length() == 0) return {0, 0};

            return *this / length();
        }

        point to_node()
        {
            auto x = static_cast<int>(_x);
            auto y = static_cast<int>(_y);

            return
            {
                10.f + (x - (x % 20)),
                10.f + (y - (y % 20))
            };
        }

        bool in_fov(point facing, point right, float radius)
        {
            auto to = (right - *this).normalized();

            return facing.dot(to) >= cos(radius / 2.f);
        }
    };

    std::istream& operator>>(std::istream& is, point& p);
}

#endif
