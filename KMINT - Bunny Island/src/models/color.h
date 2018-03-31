#ifndef _KMINT_COLOR_H
#define _KMINT_COLOR_H

#include <cstdint>

namespace kmint
{
    using component = std::uint8_t;

    class color
    {
        component _r, _g, _b, _a;

    public:
        color(component r, component g, component b, component a = 0xff) : _r{r}, _g{g}, _b{b}, _a{a} {}

        component r() const noexcept { return _r; }

        component g() const noexcept { return _g; }

        component b() const noexcept { return _b; }

        component a() const noexcept { return _a; }
    };
}

#endif
