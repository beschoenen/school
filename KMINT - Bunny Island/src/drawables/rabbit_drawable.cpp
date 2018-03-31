#include "rabbit_drawable.h"
#include "../population/rabbit_population.h"

namespace kmint
{
    const rabbit_population& rabbit_drawable::get_population() const
    {
        return dynamic_cast<const rabbit_population&>(get_board_piece());
    }

    rabbit_drawable::rabbit_drawable(const rabbit_population& population) : drawable(population)
    {
    }

    void rabbit_drawable::draw(const renderer& r) const
    {
        std::vector<rabbit*> rabbits = get_population().get_individuals();

        for (auto& rabbit : rabbits)
        {
            if (rabbit->state() != alive) continue;

            rabbit->get_drawable().draw(r);
        }
    }
}
