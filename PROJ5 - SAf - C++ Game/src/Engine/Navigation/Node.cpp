#include "Node.h"

namespace engine
{
    Node::Node(const int row, const int column, const bool accessible) : row_(row), column_(column), accessible_(accessible)
    {
    }

    void Node::set_g_cost(const double gcost)
    {
        g_cost_ = gcost;
    }

    void Node::set_h_cost(const double hcost)
    {
        h_cost_ = hcost;
    }

    double Node::get_g_cost() const
    {
        return g_cost_;
    }

    double Node::get_h_cost() const
    {
        return h_cost_;
    }

    double Node::get_f_score() const
    {
        return get_g_cost() + get_h_cost();
    }

    bool Node::get_walkable() const
    {
        return accessible_;
    }

    int Node::get_row() const
    {
        return row_;
    }

    int Node::get_column() const
    {
        return column_;
    }

    bool Node::compare_and(const std::shared_ptr<Node>& left, const Node& right)
    {
        return left->get_row() == right.get_row() && left->get_column() == right.get_column();
    }

    bool Node::compare_or(const std::shared_ptr<Node>& left, const Node& right)
    {
        return left->get_row() != right.get_row() || left->get_column() != right.get_column();
    }

    bool Node::compare_and(const std::shared_ptr<Node>& left, const level::GridTile& right)
    {
        return left->get_row() == right.get_row() && left->get_column() == right.get_column();
    }

    bool Node::compare_or(const std::shared_ptr<Node>& left, const level::GridTile& right)
    {
        return left->get_row() != right.get_row() || left->get_column() != right.get_column();
    }

    bool operator==(const std::shared_ptr<Node>& left, const std::shared_ptr<Node>& right)
    {
        return Node::compare_and(left, *right);
    }

    bool operator==(const std::shared_ptr<Node>& left, const Node& right)
    {
        return Node::compare_and(left, right);
    }

    bool operator==(const std::shared_ptr<Node>& left, const std::shared_ptr<level::GridTile>& right)
    {
        return Node::compare_and(left, *right);
    }

    bool operator==(const std::shared_ptr<Node>& left, const level::GridTile& right)
    {
        return Node::compare_and(left, right);
    }

    bool operator!=(const std::shared_ptr<Node>& left, const std::shared_ptr<Node>& right)
    {
        return Node::compare_or(left, *right);
    }

    bool operator!=(const std::shared_ptr<Node>& left, const Node& right)
    {
        return Node::compare_or(left, right);
    }

    bool operator!=(const std::shared_ptr<Node>& left, const std::shared_ptr<level::GridTile>& right)
    {
        return Node::compare_or(left, *right);
    }

    bool operator!=(const std::shared_ptr<Node>& left, const level::GridTile& right)
    {
        return Node::compare_or(left, right);
    }
}
