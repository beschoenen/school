#ifdef _WINDOWS
#include <Windows.h>
#else
#include <CoreFoundation/CFBundle.h>
#include <ApplicationServices/ApplicationServices.h>
#endif

#include "Advertisement.h"
#include <utility>

Advertisement::Advertisement(std::string img, std::string url) : web_url(std::move(url))
{
    image = std::shared_ptr<MenuImage>(new MenuImage(std::move(img), 0, 0, -1, -1, POS_BOTTOM_LEFT));
    image->set_layer(HUD_LAYER);

    // Button properties to set full image "clickable"
    button = std::shared_ptr<MenuButton>(
        new MenuButton("VISIT AD", std::bind(&Advertisement::open, this), 0, 0, POS_BOTTOM_LEFT)
    );

    button->set_padding(28);
}

void Advertisement::open()
{
    if(web_url.empty()) return;

#ifdef _WINDOWS
    ShellExecute(NULL, "open", web_url.c_str(), NULL, NULL, SW_SHOWNORMAL);
#else
    CFURLRef url = CFURLCreateWithBytes(
        nullptr, (UInt8*) web_url.c_str(), web_url.length(), kCFStringEncodingASCII, nullptr
    );

    LSOpenCFURLRef(url, nullptr);
    CFRelease(url);
#endif
}

void Advertisement::draw()
{
    Engine::instance()->draw(button);
    Engine::instance()->draw(image);
}

void Advertisement::erase()
{
    Engine::instance()->erase(button);
    Engine::instance()->erase(image);
}
