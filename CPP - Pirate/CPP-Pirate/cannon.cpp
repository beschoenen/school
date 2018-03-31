#include "stdafx.h"
#include "cannon.h"
#include "random.h"

cannon::cannon(const size_t price, const size_t max_damage) : max_damage_(max_damage), price(price)
{
}

cannon::cannon() : cannon(200, 4)
{
}

size_t cannon::get_damage() const
{
    return random::get(static_cast<int>(max_damage_));
}

size_t cannon::get_sell_price() const
{
    return price / 2;
}

bool cannon::is(const char* class_name) const
{
    return class_name == "normal";
}
