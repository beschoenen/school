#include "combined_drawable.h"

kmint::combined_drawable::combined_drawable(kmint::board_piece& board_piece, kmint::drawable& first, kmint::drawable& second) :
    drawable{board_piece}, _first{first}, _second{second}
{
}

void kmint::combined_drawable::draw(const kmint::renderer& r) const
{
    _first.draw(r);
    _second.draw(r);
}
