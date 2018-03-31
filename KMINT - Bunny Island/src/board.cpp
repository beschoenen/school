#include <thread>
#include "board.h"
#include "board pieces/base/board_piece.h"
#include "drawables/base/drawable.h"
#include "board pieces/schaap.h"
#include "entities.h"

namespace kmint
{
    board::board() : _window{"KMINT - Simulation", 1280, 720}, _renderer{_window}
    {
    }

    void board::play()
    {
        bool playing = true;
        bool paused = false;
        SDL_Event e{};

        while (playing)
        {
            while (SDL_PollEvent(&e))
            {
                if (e.key.keysym.sym == SDLK_SPACE && e.type == SDL_KEYUP)
                {
                    paused = !paused;
                }

                playing = e.type != SDL_QUIT;
            }

            if (paused)
            {
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
                continue;
            }

            _renderer.clear();

            for (auto& piece : _board_pieces)
            {
                piece->update(1.f / 60.f);
                piece->get_drawable().draw(_renderer);
            }

            int index = 0;
            for (const auto& message : logger::all())
            {
                float y = 700 - (15 * index);

                _renderer.draw_text({5, y}, message, {255, 255, 255});
                index++;
            }

            _renderer.render();
        }
    }

    void board::add_board_piece(board_piece* piece)
    {
        if (typeid(*piece) == typeid(kmint::schaap))
        {
            kmint::entities::instance()->schaap = dynamic_cast<schaap*>(piece);
        }
        else if (typeid(*piece) == typeid(mister_janssen))
        {
            kmint::entities::instance()->mister_janssen = dynamic_cast<mister_janssen*>(piece);
        }
        else if (typeid(*piece) == typeid(misses_janssen))
        {
            kmint::entities::instance()->misses_janssen = dynamic_cast<misses_janssen*>(piece);
        }
        else if (typeid(*piece) == typeid(cave))
        {
            kmint::entities::instance()->cave = dynamic_cast<cave*>(piece);
        }
        else if (typeid(*piece) == typeid(rabbit_population))
        {
            kmint::entities::instance()->rabbit_population = dynamic_cast<rabbit_population*>(piece);
        }
        else if (typeid(*piece) == typeid(graph))
        {
            kmint::entities::instance()->graph = dynamic_cast<graph*>(piece);
        }

        _board_pieces.push_back(piece);
    }

    void board::remove_board_piece(board_piece* piece)
    {
        _board_pieces.erase(std::find(_board_pieces.begin(), _board_pieces.end(), piece));
    }
};
