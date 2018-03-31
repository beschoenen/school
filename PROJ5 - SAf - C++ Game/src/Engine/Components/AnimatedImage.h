#ifndef GAME_ANIMATEDIMAGE_H
#define GAME_ANIMATEDIMAGE_H

#include <string>
#include <map>
#include <vector>
#include "Image.h"
#include "../../defines.h"

namespace engine
{
    class AnimatedImage : public Image
    {
    protected:
        std::string current_phase;
        int current_frame = 0;

        std::map<std::string, std::vector<ImageFrame>> phases;

        AnimatedImage(std::string image, int x, int y, int width, int height);

        AnimatedImage(std::string image, int x, int y);

    public:
        std::shared_ptr<engine::RenderData> render() override;

        std::string get_phase();

        bool has_phase(std::string name);

        bool add_phase(std::string name);

        bool remove_phase(std::string name);

        int get_frame();

        bool has_frame(std::string phase, int index);

        bool add_frame(std::string phase, ImageFrame position);

        bool remove_frame(std::string phase, int index);

        bool go_to_phase(std::string phase);

        bool is_at_first_frame();

        bool is_at_last_frame();

        bool next_frame(bool reset = true);
    };
}


#endif //GAME_ANIMATEDIMAGE_H
