#pragma once
#include "str.h"

class commodity
{
public:
    str* name;

    explicit commodity(const char* name);
    ~commodity();
};
