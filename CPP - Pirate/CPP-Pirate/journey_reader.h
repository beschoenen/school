#pragma once
#include "csv_reader.h"
#include "journey.h"

class journey_reader : public csv_reader
{
public:
    journey_reader();
    ~journey_reader();

    void journey_reader::get_journeys(journey* items[], const char* port_name) const;
};
