#include "rect_drawable.h"
#include "../board pieces/base/board_piece.h"
#include "../renderer.h"

kmint::rect_drawable::rect_drawable(const kmint::board_piece& act, int size, kmint::color color) :
    rect_drawable{act, size, size, color}
{
}

kmint::rect_drawable::rect_drawable(const kmint::board_piece& act, int width, int height, kmint::color color) :
    drawable{act}, _width{width}, _height{height}, _color{color}
{
}

void kmint::rect_drawable::draw(const kmint::renderer& r) const
{
    r.set_color(_color);

    auto top_left = get_board_piece().location();
    auto bottom_right = point{top_left};

    top_left.x(top_left.x() - (_width / 2));
    top_left.y(top_left.y() - (_height / 2));

    bottom_right.x(bottom_right.x() + (_width / 2));
    bottom_right.y(bottom_right.y() + (_height / 2));

    r.draw_rect(top_left, bottom_right);
}
