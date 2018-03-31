#include "stdafx.h"
#include "journey_reader.h"
#include <string>
#include <list>

journey_reader::journey_reader() : csv_reader("afstanden tussen steden.csv")
{
}

journey_reader::~journey_reader()
{
}

void journey_reader::get_journeys(journey* items[], const char* port_name) const
{
    check_file_read();

    auto size = 0;
    char string[500];

    str** headers{nullptr};
    str** ports[PORT_SIZE]{nullptr};

    while (file_stream_->getline(string, 500))
    {
        if (string[0] == '#') continue;

        if (string[0] == ';')
        {
            headers = parse_line(string);
        }
        else if (file_stream_)
        {
            ports[size++] = parse_line(string);
        }
    }

    file_stream_->close();

    for (auto x = 0; x < PORT_SIZE; x++)
    {
        const auto port = ports[x];

        if (*port[0] == port_name)
        {
            for (auto i = 1; i <= PORT_SIZE; i++)
            {
                if (!port[i]->data()) break;

                items[i - 1] = new journey(headers[i], atoi(port[i]->data()));
                delete headers[i];
            }
        }

        for (auto i = 0; i <= PORT_SIZE; i++)
        {
            delete port[i];
        }

        delete port;
    }

    delete headers[0];
    delete headers;
}
