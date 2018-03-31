#include "RoadTile.h"
#include "../../Utilities/Random.h"
#include "CampTile.h"

level::RoadTile::RoadTile(int row_index, int column_index) : FloorTile(row_index, column_index)
{
    visited_ = false;

    accessible_from_north_ = false;
    accessible_from_east_ = false;
    accessible_from_south_ = false;
    accessible_from_west_ = false;
}

bool level::RoadTile::has_any_remaining_sideways() const
{
    return check_if_path_can_be_created(north_, false) ||
           check_if_path_can_be_created(east_, false) ||
           check_if_path_can_be_created(south_, false) ||
           check_if_path_can_be_created(west_, false);
}

bool level::RoadTile::check_if_path_can_be_created(std::weak_ptr<Tile> room, bool is_visited) const
{
    if (room.expired()) return false;

    if (auto tile = std::dynamic_pointer_cast<level::RoadTile>(room.lock()))
    {
        if (std::dynamic_pointer_cast<CampTile>(tile) == nullptr && tile->get_visited() == is_visited) return true;
    }

    return false;
}

void level::RoadTile::generate_path()
{
    visited_ = true;

    while (has_any_remaining_sideways())
    {
        bool is_creating = true;
        while (is_creating)
        {
            // 1 = North, 2 = East, 3 = South, 4 = West
            const int side = Random::instance().get(1, 4);
            is_creating = create_passage(side, false);
        }
    }
}

void level::RoadTile::create_extra_passage()
{
    const int side = Random::instance().get(1, 4);
    create_passage(side, true);
}

bool level::RoadTile::create_passage(int side, bool is_visited)
{
    bool is_creating = true;

    std::shared_ptr<RoadTile> tile = nullptr;
    switch (side)
    {
        case 1:
            if (get_north().expired()) break;
            if (!check_if_path_can_be_created(get_north().lock(), is_visited)) break;

            is_creating = false;
            this->set_accessible_from_north(true);
            tile = std::dynamic_pointer_cast<RoadTile>(this->get_north().lock());
            tile->set_accessible_from_south(true);
            tile->generate_path();
            break;
        case 2:
            if (get_east().expired()) break;
            if (!check_if_path_can_be_created(get_east().lock(), is_visited)) break;

            is_creating = false;
            this->set_accessible_from_east(true);
            tile = std::dynamic_pointer_cast<RoadTile>(this->get_east().lock());
            tile->set_accessible_from_west(true);
            tile->generate_path();
            break;
        case 3:
            if (get_south().expired()) break;
            if (!check_if_path_can_be_created(get_south().lock(), is_visited)) break;

            is_creating = false;
            this->set_accessible_from_south(true);
            tile = std::dynamic_pointer_cast<RoadTile>(this->get_south().lock());
            tile->set_accessible_from_north(true);
            tile->generate_path();
            break;
        case 4:
            if (get_west().expired()) break;
            if (!check_if_path_can_be_created(get_west().lock(), is_visited)) break;

            is_creating = false;
            this->set_accessible_from_west(true);
            tile = std::dynamic_pointer_cast<RoadTile>(this->get_west().lock());
            tile->set_accessible_from_east(true);
            tile->generate_path();
            break;
        default:
            break;
    }

    return is_creating;
}
