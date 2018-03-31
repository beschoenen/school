#include <vector>
#include <iostream>
#include "CsvReader.h"

std::vector<std::vector<std::string>> CsvReader::process_csv(std::ifstream& ifstream) const
{
    std::vector<std::vector<std::string>> matrix {};

    char line[500];
    memset(line, 0x00, 500);

    while (ifstream.getline(line, 500))
    {
        std::vector<std::string> line_vector;

        auto current = 0;

        char current_string[200];
        memset(current_string, 0x00, 200);

        for (size_t i = 0; i < strlen(line); i++)
        {
            if (line[i] == ';')
            {
                line_vector.emplace_back(current_string);

                memset(current_string, 0x00, 20);
                current = 0;
                continue;
            }

            current_string[current++] = line[i];
        }

        line_vector.emplace_back(current_string);

        matrix.push_back(line_vector);
    }

    return matrix;
}
