#include "point.h"

namespace kmint
{
    std::istream& operator>>(std::istream& is, point& p)
    {
        float x, y;
        if (is >> x && is >> y)
        {
            p.x(x);
            p.y(y);
        }

        return is;
    }
}
