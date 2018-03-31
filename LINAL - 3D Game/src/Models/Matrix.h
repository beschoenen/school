#ifndef LINAL_MATRIX_H
#define LINAL_MATRIX_H

#include <ostream>
#include <memory>
#include "Vector.h"
#include "../defines.h"

class Matrix
{
    const int rows_;
    const int columns_;

    float* matrix_;

public:
    explicit Matrix(Dimensions dimension);

    Matrix(int rows, int columns);

    ~Matrix();

    float get(int row, int column) const;

    void set(int row, int column, float value) const;

    int rows() const;

    int columns() const;

    bool can_be_multiplied(Matrix& right) const;

    int index(int row, int column) const;

    ///////////
    /// Statics

    static std::shared_ptr<Matrix> identity(int rows, int columns, float value = 1.f);

    static std::shared_ptr<Matrix> identity(Dimensions dimension, float value = 1.f);

    static std::shared_ptr<Matrix> translation(Dimensions dimension, float values[]);

    static std::shared_ptr<Matrix> scale(Dimensions dimension, float scale);

    static std::shared_ptr<Matrix> rotationZ(float rad, bool reversed = false);

    static std::shared_ptr<Matrix> rotationY(float rad, bool reversed = false);

    static std::shared_ptr<Matrix> rotationX(float rad, bool reversed = false);

    static std::shared_ptr<Matrix> look_at(Vector& camera, Vector& target);

    static std::shared_ptr<Matrix> perspective(float near, float far, float pov);

    ///////////
    /// Methods

    std::string print() const;

    std::shared_ptr<Matrix> multiply(std::shared_ptr<Matrix> right);

    std::shared_ptr<Matrix> multiply(Matrix& right);

    /////////////
    /// Operators

    friend std::ostream& operator<<(std::ostream& os, Matrix* matrix);

    friend std::ostream& operator<<(std::ostream& os, Matrix& matrix);

    friend std::shared_ptr<Matrix> operator*(Matrix& left, std::shared_ptr<Matrix>& right);

    friend std::shared_ptr<Matrix> operator*(Matrix& left, Matrix& right);

    friend bool operator==(Matrix& left, Matrix& right);
};


#endif //LINAL_MATRIX_H
