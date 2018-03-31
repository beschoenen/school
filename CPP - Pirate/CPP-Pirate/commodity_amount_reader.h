#pragma once
#include "csv_reader.h"
#include "range.h"

#define CA_SIZE 16

class commodity_amount_reader : public csv_reader
{
public:
    commodity_amount_reader();
    ~commodity_amount_reader();

    void get_commodity_amounts(range* amounts[], const char* port_name) const;
};
