#pragma once
#include "csv_reader.h"
#include "ship.h"

#define S_SIZE 13

class ship_reader : public csv_reader
{
public:
    ship_reader();
    ~ship_reader();

    void get_ships(ship* ships[]) const;

    ship* get_ship(const char* name) const;
};
