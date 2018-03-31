#ifndef GAME_ADVERTISEMENT_H
#define GAME_ADVERTISEMENT_H

#include "../../defines.h"
#include "../../Engine/Engine.h"
#include "../../Components/Menu/MenuImage.h"
#include "../../Components/Menu/MenuButton.h"

class Advertisement
{
    std::shared_ptr<MenuImage> image;
    std::shared_ptr<MenuButton> button;
    std::string web_url;

public:
    Advertisement(std::string image, std::string url);

    void open();

    void draw();

    void erase();
};


#endif //GAME_ADVERTISEMENT_H
