#ifndef NAV_EDGE_H
#define NAV_EDGE_H

#include <memory>
#include "../../defines_navigation.h"
#include "Node.h"

namespace engine
{
    class Edge
    {
        std::shared_ptr<Node> from_;
        std::shared_ptr<Node> to_;
        Direction direction_;

        void set_direction();

        static bool compare(const std::shared_ptr<Node>& edge_node, const Node& right);

        static bool compare(const std::shared_ptr<Node>& edge_node, const level::GridTile& right);

    public:
        Edge(std::shared_ptr<Node>& from, std::shared_ptr<Node>& to);

        std::shared_ptr<Node> get_from_node() const;

        std::shared_ptr<Node> get_to_node() const;

        Direction get_direction() const;

        friend bool operator==(const std::shared_ptr<Edge>& left, const std::shared_ptr<Node>& right);

        friend bool operator==(const std::shared_ptr<Edge>& left, const Node& right);

        friend bool operator==(const std::shared_ptr<Edge>& left, const std::shared_ptr<level::GridTile>& right);

        friend bool operator==(const std::shared_ptr<Edge>& left, const level::GridTile& right);

        friend bool operator!=(const std::shared_ptr<Edge>& left, const std::shared_ptr<Node>& right);

        friend bool operator!=(const std::shared_ptr<Edge>& left, const Node& right);

        friend bool operator!=(const std::shared_ptr<Edge>& left, const std::shared_ptr<level::GridTile>& right);

        friend bool operator!=(const std::shared_ptr<Edge>& left, const level::GridTile& right);
    };
}

#endif //NAV_EDGE_H
