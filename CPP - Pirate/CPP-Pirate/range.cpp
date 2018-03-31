#include "stdafx.h"
#include "range.h"
#include <corecrt_memcpy_s.h>

range::range(const str* name, const size_t min, const size_t max) : name(new str(name->data())), min(min), max(max)
{
}

range::~range()
{
    delete name;
}
