#ifndef GAME_NODETILE_H
#define GAME_NODETILE_H

#include "../../Engine/Base/Renderable.h"
#include "../../Engine/Navigation/Node.h"

class NodeTile : public engine::Renderable
{
    std::shared_ptr<engine::Node> node_;
    bool closed;
    bool final;
    int step;

protected:
    SDL_Surface* build_surface() override;

    void build_texture() override;

    static std::string format_text(int value);

    static std::string format_text(double value);

public:
    virtual ~NodeTile();

    NodeTile(std::shared_ptr<engine::Node> node, bool closed, bool final, int step);

    NodeTile(std::shared_ptr<engine::Node> node);
};

#endif //GAME_NODETILE_H
