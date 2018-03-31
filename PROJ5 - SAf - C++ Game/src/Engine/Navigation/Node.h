#ifndef NAV_NODE_H
#define NAV_NODE_H

#include "../../Level/Tiles/GridTile.h"
#include <memory>

namespace engine
{
    class Node
    {
        double h_cost_ = 0;
        double g_cost_ = 0;
        int row_ = 0;
        int column_ = 0;
        bool accessible_ = false;

        static bool compare_and(const std::shared_ptr<Node>& left, const Node& right);

        static bool compare_or(const std::shared_ptr<Node>& left, const Node& right);

        static bool compare_and(const std::shared_ptr<Node>& left, const level::GridTile& right);

        static bool compare_or(const std::shared_ptr<Node>& left, const level::GridTile& right);

    public:
        Node(int row, int column, bool accessible);

        void set_g_cost(double gcost);

        void set_h_cost(double hcost);

        double get_g_cost() const;

        double get_h_cost() const;

        double get_f_score() const;

        bool get_walkable() const;

        int get_row() const;

        int get_column() const;

        friend bool operator==(const std::shared_ptr<Node>& left, const std::shared_ptr<Node>& right);

        friend bool operator==(const std::shared_ptr<Node>& left, const Node& right);

        friend bool operator==(const std::shared_ptr<Node>& left, const std::shared_ptr<level::GridTile>& right);

        friend bool operator==(const std::shared_ptr<Node>& left, const level::GridTile& right);

        friend bool operator!=(const std::shared_ptr<Node>& left, const std::shared_ptr<Node>& right);

        friend bool operator!=(const std::shared_ptr<Node>& left, const Node& right);

        friend bool operator!=(const std::shared_ptr<Node>& left, const std::shared_ptr<level::GridTile>& right);

        friend bool operator!=(const std::shared_ptr<Node>& left, const level::GridTile& right);
    };
}

#endif //NAV_NODE_H
