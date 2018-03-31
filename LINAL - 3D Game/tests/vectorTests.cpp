#include "gtest/gtest.h"
#include "../src/Models/Vector.h"

TEST(Vector, Subtraction)
{
    Vector result = {6, -8, -2};

    Vector one = {5, -5, 5};
    Vector two = {-1, 3, 7};

    one.subtract(two);

    EXPECT_EQ(one, result);
}

TEST(Vector, Addition)
{
    Vector result = {-1, 4, 12};

    Vector one = {0, -5, 5};
    Vector two = {-1, 9, 7};

    one.add(two);

    EXPECT_EQ(one, result);
}

TEST(Vector, Division)
{
    Vector result = {0.f, -1.66666663f, 4.33333349f};

    Vector one = {0, -5, 13};
    const float scalar = 3.f;

    one.divide(scalar);

    EXPECT_EQ(one, result);
}

TEST(Vector, Multiplication)
{
    Vector result = {0, -20, 20};

    Vector one = {0, -5, 5};
    const float scalar = 4.f;

    one.multiply(scalar);

    EXPECT_EQ(one, result);
}

TEST(Vector, InProductOfVector)
{
    Vector one = {9, -2, 4};
    Vector two = {-3, 3, -8};

    const float result = one.dot(two);

    EXPECT_EQ(-65.f, result);
}

TEST(Vector, OutProductOfVector)
{
    Vector result = {-4, 2, 7};

    Vector one = {-4, -1, -2};
    Vector two = {3, -1, 2};

    one.cross(two);

    EXPECT_EQ(one, result);
}

TEST(Vector, InverseVector)
{
    Vector result = {-0, 5, -9};
    Vector one = {0, -5, 9};

    one.inverse();

    EXPECT_EQ(one, result);
}

TEST(Vector, NormalizeVector)
{
    Vector result = {0.358568579f, -0.597614348f, 0.717137158f};
    Vector one = {3, -5, 6};

    one.normalize();

    EXPECT_EQ(one, result);
}

TEST(Vector, DistanceToVector)
{
    Vector one = {7, -4, 3};
    Vector two = {17, 6, -2};

    float result = one.distance(two);

    EXPECT_EQ(15.f, result);
}

TEST(Vector, LengthOfVector)
{
    Vector one = {-3, 0, 0};
    Vector two = {3, 4, 7};
    Vector three = {2, -2, 3};

    EXPECT_EQ(3.f, one.length());
    EXPECT_EQ(8.60232544f, two.length());
    EXPECT_EQ(4.12310553f, three.length());
}
