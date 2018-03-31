#include "stdafx.h"
#include "stock.h"
#include <algorithm>

stock::stock(str* name, const size_t amount, const size_t price) : name(name), amount(amount), price(price)
{
}

stock::~stock()
{
    delete name;
}

void stock::buy(const size_t count)
{
    bought += count;
}

size_t stock::can_buy(const size_t count) const
{
    return std::min(amount - bought, count);
}
