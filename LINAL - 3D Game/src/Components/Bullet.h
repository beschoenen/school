#ifndef LINAL_BULLET_H
#define LINAL_BULLET_H


#include "../Framework/Component.h"
#include "Spaceship.h"

#define B_MAX_SPEED 100
#define B_SPEED_MODIFIER 50

class Bullet : public Line
{
    Spaceship& owner_;

    Vector direction_;
    Vector velocity_;
    Point start_location_;

public:
    Bullet(Spaceship& owner, Point start_point, Vector length, Vector direction, Vector velocity);

    void update(float delta_time);
};


#endif //LINAL_BULLET_H
