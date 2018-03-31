#include "stdafx.h"
#include "ship.h"
#include <cstring>
#include <algorithm>
#include "commodity_price_reader.h"

ship::ship(ship& ship) : name(new str(ship.name->data())), price(ship.price), space(ship.space),
                         cannon_space(ship.cannon_space), current_health(ship.current_health), health(ship.health)
{
    set_attributes(ship.attr);
}

ship::ship(str* name, const size_t price, const size_t space, const size_t cannon_space, const int health)
    : name(name), price(price), space(space), cannon_space(cannon_space), current_health(health), health(health)
{
}

ship::~ship()
{
    delete name;
}

void ship::set_attributes(const size_t attri[2])
{
    for (auto i = 0; i < 2; i++)
    {
        attr[i] = std::move(attri[i]);
    }
}

bool ship::has_attribute(const attributes attribute) const
{
    auto has_attribute = false;
    for (auto i = 0; i < 2; i++)
    {
        if (attr[i] < 3)
        {
            has_attribute = attribute == attributes(attr[i]);

            if (has_attribute) 
            {
                return has_attribute;
            }
        }
    }

    return has_attribute;
}

size_t ship::get_sell_price() const
{
    return price / 2;
}

void ship::move_items(commodity* _inventory[INVENTORY_SIZE], cannon* _cannons[CANNON_SIZE])
{
    for (auto i = 0; i < INVENTORY_SIZE; i++)
    {
        if (_inventory[i] == nullptr) break;

        inventory[i] = _inventory[i];
    }

    for (auto i = 0; i < CANNON_SIZE; i++)
    {
        if (_cannons[i] == nullptr) break;

        cannons[i] = _cannons[i];
    }
}

void ship::add_cannon(cannon* cannon)
{
    for (auto i = 0; i < static_cast<int>(cannon_space); i++)
    {
        if (cannons[i] == nullptr)
        {
            cannons[i] = cannon;
            break;
        }
    }
}

void ship::add_commodity(commodity* commodity)
{
    for (auto i = 0; i < static_cast<int>(space); i++)
    {
        if (inventory[i] == nullptr)
        {
            inventory[i] = commodity;
            break;
        }
    }
}

cannon* ship::remove_cannon(const char class_name[20])
{
    cannon* c = nullptr;
    cannon* last = nullptr;

    for (auto i = cannon_space - 1; i >= 0; --i)
    {
        if (cannons[i] == nullptr) continue;

        if (cannons[i]->is(class_name) && last == nullptr)
        {
            c = cannons[i];
            cannons[i] = nullptr;
            break;
        }

        if (last == nullptr)
        {
            last = cannons[i];
            cannons[i] = nullptr;
        }
        else if (cannons[i]->is(class_name))
        {
            c = cannons[i];
            cannons[i] = last;
            break;
        }
    }

    return c;
}

commodity* ship::remove_commodity(const char name[20])
{
    commodity* c = nullptr;
    commodity* last = nullptr;

    for (auto i = space - 1; i >= 0; --i)
    {
        if (inventory[i] == nullptr) continue;

        if (*inventory[i]->name == name && last == nullptr)
        {
            c = inventory[i];
            inventory[i] = nullptr;
            break;
        }

        if (last == nullptr)
        {
            last = inventory[i];
            inventory[i] = nullptr;
        }
        else if (*inventory[i]->name == name)
        {
            c = inventory[i];
            inventory[i] = last;
            break;
        }
    }

    return c;
}

bool ship::can_fit_cannon() const
{
    for (auto i = 0; i < static_cast<int>(cannon_space); i++)
    {
        if (cannons[i] == nullptr) return true;
    }

    return false;
}

size_t ship::can_fit_commodity(const size_t amount) const
{
    auto count = space;
    for (auto item : inventory)
    {
        if (item == nullptr) break;

        count--;
    }

    return std::min(count, amount);
}

void ship::clear_items()
{
    for (auto item : inventory)
    {
        if (item == nullptr) break;
        delete item;
    }

    memset(inventory, 0x00, INVENTORY_SIZE);
}

void ship::clear_cannons()
{
    for (auto cannon : cannons)
    {
        delete cannon;
    }

    memset(cannons, 0x00, CANNON_SIZE);
}

size_t ship::cannon_count() const
{
    size_t size = 0;

    for (auto cannon : cannons)
    {
        if (cannon == nullptr) return size;

        size++;
    }

    return size;
}

size_t ship::commodity_count() const
{
    size_t size = 0;

    for (auto item : inventory)
    {
        if (item == nullptr) return size;

        size++;
    }

    return size;
}

size_t ship::get_amount_of_commodity(const char* data) const
{
    size_t size = 0;

    for (auto item : inventory)
    {
        if (item == nullptr) return size;

        if (*item->name == data)
        {
            size++;
        }
    }

    return size;
}

size_t ship::get_amount_of_cannon(const char* data) const
{
    size_t size = 0;

    for (auto item : cannons)
    {
        if (item == nullptr) return size;

        if (item->is(data))
        {
            size++;
        }
    }

    return size;
}

size_t ship::can_sell(const char* data, const size_t amount) const
{
    size_t count = 0;
    for (auto item : inventory)
    {
        if (item == nullptr) break;

        if (*item->name == data) count++;
    }

    return std::min(count, amount);
}
