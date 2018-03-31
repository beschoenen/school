#ifndef KMINT_RABBIT_DRAWABLE_H
#define KMINT_RABBIT_DRAWABLE_H

#include "base/drawable.h"

namespace kmint
{
    class rabbit_population;

    class rabbit_drawable : public drawable
    {
        const rabbit_population& get_population() const;

    public:
        rabbit_drawable(const rabbit_population& population);

        void draw(const renderer& r) const override;
    };
}


#endif //KMINT_RABBIT_DRAWABLE_H
