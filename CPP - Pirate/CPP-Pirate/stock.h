#pragma once
#include "str.h"

class stock
{
public:
    str* name;
    size_t amount;
    size_t price;
    size_t bought = 0;

    explicit stock(str* name, const size_t amount, const size_t price);
    ~stock();

    void buy(const size_t count = 1);
    size_t can_buy(const size_t count = 1) const;
};
