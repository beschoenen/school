#include "stdafx.h"
#include "light_cannon.h"

light_cannon::light_cannon() : cannon(50, 2)
{
}

bool light_cannon::is(const char* class_name) const
{
    return class_name == "light";
}
