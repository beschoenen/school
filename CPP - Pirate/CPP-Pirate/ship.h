#pragma once
#include "str.h"
#include "commodity.h"
#include "cannon.h"

#define CANNON_SIZE 50
#define INVENTORY_SIZE 800

class ship
{
public:
    enum attributes
    {
        light,
        heavy, // log
        small
    };

    commodity* inventory[INVENTORY_SIZE]{nullptr};
    cannon* cannons[CANNON_SIZE]{nullptr};

    str* name;
    size_t price;
    size_t space;
    size_t cannon_space;
    int current_health;
    int health;
    size_t attr[2];

    ship(ship& ship);
    explicit ship(str* name, const size_t price, const size_t space, const size_t cannon_space, const int health);
    ~ship();

    void set_attributes(const size_t attri[2]);
    bool has_attribute(const attributes attribute) const;

    size_t get_sell_price() const;

    void move_items(commodity* inventory[INVENTORY_SIZE], cannon* cannons[CANNON_SIZE]);

    void add_cannon(cannon* cannon);
    void add_commodity(commodity* commodity);

    cannon* remove_cannon(const char name[20]);
    commodity* remove_commodity(const char name[20]);

    bool can_fit_cannon() const;
    size_t can_fit_commodity(const size_t amount = 1) const;

    void clear_items();
    void clear_cannons();

    size_t cannon_count() const;
    size_t commodity_count() const;

    size_t get_amount_of_commodity(const char* data) const;
    size_t get_amount_of_cannon(const char* data) const;

    size_t can_sell(const char* data, const size_t amount = 1) const;
};
