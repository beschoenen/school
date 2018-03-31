#include "stdafx.h"
#include "port.h"
#include "random.h"
#include "commodity_amount_reader.h"
#include "commodity_price_reader.h"

port::port(const char* name) : port(name, 0)
{
    load_stock();
}

port::port(const char* name, const size_t turns) : name(new str(name)), turns(turns)
{
    rate = random::get(1.0f);
    amount = random::get(1.0f);

    memset(stocks, 0x00, 18);
}

port::~port()
{
    delete name;

    for (auto item : stocks)
    {
        if (item == nullptr) break;

        delete item;
    }
}

void port::buy(const char name[20], const size_t amount) const
{
    for (auto item : stocks)
    {
        if (*item->name != name) continue;

        item->buy(amount);
        break;
    }
}

size_t port::can_buy(const char name[20], const size_t amount) const
{
    for (auto item : stocks)
    {
        if (*item->name != name) continue;

        return item->can_buy(amount);
    }

    return 0;
}

size_t port::get_price_for_stock(const char* name) const
{
    auto price = -1;

    for (auto stock : stocks)
    {
        if (*stock->name == name)
        {
            price = stock->price;
            break;
        }
    }

    return price;
}

size_t port::get_stock(const char name[20]) const
{
    for (auto item : stocks)
    {
        if (*item->name != name) continue;

        return item->amount - item->bought;
    }

    return 0;
}

void port::load_stock()
{
    char names[15][9]{
        "brick", "sheet", "cocoa",
        "cotton", "dye", "fish",
        "hemp", "potatoes", "rum",
        "salt", "sugar", "tobacco",
        "grain", "meat", "wood"
    };

    range* amounts[15]{nullptr};
    range* prices[15]{nullptr};

    const auto amount_reader = new commodity_amount_reader();
    amount_reader->get_commodity_amounts(amounts, name->data());

    delete amount_reader;

    const auto prices_reader = new commodity_price_reader();
    prices_reader->get_commodity_prices(prices, name->data());

    delete prices_reader;

    for (auto i = 0; i < 15; i++)
    {
        const int price = random::get(static_cast<int>(prices[i]->min), static_cast<int>(prices[i]->max));
        const auto amnt = static_cast<int>(amounts[i]->min + (amounts[i]->max - amounts[i]->min) * amount);
        stocks[i] = new stock(new str(names[i]), amnt, price);

        delete amounts[i];
        delete prices[i];
    }

    stocks[15] = new stock(new str("light_cannon"), random::get(5), 50);
    stocks[16] = new stock(new str("normal_cannon"), random::get(3), 200);
    stocks[17] = new stock(new str("heavy_cannon"), random::get(2), 1000);
}
