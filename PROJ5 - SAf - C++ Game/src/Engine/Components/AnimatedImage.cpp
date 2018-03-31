#include "AnimatedImage.h"

engine::AnimatedImage::AnimatedImage(std::string image, int x, int y, int width, int height) :
    Image(std::move(image), x, y, width, height)
{
}

engine::AnimatedImage::AnimatedImage(std::string image, int x, int y) : AnimatedImage(std::move(image), x, y, -1, -1)
{
}

std::shared_ptr<engine::RenderData> engine::AnimatedImage::render()
{
    auto frame = phases[current_phase][current_frame];

    auto src_rect = new SDL_Rect{frame.x, frame.y, frame.w, frame.h};

    return std::shared_ptr<RenderData>(new RenderData(texture, src_rect, frame.flip));
}

std::string engine::AnimatedImage::get_phase()
{
    return current_phase;
}

bool engine::AnimatedImage::has_phase(std::string name)
{
    return phases.find(name) != phases.end();
}

bool engine::AnimatedImage::add_phase(std::string name)
{
    if (has_phase(name)) return false;

    phases[name] = std::vector<ImageFrame>();

    return true;
}

bool engine::AnimatedImage::remove_phase(std::string name)
{
    if (!has_phase(name)) return false;

    phases.erase(name);

    return true;
}

int engine::AnimatedImage::get_frame()
{
    return current_frame;
}

bool engine::AnimatedImage::has_frame(std::string phase, int index)
{
    return has_phase(phase) && phases[phase].size() - 1 >= index;
}

bool engine::AnimatedImage::add_frame(std::string phase, ImageFrame frame)
{
    if (!has_phase(phase)) return false;

    phases[phase].push_back(frame);

    return true;
}

bool engine::AnimatedImage::remove_frame(std::string phase, int index)
{
    if (!has_frame(phase, index)) return false;

    phases[phase].erase(phases[phase].begin() + index);

    return true;
}

bool engine::AnimatedImage::go_to_phase(std::string phase)
{
    if (phase == current_phase || !has_frame(phase, 0)) return false;

    current_phase = phase;
    current_frame = 0;

    return true;
}

bool engine::AnimatedImage::is_at_first_frame()
{
    return current_frame == 0;
}

bool engine::AnimatedImage::is_at_last_frame()
{
    return current_frame == phases[current_phase].size() - 1;
}

bool engine::AnimatedImage::next_frame(bool reset)
{
    if (reset && is_at_last_frame())
    {
        current_frame = 0;
        return true;
    }

    if (!has_frame(current_phase, current_frame + 1)) return false;

    current_frame++;

    return true;
}
