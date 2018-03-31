#pragma once
#include "str.h"

class range
{
public:
    str* name;
    size_t min;
    size_t max;

    explicit range(const str* name, const size_t min, const size_t max);
    ~range();
};
