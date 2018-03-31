#include <sstream>
#include "Turn.h"
#include "../Server/Socket.h"

void Turn::get_commodity() noexcept
{
    commodity = true;
    number_of_picks = 10; //too much for picking a building
}

bool Turn::did_get_commodity() const noexcept
{
    return commodity;
}

void Turn::use_ability() noexcept
{
    ability = true;
}

bool Turn::did_use_ability() const noexcept
{
    return ability;
}

void Turn::build_building() noexcept
{
    number_built_buildings++;
}

int Turn::built_buildings() const noexcept
{
    return number_built_buildings;
}

void Turn::picked_building() noexcept
{
    number_of_picks++;
    commodity = true;
}

int Turn::amount_picked() const noexcept
{
    return number_of_picks;
}

std::string Turn::build_view() noexcept
{
    int action_index = 1;
    std::stringstream view;

    view << "Pick an action:" << Socket::endl;

    view << "(" << action_index++ << ") View opponents cards and gold." << Socket::endl;
    view << "(" << action_index++ << ") Get 2 gold." << Socket::endl;
    view << "(" << action_index++ << ") Pick a building card." << Socket::endl;
    view << "(" << action_index++ << ") Build a building." << Socket::endl;
    view << "(" << action_index++ << ") Use card ability." << Socket::endl;
    view << "(" << action_index << ") End turn." << Socket::endl;

    return view.str();
}
