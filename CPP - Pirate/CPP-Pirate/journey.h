#pragma once
#include "str.h"

class journey
{
public:
    str* name;
    size_t turns;

    explicit journey(const str* name, const size_t turns);
    ~journey();
};
