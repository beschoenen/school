#ifndef LINAL_POINT_H
#define LINAL_POINT_H

class Vector;

#include <memory>
#include "Matrix.h"

class Point
{
    float x_;
    float y_;
    float z_;

public:
    Point();

    explicit Point(Vector vector);

    Point(int x, int y, int z = 1);

    Point(float x, float y, float z = 1);

    /////////////////////
    /// Getters / Setters

    float x() const;

    void x(float value);

    float y() const;

    void y(float value);

    float z() const;

    void z(float value);

    Vector vector_to(Point right);

    std::shared_ptr<Matrix> to_4d_matrix(int w = 1);

    Point center_to(Point point);

    Point barycentric(Point left, Point right);

    float distance(Point to);

    float dot();

    bool is_zero();

    friend std::ostream& operator<<(std::ostream& ostream, Point& point);

    friend std::ostream& operator<<(std::ostream& ostream, Point point);

    Point operator-(Point right);

    void operator+=(Point right);
};

#endif //LINAL_POINT_H
