#ifndef _KMINT_GRAPH_DRAWABLE_H
#define _KMINT_GRAPH_DRAWABLE_H

#include "base/drawable.h"

namespace kmint
{
    class graph;

    class graph_drawable : public drawable
    {
        const graph& get_graph() const;

    public:
        graph_drawable(const graph& g);

        void draw(const renderer& r) const override;
    };
}

#endif
