#include "rabbit_population.h"
#include "../random.h"
#include "../entities.h"
#include <iostream>

#define NUMBER_OF_RABBIT_SPAWNS 100

namespace kmint
{
    rabbit_population::rabbit_population() : static_board_piece({0, 0}, 0, {0, 0, 0}), _drawable{*this}
    {
        generate_population();
    }

    void rabbit_population::update(float delta_time)
    {
        for (auto& rabbit : individuals)
        {
            if (rabbit->state() != alive) continue;

            rabbit->update(delta_time);
        }
    }

    void rabbit_population::remove_rabbits()
    {
        std::vector<rabbit*> alive_rabbits;
        for (auto bunny : individuals)
        {
            if (bunny->state() == alive)
            {
                alive_rabbits.push_back(new rabbit(*bunny));
            }
            else
            {
                delete bunny;
            }
        }
        individuals.clear();

        individuals = alive_rabbits;
    }

    void rabbit_population::select_parents()
    {
        rabbit* temp_father = rabbit::null_object();
        rabbit* temp_mother = rabbit::null_object();

        for (auto& rabbit : individuals)
        {
            if (rabbit->state() != alive) continue;

            auto genetics = rabbit->get_genetics();

            if (genetics.get_gender() == male)
            {
                auto fgenetics = temp_father->get_genetics();

                if (genetics.get_generations_alive() > fgenetics.get_generations_alive()
                    && genetics.get_time_alive() > fgenetics.get_time_alive())
                {
                    temp_father = rabbit;
                }
            }
            else
            {
                auto mgenetics = temp_mother->get_genetics();

                if (genetics.get_generations_alive() > mgenetics.get_generations_alive()
                    && genetics.get_time_alive() > mgenetics.get_time_alive())
                {
                    temp_mother = rabbit;
                }
            }
        }

        delete father;
        father = new rabbit{*temp_father};
        delete mother;
        mother = new rabbit{*temp_mother};
    }

    void rabbit_population::generate_population()
    {
        const auto spawn_numbers = int(NUMBER_OF_RABBIT_SPAWNS - individuals.size());
        for (int i = 0; i < spawn_numbers; ++i)
        {
            auto rabbit = generate_rabbit();

            individuals.push_back(rabbit);
        }

        std::cout << "Spawned population" << std::endl;
        logger::log("Spawned rabbit gen. " + std::to_string(generations) + " of " + std::to_string(spawn_numbers) + " new rabbits");
        logger::log(std::to_string(amount_mutated) + " rabbits mutated this generation.");
        amount_mutated = 0;
        generations++;
    }

    void rabbit_population::reproduce_offspring()
    {
        remove_rabbits();
        select_parents();
        generate_population();
    }

    rabbit* rabbit_population::generate_rabbit()
    {
        const auto loc = get_new_location();
        auto temp_rabbit = new rabbit{loc};
        const auto nullobj = rabbit::null_object();

        if (*father == *nullobj && *mother == *nullobj)
        {
            temp_rabbit->get_genetics().generate();
            return temp_rabbit;
        }

        if (*father == *nullobj)
        {
            father = new rabbit{loc};
            father->get_genetics().generate();
        }

        if (*mother == *nullobj)
        {
            mother = new rabbit{loc};
            mother->get_genetics().generate();
        }

        auto mutated = temp_rabbit->get_genetics().generate(*father, *mother);
        if (mutated) amount_mutated++;
        temp_rabbit->get_genetics()._generations_alive++;

        delete nullobj;
        return temp_rabbit;
    }

    point rabbit_population::get_new_location()
    {
        const auto graph = entities::instance()->graph;
        point random_location;
        bool unique = false;

        while (!unique)
        {
            unique = true;
            random_location = graph->operator[](random::instance().get(graph->num_nodes() - 1)).location();

            for (auto rabbit : individuals)
            {
                if (random_location.to_node() == rabbit->location().to_node())
                {
                    unique = false;
                    break;
                }
            }
        }

        return random_location;
    }

    int rabbit_population::get_generations() const
    {
        return generations;
    }
}
