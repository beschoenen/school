#ifndef KMINT_PATH_DRAWABLE_H
#define KMINT_PATH_DRAWABLE_H


#include "base/drawable.h"
#include "../graph.h"

namespace kmint
{
    class path_drawable : public drawable
    {
        std::vector<node_id> _path;
        kmint::graph* _graph;

    public:
        explicit path_drawable(const board_piece& act, kmint::graph* graph);

        void draw(const renderer& r) const override;

        void set_path(std::vector<node_id> path);
    };
}


#endif //KMINT_PATH_DRAWABLE_H
