#include "stdafx.h"
#include "commodity_price_reader.h"


commodity_price_reader::commodity_price_reader(): csv_reader("goederen prijzen.csv")
{
}

commodity_price_reader::~commodity_price_reader()
{
}

void commodity_price_reader::get_commodity_prices(range* prices[], const char* port_name) const
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
            for (auto i = 1; i <= CP_SIZE; i++)
            {
                if (!port[i]->data()) break;

                auto second = 0;
                auto index = 0;
                char numbers[2][4]{'\0'};

                const auto rng = port[i]->data();
                for (size_t j = 0; j < strlen(rng); j++)
                {
                    if (rng[j] == '-')
                    {
                        index = 0;
                        second = 1;
                        continue;
                    }

                    numbers[second][index] = rng[j];
                    index++;
                }

                prices[i - 1] = new range(headers[i], atoi(numbers[0]), atoi(numbers[1]));
                delete headers[i];
            }
        }

        for (auto i = 0; i <= CP_SIZE; i++)
        {
            delete port[i];
        }

        delete port;
    }

    delete headers[0];
    delete headers;
}
