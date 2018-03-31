#ifndef GAME_ADVERTISEMENTMANAGER_H
#define GAME_ADVERTISEMENTMANAGER_H


#include <string>
#include <unordered_map>
#include <vector>
#include "../../Components/Menu/Advertisement.h"

class AdvertisementManager
{
public:
    static void init();

    static std::shared_ptr<Advertisement> pick_one();

    static std::shared_ptr<Advertisement> single_image();
};


#endif //GAME_ADVERTISEMENTMANAGER_H
