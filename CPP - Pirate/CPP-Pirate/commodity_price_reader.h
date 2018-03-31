#pragma once
#include "csv_reader.h"
#include "range.h"

#define CP_SIZE 15

class commodity_price_reader : public csv_reader
{
public:
    commodity_price_reader();
    ~commodity_price_reader();

    void get_commodity_prices(range* prices[], const char* port_name) const;
};
