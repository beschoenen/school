#pragma once
#include "cannon.h"

class heavy_cannon : public cannon
{
public:
    heavy_cannon();

    bool is(const char* class_name) const override;
};
