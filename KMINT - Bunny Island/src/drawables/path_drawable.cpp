#include "path_drawable.h"
#include "../renderer.h"

kmint::path_drawable::path_drawable(const kmint::board_piece& act, kmint::graph* graph) :
    drawable(act), _graph{graph}, _path{}
{
}

void kmint::path_drawable::set_path(std::vector<kmint::node_id> path)
{
    _path = std::move(path);
}

void kmint::path_drawable::draw(const kmint::renderer& r) const
{
    for (auto& id : _path)
    {
        auto node = _graph->operator[](id);

        point start = {-3, -3};
        point offset = {3, 3};

        r.draw_rect(node.location() + start, node.location() + offset);
    }
}
