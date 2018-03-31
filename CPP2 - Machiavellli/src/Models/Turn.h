#ifndef MACHIAVELLI_TURN_H
#define MACHIAVELLI_TURN_H

#include "../Server/ClientCommand.h"

class Turn
{
    bool ability = false;
    bool commodity = false;
    int number_built_buildings = 0;
    int number_of_picks = 0;

public:
    void get_commodity() noexcept;

    bool did_get_commodity() const noexcept;

    void use_ability() noexcept;

    bool did_use_ability() const noexcept;

    void build_building() noexcept;

    int built_buildings() const noexcept;

    void picked_building() noexcept;

    int amount_picked() const noexcept;

    static std::string build_view() noexcept;
};

#endif //MACHIAVELLI_TURN_H
