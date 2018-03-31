#ifndef GAME_CURRENTTILES_H
#define GAME_CURRENTTILES_H


#include "../../Engine/Base/Renderable.h"
#include "../../Engine/Base/Entity.h"

class CurrentTiles : public engine::Renderable
{
    std::shared_ptr<engine::Entity> entity;

protected:
    SDL_Surface* build_surface() override;

    void build_texture() override;

public:
    CurrentTiles(std::shared_ptr<engine::Entity>& entity, int x, int y);

    void set_coordinate(int x_offset, int y_offset);
};


#endif //GAME_CURRENTTILES_H
