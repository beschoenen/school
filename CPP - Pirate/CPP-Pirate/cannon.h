#pragma once

class cannon
{
protected:
    size_t max_damage_;

    explicit cannon(const size_t price, const size_t max_damage);
public:
    size_t price;

    cannon();

    size_t get_damage() const;

    size_t get_sell_price() const;

    virtual bool is(const char* class_name) const;
};
