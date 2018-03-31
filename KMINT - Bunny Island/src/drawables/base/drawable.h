#ifndef _KMINT_DRAWABLE_H
#define _KMINT_DRAWABLE_H


namespace kmint
{
    class renderer;

    class board_piece;

    class drawable
    {
        const board_piece* _board_piece;

    protected:
        const board_piece& get_board_piece() const { return *_board_piece; }

    public:
        explicit drawable(const board_piece& act);

        virtual ~drawable() = default;

        virtual void draw(const renderer& r) const = 0;

        // remove copy and move operators and constructors
        drawable(const drawable&) = delete;

        drawable(drawable&&) = delete;

        drawable& operator=(const drawable&) = delete;

        drawable& operator=(drawable&&) = delete;
    };
}

#endif
