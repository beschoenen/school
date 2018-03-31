#include "FpsText.h"

engine::FpsText::FpsText(std::string text) : Text(std::move(text), FONT, 10, 55, {255, 255, 0, 0}, 24)
{
    anchor = POS_TOP_LEFT;
    layer = HUD_LAYER;
}
