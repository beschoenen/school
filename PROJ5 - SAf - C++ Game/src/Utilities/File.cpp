#include "File.h"
#include <iostream>
#include <fstream>
#include "../../lib/json/src/json.hpp"

std::string File::get_sub_directory(std::string directory)
{
    return File::get_settings_directory() + "/" + directory;
}

std::string File::get_file_path(std::string directory, std::string filename)
{
    return File::get_sub_directory(std::move(directory)) + "/" + filename;
}

bool File::has_file(std::string directory, std::string filename)
{
    if (!File::has_sub_directory(directory)) return false;

    auto path = File::get_file_path(directory, std::move(filename));

    auto exists = false;

    auto file = fopen(path.c_str(), "r");
    if (file)
    {
        exists = true;
        fclose(file);
    }

    return exists;
}

bool File::write_file(std::string directory, std::string filename, std::string content)
{
    std::ofstream output(File::get_file_path(std::move(directory), std::move(filename)).c_str());

    output << content;
    output.close();

    return true;
}

std::string File::read_file(std::string directory, std::string filename)
{
    if (!File::has_file(directory, filename))
    {
        std::cerr << File::get_file_path(directory, filename) << " does not exist." << std::endl;
    }

    std::ifstream in(File::get_file_path(directory, filename).c_str());
    std::string content((std::istreambuf_iterator<char>(in)), (std::istreambuf_iterator<char>()));

    return content;
}

bool File::delete_file(std::string directory, std::string filename)
{
    return remove(File::get_file_path(std::move(directory), std::move(filename)).c_str()) == 0;
}
