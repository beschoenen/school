#ifdef __APPLE__

#include "../File.h"
#include <dirent.h>
#include <sys/stat.h>
#include <unistd.h>
#include <iostream>

#define PERMISSIONS (S_IRWXU | S_IRWXG | S_IRWXO)

std::string File::get_settings_directory()
{
    std::string root = getenv("HOME");

    return root + "/.saf-game";
}

std::string File::get_oldest_sub_directory(std::string directory)
{
    if (!File::has_sub_directory(directory))
    {
        return "";
    }

    std::string oldest;
    long time = -1;

    auto dir = opendir(File::get_sub_directory(directory).c_str());

    dirent* item;
    while ((item = readdir(dir)) != nullptr)
    {
        if (item->d_name[0] == '.') continue;

        auto filepath = File::get_sub_directory(directory) + "/" + item->d_name;

        struct stat stats{};
        if (stat(filepath.c_str(), &stats) != 0)
        {
            std::cerr << "Error getting stats" << std::endl;
            continue;
        }

        if (time < 0 || stats.st_mtimespec.tv_sec < time)
        {
            time = stats.st_mtimespec.tv_sec;
            oldest = item->d_name;
        }
    }

    closedir(dir);

    return oldest;
}

bool File::has_settings_directory()
{
    auto exists = false;

    auto dir = opendir(File::get_settings_directory().c_str());
    if (dir)
    {
        exists = true;
        closedir(dir);
    }

    return exists;
}

bool File::create_settings_directory()
{
    if (File::has_settings_directory()) return true;

    return mkdir(File::get_settings_directory().c_str(), PERMISSIONS) == 0;
}

bool File::has_sub_directory(std::string directory)
{
    if (!File::has_settings_directory()) return false;

    auto exists = false;

    auto dir = opendir(File::get_sub_directory(std::move(directory)).c_str());
    if (dir)
    {
        exists = true;
        closedir(dir);
    }

    return exists;
}

bool File::create_sub_directory(std::string directory)
{
    if (File::has_sub_directory(directory)) return true;

    return mkdir(File::get_sub_directory(directory).c_str(), PERMISSIONS) == 0;
}

bool File::delete_sub_directory(std::string directory)
{
    if (!File::has_sub_directory(directory)) return false;

    return rmdir(File::get_sub_directory(directory).c_str()) == 0;
}

std::unordered_map<std::string, bool> File::list_directory(std::string directory)
{
    std::unordered_map<std::string, bool> contents;

    if (!File::has_sub_directory(directory))
    {
        return contents;
    }

    auto dir = opendir(File::get_sub_directory(directory).c_str());

    dirent* item;
    while ((item = readdir(dir)) != nullptr)
    {
        if (item->d_name[0] == '.') continue;

        contents.insert_or_assign(item->d_name, item->d_type == DT_DIR);
    }

    closedir(dir);

    return contents;
}

#endif
