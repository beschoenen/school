#ifndef MACHIAVELLI_CSVREADER_H
#define MACHIAVELLI_CSVREADER_H


#include <fstream>

class CsvReader
{
protected:
    CsvReader() = default;

public:
    std::vector<std::vector<std::string>> process_csv(std::ifstream& ifstream) const;
};


#endif //MACHIAVELLI_CSVREADER_H
