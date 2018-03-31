#include "src/Framework/Engine.h"
#include "src/Controllers/GameController.h"

int main()
{
    auto engine = Engine();

    GameController test;

    engine.add_controller(&test);

    engine.start();

    return 0;
}
