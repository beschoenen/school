#pragma once
#include <limits>

class random
{
public:
    static size_t get(int const max);
    static size_t get(int const min, int const max);

    static float_t get(float const max);
    static float_t get(float const min, float const max);
};
