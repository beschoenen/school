#pragma once
#include <iosfwd>
#include <fstream>
#include "str.h"

#define PORT_SIZE 24

class csv_reader
{
    str* file_name_;
protected:
    std::fstream* file_stream_;

public:
    explicit csv_reader(const char* filename);
    ~csv_reader();
    void check_file_read() const;

    static str** parse_line(const char* line);
};
