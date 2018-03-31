#include "Matrix.h"
#include <sstream>
#include <iostream>
#define _USE_MATH_DEFINES
#include <math.h>

Matrix::Matrix(Dimensions dimension) : Matrix(dimension + 1, dimension + 1)
{
}

Matrix::Matrix(int rows, int columns) : rows_{rows}, columns_{columns}
{
    matrix_ = new float[rows * columns];

    for (int i = 0; i < rows * columns; i++)
    {
        matrix_[i] = 0.f;
    }
}

Matrix::~Matrix()
{
    delete[] matrix_;
}

float Matrix::get(int row, int column) const
{
    return matrix_[index(row, column)];
}

void Matrix::set(int row, int column, float value) const
{
    matrix_[index(row, column)] = value;
}

int Matrix::rows() const
{
    return rows_;
}

int Matrix::columns() const
{
    return columns_;
}

bool Matrix::can_be_multiplied(Matrix& right) const
{
    return columns() == right.rows();
}

int Matrix::index(int row, int column) const
{
    return column + columns_ * row;
}

///////////
/// Statics

std::shared_ptr<Matrix> Matrix::identity(int rows, int columns, float value)
{
    auto matrix = std::make_shared<Matrix>(rows, columns);

    for (auto i = 0; i < columns; i++)
    {
        if (i > rows) break;

        matrix->set(i, i, value);
    }

    return matrix;
}

std::shared_ptr<Matrix> Matrix::identity(Dimensions dimension, float value)
{
    return Matrix::identity(dimension + 1, dimension + 1, value);
}

std::shared_ptr<Matrix> Matrix::translation(Dimensions dimension, float* values)
{
    auto matrix = Matrix::identity(dimension);

    for (int i = 0; i < dimension; ++i)
    {
        matrix->set(i, dimension, values[i]);
    }

    return matrix;
}

std::shared_ptr<Matrix> Matrix::scale(Dimensions dimension, float scale)
{
    auto matrix = Matrix::identity(dimension, scale);

    matrix->set(matrix->rows() - 1, matrix->columns() - 1, 1);

    return matrix;
}

std::shared_ptr<Matrix> Matrix::rotationZ(float rad, bool reversed)
{
    auto matrix = Matrix::identity(Three);

    matrix->set(0, 0, cos(rad));
    matrix->set(0, 1, reversed ? sin(rad) : -sin(rad));
    matrix->set(1, 0, reversed ? -sin(rad) : sin(rad));
    matrix->set(1, 1, cos(rad));

    return matrix;
}

std::shared_ptr<Matrix> Matrix::rotationX(float rad, bool reversed)
{
    auto matrix = Matrix::identity(Three);

    matrix->set(1, 1, cos(rad));
    matrix->set(1, 2, reversed ? sin(rad) : -sin(rad));
    matrix->set(2, 1, reversed ? -sin(rad) : sin(rad));
    matrix->set(2, 2, cos(rad));

    return matrix;
}

std::shared_ptr<Matrix> Matrix::rotationY(float rad, bool reversed)
{
    auto matrix = Matrix::identity(Three);

    matrix->set(0, 0, cos(rad));
    matrix->set(0, 2, reversed ? sin(rad) : -sin(rad));
    matrix->set(2, 0, reversed ? -sin(rad) : sin(rad));
    matrix->set(2, 2, cos(rad));

    return matrix;
}

std::shared_ptr<Matrix> Matrix::look_at(Vector& camera, Vector& target)
{
    auto matrix = std::make_shared<Matrix>(4, 4);

    auto vec_z = (camera - target).normalized();
    auto vec_x = Vector(0, 1, 0).crossed(vec_z).normalize();
    auto vec_y = vec_z.crossed(vec_x);

    matrix->set(0, 0, vec_x.x());
    matrix->set(0, 1, vec_x.y());
    matrix->set(0, 2, vec_x.z());

    matrix->set(1, 0, vec_y.x());
    matrix->set(1, 1, vec_y.y());
    matrix->set(1, 2, vec_y.z());

    matrix->set(2, 0, vec_z.x());
    matrix->set(2, 1, vec_z.y());
    matrix->set(2, 2, vec_z.z());

    matrix->set(0, 3, -camera.dot(vec_x));
    matrix->set(1, 3, -camera.dot(vec_y));
    matrix->set(2, 3, -camera.dot(vec_z));
    matrix->set(3, 3, 1);

    return matrix;
}

std::shared_ptr<Matrix> Matrix::perspective(float near, float far, float fov)
{
    const auto rad = float(fov * M_PI / 180);
    const auto scale = near * tanf(rad * 0.5f);
    const auto difference = far - near;

    auto matrix = std::make_shared<Matrix>(Three);

    matrix->set(0, 0, scale);

    matrix->set(1, 1, scale);

    matrix->set(2, 2, -far / difference);
    matrix->set(2, 3, -1);

    matrix->set(3, 2, (-far * near) / difference);

    return matrix;
}

////////////////////
/// Operator Methods

std::string Matrix::print() const
{
    std::stringstream output;

    for (auto y = 0; y < rows(); y++)
    {
        output << "[ ";

        for (auto x = 0; x < columns(); x++)
        {
            output << get(y, x);
            if (x + 1 < columns())
            {
                output << " ";
            }
        }

        output << " ]" << std::endl;
    }

    return output.str();
}

std::shared_ptr<Matrix> Matrix::multiply(std::shared_ptr<Matrix> right)
{
    return multiply(*right);
}

std::shared_ptr<Matrix> Matrix::multiply(Matrix& right)
{
    if (!can_be_multiplied(right)) throw std::string("Can not compare matrices");

    const auto final = std::make_shared<Matrix>(rows(), right.columns());

    for (auto rc = 0; rc < right.columns(); rc++)
    {
        for (auto lr = 0; lr < rows(); lr++)
        {
            float value = 0;

            for (auto lc = 0; lc < columns(); lc++)
            {
                const auto value_left = get(lr, lc);
                const auto value_right = right.get(lc, rc);

                value += value_left * value_right;
            }

            final->set(lr, rc, value);
        }
    }

    return final;
}

/////////////
/// Operators

std::ostream& operator<<(std::ostream& os, Matrix& matrix)
{
    return os << matrix.print();
}

std::ostream& operator<<(std::ostream& os, Matrix* matrix)
{
    return os << matrix->print();
}

std::shared_ptr<Matrix> operator*(Matrix& left, std::shared_ptr<Matrix>& right)
{
    return left.multiply(right);
}

std::shared_ptr<Matrix> operator*(Matrix& left, Matrix& right)
{
    return left.multiply(right);
}

bool operator==(Matrix& left, Matrix& right)
{
    if (left.rows() != right.rows() || left.columns() != right.columns()) return false;

    for (auto row = 0; row < left.rows(); row++)
    {
        for (auto column = 0; column < left.rows(); column++)
        {
            if (left.get(row, column) != right.get(row, column)) return false;
        }
    }

    return true;
}
