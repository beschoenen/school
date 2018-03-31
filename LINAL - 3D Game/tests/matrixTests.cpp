#include "gtest/gtest.h"
#include "../src/Models/Matrix.h"

TEST(Matrix, CanNotMultiplyDifferentSizedMatrices)
{
    auto matrix = Matrix(4, 1);

    try
    {
        matrix * matrix;
        FAIL() << "Expected thrown std::string";
    }
    catch (std::string& err)
    {
        EXPECT_EQ(err, "Can not compare matrices");
    }
}

TEST(Matrix, CanCreateCorrectScalingMatrix)
{
    auto matrix = Matrix::scale(Three, 2);

    auto expected = Matrix(4, 4);
    expected.set(0, 0, 2);
    expected.set(1, 1, 2);
    expected.set(2, 2, 2);
    expected.set(3, 3, 1);

    EXPECT_TRUE(*matrix == expected);
}

TEST(Matrix, CanCreateCorrectRotationMatrices)
{
    auto rad = float(10 * M_PI / 180);

    auto x_rotation = Matrix::rotationX(rad);
    auto y_rotation = Matrix::rotationY(rad);
    auto z_rotation = Matrix::rotationZ(rad);

    auto x_expected = Matrix(4, 4);
    x_expected.set(0, 0, 1);
    x_expected.set(1, 1, cos(rad));
    x_expected.set(2, 1, sin(rad));
    x_expected.set(1, 2, -sin(rad));
    x_expected.set(2, 2, cos(rad));
    x_expected.set(3, 3, 1);

    auto y_expected = Matrix(4, 4);
    y_expected.set(0, 0, cos(rad));
    y_expected.set(2, 0, sin(rad));
    y_expected.set(1, 1, 1);
    y_expected.set(0, 2, -sin(rad));
    y_expected.set(2, 2, cos(rad));
    y_expected.set(3, 3, 1);

    auto z_expected = Matrix(4, 4);
    z_expected.set(0, 0, cos(rad));
    z_expected.set(1, 0, sin(rad));
    z_expected.set(0, 1, -sin(rad));
    z_expected.set(1, 1, cos(rad));
    z_expected.set(2, 2, 1);
    z_expected.set(3, 3, 1);

    EXPECT_TRUE(*x_rotation == x_expected);
    EXPECT_TRUE(*y_rotation == y_expected);
    EXPECT_TRUE(*z_rotation == z_expected);
}

TEST(Matrix, CanMultiplyMatrices)
{
    auto left = Matrix(2, 2);
    left.set(0, 0, 4);
    left.set(1, 0, 2);
    left.set(0, 1, 1);
    left.set(1, 1, 3);

    auto right = Matrix(2, 3);
    right.set(0, 0, 3);
    right.set(1, 0, 2);
    right.set(0, 1, 0);
    right.set(1, 1, 5);
    right.set(0, 2, 4);
    right.set(1, 2, 1);

    auto expected = Matrix(2, 3);
    expected.set(0, 0, 14);
    expected.set(1, 0, 12);
    expected.set(0, 1, 5);
    expected.set(1, 1, 15);
    expected.set(0, 2, 17);
    expected.set(1, 2, 11);

    auto result = left * right;

    EXPECT_TRUE(*result == expected);
}
