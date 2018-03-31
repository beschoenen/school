#include "graph_drawable.h"
#include "../board pieces/base/board_piece.h"
#include "../graph.h"
#include "../renderer.h"

namespace kmint
{
    constexpr float node_width = 20.0f;
    constexpr float node_half_width = node_width / 2.0f;

    const graph& graph_drawable::get_graph() const
    {
        return dynamic_cast<const graph&>(get_board_piece());
    }

    graph_drawable::graph_drawable(const graph& g) : drawable(g)
    {
    }

    void graph_drawable::draw(const renderer& r) const
    {
        auto graph_location = get_graph().location();

        // draw nodes
        r.set_color(color {0x03, 0xC1, 0x01});
        for (const auto& node : get_graph())
        {
            auto x = graph_location.x() + node.location().x();
            auto y = graph_location.y() + node.location().y();

            r.draw_rect({x - node_half_width, y - node_half_width}, {x + node_half_width, y + node_half_width});
        }

        // draw edges
        r.set_color(color {0x00, 0x00, 0x00});
        for (const auto& node : get_graph())
        {
            auto x = graph_location.x() + node.location().x();
            auto y = graph_location.y() + node.location().y();

            for (auto& edge : node)
            {
                auto to_x = graph_location.x() + edge.to().location().x();
                auto to_y = graph_location.y() + edge.to().location().y();

                r.draw_line({x, y}, {to_x, to_y});
            }
        }
    }
}
