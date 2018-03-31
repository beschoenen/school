#include "AStar.h"
#include "../Engine.h"

namespace engine
{
    int Astar::search_range_;

    std::shared_ptr<Node> Astar::start_node_;
    std::shared_ptr<Node> Astar::end_node_;
    std::shared_ptr<Node> Astar::selected_node_;
    std::shared_ptr<Node> Astar::previous_node_;
    std::vector<std::shared_ptr<Node>> Astar::open_list_;
    std::vector<std::shared_ptr<Edge>> Astar::closed_list_;
    std::vector<std::shared_ptr<NodeTile>> Astar::debug_list_;
    std::stack<Direction> Astar::direction_list_;

    std::tuple<std::stack<Direction>, bool> Astar::find_path(std::shared_ptr<level::GridTile>& start, std::shared_ptr<level::GridTile>& end, const int search_range)
    {
        start_node_ = create_node(start);
        previous_node_ = start_node_;
        end_node_ = create_node(end);
        search_range_ = search_range;

        search_for_endpoint();
        backtrack_to_startpoint();
        bool found_endpoint = can_reach_destination();

        auto tuple = std::make_tuple(direction_list_, found_endpoint);

        debugger();
        cleanup();

        return tuple;
    }

    void Astar::search_for_endpoint()
    {
        selected_node_ = start_node_;
        add_to_open(selected_node_);

        while (selected_node_ != end_node_ && closed_list_.size() < search_range_)
        {
            add_to_closed(selected_node_);

            get_surrounding_nodes();

            select_node();
        }
    }

    void Astar::backtrack_to_startpoint()
    {
        for (auto i = closed_list_.rbegin(); i != closed_list_.rend(); ++i)
        {
            direction_list_.push(i->get()->get_direction());
        }
    }

    bool Astar::can_reach_destination()
    {
        return selected_node_ == end_node_;
    }

    void Astar::cleanup()
    {
        open_list_.clear();
        closed_list_.clear();
        direction_list_ = std::stack<Direction>();

        start_node_ = nullptr;
        end_node_ = nullptr;
        selected_node_ = nullptr;
        previous_node_ = nullptr;
    }

    void Astar::get_surrounding_nodes()
    {
        auto levelmatrix = Engine::instance()->get_map()->get_map();
        const auto gridtile = levelmatrix.at(selected_node_->get_row()).at(selected_node_->get_column());

        auto positions = std::vector<std::shared_ptr<Node>>{
            create_node(gridtile->get_north().lock()),
            create_node(gridtile->get_west().lock()),
            //current position
            create_node(gridtile->get_east().lock()),
            create_node(gridtile->get_south().lock())
        };

        for (auto& node : positions)
        {
            if (node->get_walkable())
            {
                add_to_open(node);
            }
        }
    }

    void Astar::select_node()
    {
        auto lowest_score = std::numeric_limits<double>::max();
        std::shared_ptr<Node> selected = nullptr;

        for (const auto& tile : open_list_)
        {
            if (tile == end_node_)
            {
                selected = tile;
                break;
            }

            if (tile->get_f_score() < lowest_score)
            {
                lowest_score = tile->get_f_score();
                selected = tile;
            }
        }

        selected_node_ = selected;
    }

    std::shared_ptr<Node> Astar::create_node(const std::shared_ptr<level::GridTile> gridtile)
    {
        auto node = std::make_shared<Node>(gridtile->get_row(), gridtile->get_column(), gridtile->get_accessible());
        if (end_node_ == nullptr) return node;

        node->set_g_cost(NODE_MOVEMENT_COST);
        node->set_h_cost(heuristic(node));
        return node;
    }

    double Astar::heuristic(std::shared_ptr<Node>& current_tile)
    {
        const int sum_x = abs(end_node_->get_column() - current_tile->get_column());
        const int sum_y = abs(end_node_->get_row() - current_tile->get_row());

        //Manhattan
        //    return sum_x + sum_y;

        //Euclidean
        const auto weight = 2.00;
        const auto euclidean = sqrt(pow(sum_x, 2) + pow(sum_y, 2));
        const auto weighted = weight * euclidean;        
        
        return weighted;
    }

    bool Astar::add_to_open(std::shared_ptr<Node>& open_node)
    {
        const auto in_open = std::find(open_list_.begin(), open_list_.end(), open_node);
        const auto in_closed = std::find(closed_list_.begin(), closed_list_.end(), open_node);

        if (in_open != open_list_.end())
        {
            *in_open = open_node;
        }
        else if (in_closed == closed_list_.end())
        {
            open_list_.push_back(open_node);
        }

        return true;
    }

    bool Astar::add_to_closed(std::shared_ptr<Node>& closed_node)
    {
        open_list_.erase(std::find(open_list_.begin(), open_list_.end(), closed_node));

        if (previous_node_ == closed_node) return false;

        closed_list_.push_back(std::make_shared<Edge>(previous_node_, closed_node));

        return true;
    }

    void Astar::debugger()
    {
        if (ASTAR_DEBUG)
        {
            destroy_debug();
            create_debug();
        }
    }

    void Astar::create_debug()
    {
        for (auto& node : open_list_)
        {
            debug_list_.push_back(std::make_shared<NodeTile>(node));
        }

        int index = 1;
        for (const auto& edge : closed_list_)
        {
            bool final = false;
            if (index == 1) final = true;

            debug_list_.push_back(std::make_shared<NodeTile>(edge->get_to_node(), true, final, index));
            index++;
        }

        for (auto& tile : debug_list_)
        {
            Engine::instance()->draw(tile);
        }
    }

    void Astar::destroy_debug()
    {
        for (auto& tile : debug_list_)
        {
            Engine::instance()->erase(tile);
        }
        debug_list_.clear();
    }
}
