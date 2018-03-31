#include "stdafx.h"
#include "csv_reader.h"
#include <iostream>
#include <string>

csv_reader::csv_reader(const char* filename)
{
    file_name_ = new str(filename);
    file_stream_ = new std::fstream(filename, std::ios::in);
}

csv_reader::~csv_reader()
{
    if (file_stream_->is_open())
    {
        file_stream_->close();
    }

    delete file_stream_;
    delete file_name_;
}

void csv_reader::check_file_read() const
{
    if (!file_stream_->is_open())
    {
        char line[1];

        std::cout << "There was an error reading " << file_name_->data() << ". The program will now exit.";
        std::cin.getline(line, 1);

        exit(1);
    }
}

str** csv_reader::parse_line(const char* line)
{
    const auto items = new str*[30];

    auto size = 0;
    auto current = 0;

    char current_string[20];
    memset(current_string, 0x00, 20);

    for (size_t i = 0; i < strlen(line); i++)
    {
        if (line[i] == ';')
        {
            items[size++] = new str(current_string);

            memset(current_string, 0x00, 20);
            current = 0;
            continue;
        }

        current_string[current++] = line[i];
    }

    items[size] = new str(current_string);

    return items;
}
