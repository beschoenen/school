#ifdef _WINDOWS

#include "../../defines.h"
#include <iterator>
#include "../File.h"
#include <Windows.h>

std::string File::get_settings_directory()
{
    std::string root = getenv("APPDATA");

    return root + "/SAf-Game";
}

bool File::has_settings_directory()
{
    auto ftyp = GetFileAttributesA(File::get_settings_directory().c_str());

    return ftyp != INVALID_FILE_ATTRIBUTES && (ftyp & FILE_ATTRIBUTE_DIRECTORY);
}

bool File::create_settings_directory()
{
    if (File::has_settings_directory()) return true;

    return CreateDirectory(File::get_settings_directory().c_str(), nullptr);
}

std::string File::get_oldest_sub_directory(std::string directory)
{
    WIN32_FIND_DATA fd_file;
    HANDLE handle{ nullptr };

    char s_path[2048];

    sprintf(s_path, "%s\\*.*", File::get_sub_directory(directory).c_str());

    if ((handle = FindFirstFile(s_path, &fd_file)) == INVALID_HANDLE_VALUE)
    {
        printf("Path not found: [%s]\n", File::get_sub_directory(directory).c_str());
    }

    FILETIME* time = nullptr;
    FILETIME* current_time = nullptr;
    std::string oldest;

    try
    {
        do
        {
            std::string file_name = fd_file.cFileName;
            if (file_name.find(".") != 0)
            {
                current_time = new FILETIME(fd_file.ftLastWriteTime);

                if (time == nullptr)
                {
                    time = current_time;
                    oldest = file_name;
                }
                else if (CompareFileTime(time, current_time) == -1)
                {
                    delete current_time;
                }
                else
                {
                    delete time;
                    time = current_time;
                    oldest = file_name;
                }

                sprintf(s_path, "%s\\%s", File::get_sub_directory(directory).c_str(), file_name);
            }
        } while (FindNextFile(handle, &fd_file)); //Find the next file.

        FindClose(handle);
    } catch(...)
    {
        try
        {
            FindClose(handle);
        } catch(...)
        {
            
        }
    }

    if (time != nullptr)
    {
        delete time;
    }

    return oldest;
}

bool File::has_sub_directory(std::string directory)
{
    if (!File::has_settings_directory()) return false;

    auto ftyp = GetFileAttributesA(File::get_sub_directory(std::move(directory)).c_str());

    return ftyp != INVALID_FILE_ATTRIBUTES && (ftyp & FILE_ATTRIBUTE_DIRECTORY);
}

bool File::create_sub_directory(std::string directory)
{
    if (File::has_sub_directory(directory)) return true;

    return CreateDirectory(File::get_sub_directory(directory).c_str(), nullptr);
}

bool File::delete_sub_directory(std::string directory)
{
    return RemoveDirectory(File::get_sub_directory(directory).c_str());
}

std::unordered_map<std::string, bool> File::list_directory(std::string directory)
{
    std::unordered_map<std::string, bool> contents;

    WIN32_FIND_DATA fd_file;
    HANDLE handle{ nullptr };

    char s_path[2048];

    sprintf(s_path, "%s\\*.*", File::get_sub_directory(directory).c_str());

    if ((handle = FindFirstFile(s_path, &fd_file)) == INVALID_HANDLE_VALUE)
    {
        printf("Path not found: [%s]\n", File::get_sub_directory(directory).c_str());
    }

    do
    {
        std::string file_name = fd_file.cFileName;
        if (file_name.find(".") != 0)
        {
            sprintf(s_path, "%s\\%s", File::get_sub_directory(directory).c_str(), file_name);
            contents.insert_or_assign(file_name, fd_file.dwFileAttributes &FILE_ATTRIBUTE_DIRECTORY);
        }
    }
    while (FindNextFile(handle, &fd_file)); //Find the next file.

    FindClose(handle);

    return contents;
}

#endif
