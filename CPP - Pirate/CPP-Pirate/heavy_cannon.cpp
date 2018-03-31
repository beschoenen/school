#include "stdafx.h"
#include "heavy_cannon.h"

heavy_cannon::heavy_cannon() : cannon(1000, 6)
{
}

bool heavy_cannon::is(const char* class_name) const
{
    return class_name == "heavy";
}
