#include <iostream>
#include "src/internal/base_systems.h"
#include "src/board.h"
#include "src/graph.h"
#include "src/board pieces/base/graph_bound_board_piece.h"
#include "src/board pieces/base/person.h"
#include "src/board pieces/mister_janssen.h"
#include "src/board pieces/misses_janssen.h"
#include "src/board pieces/schaap.h"
#include "src/board pieces/rabbit.h"
#include "src/board pieces/cave.h"
#include "src/logger.h"
#include "src/population/rabbit_population.h"
#include <fstream>

int main(int argc, char* args[])
{
    try
    {
        // Setup
        kmint::base_systems bs{};
        kmint::board board{};

        auto* graph = new kmint::graph{{0.0f, 0.0f}};
        board.add_board_piece(graph);

        // Load graph
        std::ifstream file{"graph.txt"};
        graph->fill(file);

        // Load board pieces
        auto cave = new kmint::cave{graph};
        board.add_board_piece(cave);

        auto mister_janssen = new kmint::mister_janssen{graph};
        board.add_board_piece(mister_janssen);

        auto misses_janssen = new kmint::misses_janssen{graph};
        board.add_board_piece(misses_janssen);

        auto schaap = new kmint::schaap{graph};
        board.add_board_piece(schaap);

        auto rabbit_population = new kmint::rabbit_population();
        board.add_board_piece(rabbit_population);

        // Start the simulation
        board.play();
    }
    catch (const std::runtime_error& e)
    {
        std::cerr << "Runtime error: " << e.what() << "\n";
        return 1;
    }

    return 0;
}
