#ifndef GAME_UTIL_H
#define GAME_UTIL_H

#include <string>
#include <map>
#include <unordered_map>

class File
{
public:

    // Directory

    static std::string get_settings_directory();

    static bool has_settings_directory();

    static bool create_settings_directory();

    // Sub Directory

    static std::string get_sub_directory(std::string directory);

    static std::string get_oldest_sub_directory(std::string directory);

    static bool has_sub_directory(std::string directory);

    static bool create_sub_directory(std::string directory);

    static bool delete_sub_directory(std::string directory);

    static std::unordered_map<std::string, bool> list_directory(std::string directory);

    // File

    static std::string get_file_path(std::string directory, std::string filename);

    static bool has_file(std::string directory, std::string filename);

    static bool write_file(std::string directory, std::string filename, std::string content);

    static std::string read_file(std::string directory, std::string filename);

    static bool delete_file(std::string directory, std::string filename);
};

#endif //GAME_UTIL_H
