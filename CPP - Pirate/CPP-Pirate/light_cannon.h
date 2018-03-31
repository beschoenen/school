#pragma once
#include "cannon.h"

class light_cannon : public cannon
{
public:
    light_cannon();

    bool is(const char* class_name) const override;
};
