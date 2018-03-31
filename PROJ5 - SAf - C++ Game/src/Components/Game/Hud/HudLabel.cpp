#include "HudLabel.h"

HudLabel::HudLabel(const std::string& string, int x, int y, Position anchor) :
    Text(string, FONT, x, y, {255, 255, 255, 0}, 24)
{
    this->anchor = anchor;
    layer = HUD_LAYER;
}
