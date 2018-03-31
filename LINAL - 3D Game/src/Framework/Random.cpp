#include "Random.h"

std::random_device rd_;
std::mt19937 mt_;

Random::Random()
{
    mt_.seed(rd_());
}

Random& Random::instance()
{
    static Random instance;

    return instance;
}

int Random::get(int max) const
{
    return get(0, max);
}

int Random::get(int min, int max) const
{
    return std::uniform_int_distribution<int>{min, max}(mt_);
}

float Random::getf(float max) const
{
    return getf(0, max);
}

float Random::getf(float min, float max) const
{
    return std::uniform_real_distribution<float>{min, max}(mt_);
}
