#include "stdafx.h"
#include "journey.h"

journey::journey(const str* name, const size_t turns) : name(new str(name->data())), turns(turns)
{
}

journey::~journey()
{
    delete name;
}
