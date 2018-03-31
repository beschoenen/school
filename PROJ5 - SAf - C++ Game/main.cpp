#include "src/Engine/Engine.h"
#include "src/Scenes/MainMenu.h"

int main()
{
    auto game = Engine::instance();

    game->go_to_scene<scenes::MainMenu>();

    while (game->get_is_running())
    {
        if (!game->input()) break;

        game->update();

        game->render();
    }

    delete game;

    return 0;
}
