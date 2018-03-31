#include "random.h"

std::random_device rd_;
std::mt19937 mt_;

random::random()
{
    mt_.seed(rd_());
}

class random& random::instance()
{
    static random instance;

    return instance;
}

int random::get(int max) const
{
    return get(0, max);
}

int random::get(int min, int max) const
{
    return std::uniform_int_distribution<int>{min, max}(mt_);
}

float random::getf(float max) const
{
    return getf(0, max);
}

float random::getf(float min, float max) const
{
    return std::uniform_real_distribution<float>{min, max}(mt_);
}
