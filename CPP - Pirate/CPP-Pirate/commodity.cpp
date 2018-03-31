#include "stdafx.h"
#include "commodity.h"

commodity::commodity(const char* name) : name(new str(name))
{
}

commodity::~commodity()
{
    delete name;
}
