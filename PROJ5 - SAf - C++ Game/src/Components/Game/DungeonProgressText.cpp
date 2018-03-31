#include "DungeonProgressText.h"

DungeonProgressText::DungeonProgressText(std::string text) :
    Text(std::move(text), FONT, 0, 150, {255, 255, 255, 255}, 63)
{
    anchor = POS_TOP_CENTER;
    layer = HUD_LAYER;
}
