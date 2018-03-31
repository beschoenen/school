#ifndef KMINT_RECT_DRAWABLE_H
#define KMINT_RECT_DRAWABLE_H


#include "base/drawable.h"
#include "../models/color.h"

namespace kmint
{
    class rect_drawable : public drawable
    {
        int _width;
        int _height;

        kmint::color _color;

    public:
        rect_drawable(const board_piece& act, int size, kmint::color color);

        rect_drawable(const board_piece& act, int width, int height, kmint::color color);

        void draw(const renderer& r) const override;
    };
}


#endif //KMINT_RECT_DRAWABLE_H
