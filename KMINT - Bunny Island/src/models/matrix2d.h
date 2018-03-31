#ifndef _KMINT_MATRIX2D_H
#define _KMINT_MATRIX2D_H

#include <vector>
#include "point.h"

namespace kmint
{
    class matrix2d
    {
    public:
        struct matrix
        {

            float _11, _12, _13;
            float _21, _22, _23;
            float _31, _32, _33;

            matrix()
            {
                _11 = 0.0;
                _12 = 0.0;
                _13 = 0.0;
                _21 = 0.0;
                _22 = 0.0;
                _23 = 0.0;
                _31 = 0.0;
                _32 = 0.0;
                _33 = 0.0;
            }

        };

        matrix m_Matrix;

        matrix2d()
        {
            identity();
        }

        inline void multiply(matrix& min);

        inline void identity();

        inline void translate(float x, float y);

        inline void scale(float x_scale, float y_scale);

        inline void rotate(float rotation);

        inline void rotate(kmint::point& fwd, kmint::point& side);

        inline void transform(std::vector<kmint::point>& points);

        inline void transform(kmint::point& point);

        //accessors to the matrix elements
        void _11(float val) { m_Matrix._11 = val; }

        void _12(float val) { m_Matrix._12 = val; }

        void _13(float val) { m_Matrix._13 = val; }

        void _21(float val) { m_Matrix._21 = val; }

        void _22(float val) { m_Matrix._22 = val; }

        void _23(float val) { m_Matrix._23 = val; }

        void _31(float val) { m_Matrix._31 = val; }

        void _32(float val) { m_Matrix._32 = val; }

        void _33(float val) { m_Matrix._33 = val; }
    };
}

//multiply two matrices together
inline void kmint::matrix2d::multiply(matrix& min)
{
    kmint::matrix2d::matrix mat_temp;

    //first row
    mat_temp._11 = (m_Matrix._11 * min._11) + (m_Matrix._12 * min._21) + (m_Matrix._13 * min._31);
    mat_temp._12 = (m_Matrix._11 * min._12) + (m_Matrix._12 * min._22) + (m_Matrix._13 * min._32);
    mat_temp._13 = (m_Matrix._11 * min._13) + (m_Matrix._12 * min._23) + (m_Matrix._13 * min._33);

    //second
    mat_temp._21 = (m_Matrix._21 * min._11) + (m_Matrix._22 * min._21) + (m_Matrix._23 * min._31);
    mat_temp._22 = (m_Matrix._21 * min._12) + (m_Matrix._22 * min._22) + (m_Matrix._23 * min._32);
    mat_temp._23 = (m_Matrix._21 * min._13) + (m_Matrix._22 * min._23) + (m_Matrix._23 * min._33);

    //third
    mat_temp._31 = (m_Matrix._31 * min._11) + (m_Matrix._32 * min._21) + (m_Matrix._33 * min._31);
    mat_temp._32 = (m_Matrix._31 * min._12) + (m_Matrix._32 * min._22) + (m_Matrix._33 * min._32);
    mat_temp._33 = (m_Matrix._31 * min._13) + (m_Matrix._32 * min._23) + (m_Matrix._33 * min._33);

    m_Matrix = mat_temp;
}

//applies a 2D transformation matrix to a std::vector of Vector2Ds
inline void kmint::matrix2d::transform(std::vector<kmint::point>& points)
{
    for (auto& item : points)
    {
        item.x((m_Matrix._11 * item.x()) + (m_Matrix._21 * item.y()) + (m_Matrix._31));
        item.y((m_Matrix._12 * item.x()) + (m_Matrix._22 * item.y()) + (m_Matrix._32));
    }
}

//applies a 2D transformation matrix to a single Vector2D
inline void kmint::matrix2d::transform(kmint::point& point)
{
    float x = (m_Matrix._11 * point.x()) + (m_Matrix._21 * point.y()) + (m_Matrix._31);
    float y = (m_Matrix._12 * point.x()) + (m_Matrix._22 * point.y()) + (m_Matrix._32);

    point.x(x);
    point.y(y);
}

//create an identity matrix
inline void kmint::matrix2d::identity()
{
    m_Matrix._11 = 1;
    m_Matrix._12 = 0;
    m_Matrix._13 = 0;

    m_Matrix._21 = 0;
    m_Matrix._22 = 1;
    m_Matrix._23 = 0;

    m_Matrix._31 = 0;
    m_Matrix._32 = 0;
    m_Matrix._33 = 1;
}

//create a transformation matrix
inline void kmint::matrix2d::translate(float x, float y)
{
    kmint::matrix2d::matrix mat;

    mat._11 = 1;
    mat._12 = 0;
    mat._13 = 0;

    mat._21 = 0;
    mat._22 = 1;
    mat._23 = 0;

    mat._31 = x;
    mat._32 = y;
    mat._33 = 1;

    //and multiply
    multiply(mat);
}

//create a scale matrix
inline void kmint::matrix2d::scale(float x_scale, float y_scale)
{
    kmint::matrix2d::matrix mat;

    mat._11 = x_scale;
    mat._12 = 0;
    mat._13 = 0;

    mat._21 = 0;
    mat._22 = y_scale;
    mat._23 = 0;

    mat._31 = 0;
    mat._32 = 0;
    mat._33 = 1;

    //and multiply
    multiply(mat);
}


//create a rotation matrix
inline void kmint::matrix2d::rotate(float rot)
{
    kmint::matrix2d::matrix mat;

    float Sin = sin(rot);
    float Cos = cos(rot);

    mat._11 = Cos;
    mat._12 = Sin;
    mat._13 = 0;

    mat._21 = -Sin;
    mat._22 = Cos;
    mat._23 = 0;

    mat._31 = 0;
    mat._32 = 0;
    mat._33 = 1;

    //and multiply
    multiply(mat);
}


//create a rotation matrix from a 2D vector
inline void kmint::matrix2d::rotate(kmint::point& fwd, kmint::point& side)
{
    kmint::matrix2d::matrix mat;

    mat._11 = fwd.x();
    mat._12 = fwd.y();
    mat._13 = 0;

    mat._21 = side.x();
    mat._22 = side.y();
    mat._23 = 0;

    mat._31 = 0;
    mat._32 = 0;
    mat._33 = 1;

    multiply(mat);
}

#endif //_KMINT_MATRIX2D_H

