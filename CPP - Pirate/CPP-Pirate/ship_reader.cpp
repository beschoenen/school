#include "stdafx.h"
#include "ship.h"
#include "ship_reader.h"


ship_reader::ship_reader(): csv_reader("schepen.csv")
{
}

ship_reader::~ship_reader()
{
}

void ship_reader::get_ships(ship* ships[]) const
{
    check_file_read();

    auto size = -1;
    char string[500];

    str** ships_lines[S_SIZE]{nullptr};

    while (file_stream_->getline(string, 500))
    {
        if (file_stream_ && size >= 0)
        {
            ships_lines[size] = parse_line(string);
        }
        size++;
    }

    file_stream_->close();

    for (auto x = 0; x < S_SIZE; x++)
    {
        const auto current_ship = ships_lines[x];

        const auto name = current_ship[0];
        const auto price = atoi(current_ship[1]->data());
        const auto space = atoi(current_ship[2]->data());
        const auto cannon_space = atoi(current_ship[3]->data());
        const auto health = atoi(current_ship[4]->data());

        auto s = new ship(name, price, space, cannon_space, health);

        const auto attri_line = current_ship[5]->data();
        char current_attri[6]{0};
        str* parsed_attri[2]{nullptr};
        auto index = 0;
        auto current = 0;

        for (size_t i = 0; i < strlen(attri_line); i++)
        {
            if (attri_line[i] == ',')
            {
                parsed_attri[index] = new str(current_attri);
                memset(current_attri, 0x00, 6);
                index++;
                current = 0;
                continue;
            }

            current_attri[current++] = attri_line[i];
        }

        parsed_attri[index] = new str(current_attri);

        size = 0;
        size_t s_attrs[2];
        s_attrs[0] = -1;
        s_attrs[1] = -1;

        for (auto i = 0; i < 2; i++)
        {
            if (parsed_attri[i] == nullptr) continue;

            if (*parsed_attri[i] == "klein")
            {
                s_attrs[size++] = ship::small;
            }
            else if (*parsed_attri[i] == "licht")
            {
                s_attrs[size++] = ship::light;
            }
            else if (*parsed_attri[i] == "log")
            {
                s_attrs[size++] = ship::heavy;
            }

            delete parsed_attri[i];
        }

        s->set_attributes(s_attrs);

        for (auto i = 1; i < 6; i++)
        {
            delete current_ship[i];
        }

        delete current_ship;

        ships[x] = s;
    }
}

ship* ship_reader::get_ship(const char* name) const
{
    ship* working_ship = nullptr;

    ship* ships[S_SIZE]{nullptr};
    get_ships(ships);

    for (auto item : ships)
    {
        if (*item->name != name)
        {
            delete item;
            continue;
        }

        working_ship = item;
    }

    return working_ship;
}
