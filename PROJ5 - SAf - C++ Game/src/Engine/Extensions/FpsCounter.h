#ifndef GAME_FPSCOUNTER_H
#define GAME_FPSCOUNTER_H

#include <SDL_quit.h>
#include "FpsText.h"

#define FRAME_VALUES 10
#define TEXT_PREFIX "FPS: "

namespace engine
{
    class FpsCounter
    {
        std::shared_ptr<FpsText> text_display = nullptr;

        bool visible = false;
        int frame_times[FRAME_VALUES];
        int frame_time_last = 0;
        int frame_count = 0;
        float fps = 0;

        void draw();

        void erase();

    protected:
        FpsCounter();

    public:
        void tick();

        void toggle();
    };
}

#endif //GAME_FPSCOUNTER_H
