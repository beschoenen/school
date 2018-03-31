#include "stdafx.h"
#include "random.h"
#include <random>
#include <ctime>

// Zie ook:
// http://www.cplusplus.com/reference/random/

size_t random::get(const int max)
{
    return get(0, max);
}

size_t random::get(const int min, const int max)
{
    std::mt19937 generator;
    generator.seed(static_cast<unsigned int>(time(nullptr)));

    const std::uniform_int_distribution<int> distribution(min, max);

    return distribution(generator);
}

float_t random::get(const float max)
{
    return get(0.0, max);
}

float_t random::get(const float min, const float max)
{
    std::mt19937 generator;
    generator.seed(static_cast<unsigned int>(time(nullptr)));

    const std::uniform_real_distribution<float> distribution(min, max);

    return distribution(generator);
}
