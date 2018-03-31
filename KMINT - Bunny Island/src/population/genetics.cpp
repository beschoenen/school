#include "genetics.h"
#include "../random.h"

#define MUTATE_PERCENTAGE 5

namespace kmint
{
    genetics::genetics(rabbit* rabbit) : _base_rabbit(rabbit)
    {
        int gender_value = random::instance().get(0, 99) <= 49 ? 0 : 1;
        _gender = static_cast<gender>(gender_value);
    }

    bool genetics::generate(rabbit & father, rabbit & mother)
    {
        crossover(father, mother);
        return mutate(father, mother);
    }

    void genetics::generate()
    {
        _base_rabbit->attraction_to_schaap(random::instance().getf(-1, 1));
        _base_rabbit->attraction_to_water(random::instance().getf(-1, 1));
        _base_rabbit->cohesion(random::instance().getf(1));
        _base_rabbit->separation(random::instance().getf(1));
        _base_rabbit->alignment(random::instance().getf(1));
    }

    void genetics::crossover(rabbit & father, rabbit & mother)
    {
        float father_chromosome[] = {father.attraction_to_schaap(), father.attraction_to_water(), father.cohesion(), father.separation(), father.alignment()};
        float mother_chromosome[] = {mother.attraction_to_schaap(), mother.attraction_to_water(), mother.cohesion(), mother.separation(), mother.alignment()};
        std::vector<float> new_chromosomes;

        auto sep = random::instance().get(1, 4);

        auto current = father_chromosome;

        for (int i = 0; i < 5; i++)
        {
            if (i >= sep) current = mother_chromosome;

            new_chromosomes.push_back(current[i]);
        }

        _base_rabbit->set_chromesomes(new_chromosomes);
    }

    bool genetics::mutate(rabbit & father, rabbit & mother)
    {
        const auto must_mutate = random::instance().get(100);
        if (must_mutate >= MUTATE_PERCENTAGE) return false;

        const auto mutation_kind = random::instance().get(100);
        if (mutation_kind <= 20)
        {
            _base_rabbit->attraction_to_schaap(random::instance().getf(-1, 1));
        }
        else if (mutation_kind <= 40)
        {
            _base_rabbit->attraction_to_water(random::instance().getf(-1, 1));
        }
        else if (mutation_kind <= 60)
        {
            _base_rabbit->cohesion(random::instance().getf(1));
        }
        else if (mutation_kind <= 80)
        {
            _base_rabbit->separation(random::instance().getf(1));
        }
        else if (mutation_kind <= 100)
        {
            _base_rabbit->alignment(random::instance().getf(1));
        }
        return true;
    }

    gender genetics::get_gender()
    {
        return _gender;
    }

    int genetics::get_generations_alive()
    {
        return _generations_alive;
    }

    void genetics::increase_generation_alive()
    {
        _generations_alive++;
    }

    float genetics::get_time_alive()
    {
        return _base_rabbit->time_alive();
    }
}
