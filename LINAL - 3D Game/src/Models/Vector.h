#ifndef PROGRAM_LINE_H
#define PROGRAM_LINE_H

class Point;
class Matrix;

#include "Point.h"
#include "../Framework/Window.h"
#include "Matrix.h"

class Vector
{
    float x_;
    float y_;
    float z_;

public:
    Vector();

    explicit Vector(Point point);

    Vector(int x, int y, int z = 0);

    Vector(float x, float y, float z = 0);

    Vector(const Vector& copy) noexcept;

    Vector(Vector&& move) noexcept;

    /////////////////////
    /// Getters / Setters

    float x() const;

    void x(float value);

    float y() const;

    void y(float value);

    float z() const;

    void z(float value);

    ///////////////
    /// Arithmetics

    Vector subtract(Vector& right);

    Vector subtracted(Vector& right) const;

    Vector add(Vector& right);

    Vector added(Vector& right) const;

    Vector divide(float scalar);

    Vector divided(float scalar) const;

    Vector multiply(float scalar);

    Vector multiplied(float scalar) const;

    //////////////
    /// 2D methods

    float dot();

    float dot(Vector& right) const;

    Vector cross(Vector& right);

    Vector crossed(Vector& right) const;

    Vector inverse();

    Vector inversed() const;

    Vector normalize();

    Vector normalized() const;

    Vector reverse();

    Vector reversed() const;

    float distance(Vector& to);

    float distance_squared(Vector& to);

    float length();

    float length_squared();

    Vector perpendicular() const;

    float angle(Vector& owner, Vector& target) const;

    Point point_from(Point& point);

    void truncate(float max_velocity);

    bool is_zero();

    void zero();

    ///////////////
    /// Comparisons

    bool equals(const Vector& right) const;

    bool not_equals(const Vector& right) const;

    ////////////////////////
    /// Assignment operators

    Vector operator+=(Vector& right);

    Vector operator-=(Vector& right);

    Vector operator*=(float scalar);

    Vector operator/=(float scalar);

    Vector& operator=(Vector right);

    //////////////
    /// Arithmetic

    Vector operator+(Vector& right) const;

    Vector operator-(Vector& right) const;

    Vector operator*(float scalar) const;

    Vector operator/(float scalar) const;

    ///////////////
    /// Comparisons

    bool operator==(const Vector& right) const;

    bool operator!=(const Vector& right) const;

    friend std::ostream& operator<<(std::ostream& ostream, Vector& vector);
};


#endif //PROGRAM_LINE_H
