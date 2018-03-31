#include "AdvertisementManager.h"
#include <iostream>
#include "../../Utilities/File.h"
#include "../../Utilities/Random.h"
#include "../../../lib/json/src/json.hpp"

void AdvertisementManager::init()
{
    // Init main ad folder
    if (!File::has_sub_directory(AD_DIR))
    {
        File::create_sub_directory(AD_DIR);
    }

    // Init image folder
    auto image_folder = std::string(AD_DIR) + "/images";
    if (!File::has_sub_directory(image_folder))
    {
        File::create_sub_directory(image_folder);
    }

    // Init single image folder
    auto single_image_folder = std::string(AD_DIR) + "/singles";
    if (!File::has_sub_directory(single_image_folder))
    {
        File::create_sub_directory(single_image_folder);
    }
}

std::shared_ptr<Advertisement> AdvertisementManager::pick_one()
{
    auto files = File::list_directory(AD_DIR);
    std::vector<std::string> file_names;

    for (auto f : files)
    {
        if (f.second == 1) continue;

        file_names.push_back(f.first);
    }

    if (file_names.empty())
    {
        return single_image();
    }

    try
    {
        auto random_file_name = file_names[Random::instance().get(0, file_names.size() - 1)];
        auto json = nlohmann::json::parse(File::read_file(AD_DIR, random_file_name));

        return std::shared_ptr<Advertisement>(new Advertisement(json["image"], json["web_url"]));
    }
    catch (...)
    {
        std::cout << "Error loading ad" << std::endl;

        return nullptr;
    }
}

std::shared_ptr<Advertisement> AdvertisementManager::single_image()
{
    auto files = File::list_directory(std::string(AD_DIR) + "/singles");
    std::vector<std::string> file_names;

    for (auto f : files)
    {
        if (f.second == 1) continue;

        file_names.push_back(f.first);
    }

    if (file_names.empty()) return nullptr;

    try
    {
        auto folder = File::get_file_path(std::string(AD_DIR) + "/singles/", "");
        auto file_name = file_names[Random::instance().get(0, file_names.size() - 1)];

        return std::shared_ptr<Advertisement>(new Advertisement(folder + file_name, ""));
    }
    catch (...)
    {
        std::cout << "Error loading ad" << std::endl;

        return nullptr;
    }
}
