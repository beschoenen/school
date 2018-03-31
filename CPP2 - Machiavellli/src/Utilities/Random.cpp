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

int Random::get(const int max) const
{
    return get(0, max);
}

int Random::get(const int min, const int max) const
{
    return std::uniform_int_distribution<int>{min, max}(mt_);
}
