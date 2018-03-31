#include "drawable.h"
#include "../../board pieces/base/board_piece.h"

namespace kmint
{
    drawable::drawable(const board_piece& act) : _board_piece{&act}
    {
    }
}
