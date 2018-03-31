#ifndef KMINT_COMBINED_DRAWABLE_H
#define KMINT_COMBINED_DRAWABLE_H


#include "drawable.h"

namespace kmint
{
    class combined_drawable : public drawable
    {
        drawable& _first;
        drawable& _second;

    public:
        combined_drawable(board_piece& board_piece, drawable& first, drawable& second);

        void draw(const renderer& r) const override;
    };
}


#endif //KMINT_COMBINED_DRAWABLE_H
