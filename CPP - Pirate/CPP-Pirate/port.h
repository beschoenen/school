#pragma once
#include "stock.h"

class port
{
public:
    str* name;
    size_t turns;

    float_t rate;
    float_t amount;

    stock* stocks[18];

    explicit port(const char* name);
    explicit port(const char* name, const size_t turns);
    ~port();

    void buy(const char name[20], const size_t amount = 1) const;
    size_t can_buy(const char name[20], const size_t amount = 1) const;

    size_t get_price_for_stock(const char* name) const;
    size_t get_stock(const char name[20]) const;

    void load_stock();
};
