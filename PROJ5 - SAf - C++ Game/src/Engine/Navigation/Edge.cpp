#include "Edge.h"

namespace engine
{
    Edge::Edge(std::shared_ptr<Node>& from, std::shared_ptr<Node>& to) : from_(from), to_(to)
    {
        set_direction();
    }

    void Edge::set_direction()
    {
        const auto sum_x = from_->get_column() - to_->get_column();
        const auto sum_y = from_->get_row() - to_->get_row();

        if (sum_x == 0 && sum_y > 0)
        {
            direction_ = DIR_UP;
        }
        else if (sum_x < 0 && sum_y > 0)
        {
            direction_ = DIR_UP_RIGHT;
        }
        else if (sum_x < 0 && sum_y == 0)
        {
            direction_ = DIR_RIGHT;
        }
        else if (sum_x < 0 && sum_y < 0)
        {
            direction_ = DIR_DOWN_RIGHT;
        }
        else if (sum_x == 0 && sum_y < 0)
        {
            direction_ = DIR_DOWN;
        }
        else if (sum_x > 0 && sum_y < 0)
        {
            direction_ = DIR_DOWN_LEFT;
        }
        else if (sum_x > 0 && sum_y == 0)
        {
            direction_ = DIR_LEFT;
        }
        else if (sum_x > 0 && sum_y > 0)
        {
            direction_ = DIR_UP_LEFT;
        }
    }

    std::shared_ptr<Node> Edge::get_from_node() const
    {
        return from_;
    }

    std::shared_ptr<Node> Edge::get_to_node() const
    {
        return to_;
    }

    Direction Edge::get_direction() const
    {
        return direction_;
    }

    bool Edge::compare(const std::shared_ptr<Node>& edge_node, const Node& right)
    {
        return edge_node->get_row() == right.get_row() && edge_node->get_column() == right.get_column();
    }

    bool Edge::compare(const std::shared_ptr<Node>& edge_node, const level::GridTile& right)
    {
        return edge_node->get_row() == right.get_row() && edge_node->get_column() == right.get_column();
    }

    bool operator==(const std::shared_ptr<Edge>& left, const std::shared_ptr<Node>& right)
    {
        return Edge::compare(left->get_from_node(), *right) || Edge::compare(left->get_to_node(), *right);
    }

    bool operator==(const std::shared_ptr<Edge>& left, const Node& right)
    {
        return Edge::compare(left->get_from_node(), right) || Edge::compare(left->get_to_node(), right);
    }

    bool operator==(const std::shared_ptr<Edge>& left, const std::shared_ptr<level::GridTile>& right)
    {
        return Edge::compare(left->get_from_node(), *right) || Edge::compare(left->get_to_node(), *right);
    }

    bool operator==(const std::shared_ptr<Edge>& left, const level::GridTile& right)
    {
        return Edge::compare(left->get_from_node(), right) || Edge::compare(left->get_to_node(), right);
    }

    bool operator!=(const std::shared_ptr<Edge>& left, const std::shared_ptr<Node>& right)
    {
        return Edge::compare(left->get_from_node(), *right) || Edge::compare(left->get_to_node(), *right);
    }

    bool operator!=(const std::shared_ptr<Edge>& left, const Node& right)
    {
        return Edge::compare(left->get_from_node(), right) || Edge::compare(left->get_to_node(), right);
    }

    bool operator!=(const std::shared_ptr<Edge>& left, const std::shared_ptr<level::GridTile>& right)
    {
        return Edge::compare(left->get_from_node(), *right) || Edge::compare(left->get_to_node(), *right);
    }

    bool operator!=(const std::shared_ptr<Edge>& left, const level::GridTile& right)
    {
        return Edge::compare(left->get_from_node(), right) || Edge::compare(left->get_to_node(), right);
    }
}
