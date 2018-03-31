#include <SDL_timer.h>
#include "FpsCounter.h"
#include "../Engine.h"
#include <iostream>

engine::FpsCounter::FpsCounter()
{
    frame_time_last = SDL_GetTicks();
}

void engine::FpsCounter::tick()
{
    int frame_times_index;
    int get_ticks;
    int count;
    int i;

    frame_times_index = frame_count % FRAME_VALUES;
    get_ticks = SDL_GetTicks();

    frame_times[frame_times_index] = get_ticks - frame_time_last;
    frame_time_last = get_ticks;

    frame_count++;
    count = frame_count < FRAME_VALUES ? frame_count : FRAME_VALUES;

    fps = 0;
    for (i = 0; i < count; i++)
    {
        fps += frame_times[i];
    }

    fps /= count;

    //Frames per second value
    fps = 1000.f / fps;

    int int_value_fps = fps;

    if (text_display == nullptr)
    {
        text_display = std::shared_ptr<FpsText>(new FpsText(TEXT_PREFIX));
    }

    text_display->set_text(TEXT_PREFIX + std::to_string(int_value_fps));
}

void engine::FpsCounter::draw()
{
    if (text_display != nullptr)
    {
        Engine::instance()->draw(text_display);
    }
}

void engine::FpsCounter::erase()
{
    if (text_display != nullptr)
    {
        Engine::instance()->erase(text_display);
    }
}

void engine::FpsCounter::toggle()
{
    visible = !visible;
    visible ? draw() : erase();
}
