#ifndef SDL_PROJECT_TILE_H
#define SDL_PROJECT_TILE_H

#include <memory>

namespace level
{
    class Tile
    {
    protected:
        int row_index_;
        int column_index_;

        std::weak_ptr<Tile> north_;
        std::weak_ptr<Tile> east_;
        std::weak_ptr<Tile> south_;
        std::weak_ptr<Tile> west_;

        bool accessible_from_north_;
        bool accessible_from_east_;
        bool accessible_from_south_;
        bool accessible_from_west_;

    public:
        Tile(int row_index, int column_index);

        virtual ~Tile() = default;

        int get_row() const;

        int get_column() const;

        std::weak_ptr<Tile> get_north() const;

        std::weak_ptr<Tile> get_east() const;

        std::weak_ptr<Tile> get_south() const;

        std::weak_ptr<Tile> get_west() const;

        void set_north(std::shared_ptr<Tile> north);

        void set_east(std::shared_ptr<Tile> east);

        void set_south(std::shared_ptr<Tile> south);

        void set_west(std::shared_ptr<Tile> west);

        bool get_accessible_from_north() const;

        bool get_accessible_from_east() const;

        bool get_accessible_from_south() const;

        bool get_accessible_from_west() const;

        void set_accessible_from_north(bool is_accessible);

        void set_accessible_from_east(bool is_accessible);

        void set_accessible_from_south(bool is_accessible);

        void set_accessible_from_west(bool is_accessible);
    };
}

#endif //SDL_PROJECT_TILE_H
