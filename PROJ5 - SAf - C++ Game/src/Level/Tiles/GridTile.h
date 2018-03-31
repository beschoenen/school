#ifndef SDL_PROJECT_GRID_TILE_H
#define SDL_PROJECT_GRID_TILE_H

#include <memory>

enum GridTileTypes
{
    wall, ground, camp, campfire, portal
};

namespace level
{
    class GridTile
    {
        int row_index_;
        int column_index_;
        GridTileTypes grid_tile_type_;

        std::weak_ptr<GridTile> north_;
        std::weak_ptr<GridTile> east_;
        std::weak_ptr<GridTile> south_;
        std::weak_ptr<GridTile> west_;
    public:
        explicit GridTile(GridTileTypes grid_tile_type);

        ~GridTile() = default;

        int get_row() const;

        void set_row(int row);

        int get_column() const;

        void set_column(int column);

        GridTileTypes get_grid_tile_type() const;

        void set_grid_tile_type(GridTileTypes grid_tile_type);

        bool get_accessible() const;

        std::weak_ptr<GridTile> get_north() const;

        std::weak_ptr<GridTile> get_east() const;

        std::weak_ptr<GridTile> get_south() const;

        std::weak_ptr<GridTile> get_west() const;

        void set_north(std::shared_ptr<GridTile> north);

        void set_east(std::shared_ptr<GridTile> east);

        void set_south(std::shared_ptr<GridTile> south);

        void set_west(std::shared_ptr<GridTile> west);
    };
}

#endif //SDL_PROJECT_GRID_TILE_H
