#ifndef GAME_MOBFOLLOWING_H
#define GAME_MOBFOLLOWING_H

#include <stack>
#include "../../Engine/Base/State.h"
#include "../../Engine/Navigation/Edge.h"
#include "../../Engine/Navigation/Node.h"

class MobFollowing : public engine::State
{
public:
    int counter = 0;
    
    std::stack<Direction> directions;

    std::shared_ptr<level::GridTile> temp_startpoint = nullptr;    
    std::shared_ptr<engine::Node> startpoint = nullptr;
    std::shared_ptr<level::GridTile> temp_endpoint = nullptr;
    std::shared_ptr<engine::Node> endpoint = nullptr;

    void update() override;

    void set_start_point(std::shared_ptr<level::GridTile>& gridtile);

    void set_end_point(std::shared_ptr<level::GridTile>& gridtile);
};

#endif //GAME_MOBFOLLOWING_H
