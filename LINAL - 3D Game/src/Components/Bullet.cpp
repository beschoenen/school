#include "Bullet.h"
#include <utility>
#include <iostream>

Bullet::Bullet(Spaceship& owner, Point start_point, Vector length, Vector direction, Vector velocity) :
    Line(start_point, std::move(length)),
    owner_{owner}, start_location_{start_point}, direction_{std::move(direction)}, velocity_{std::move(velocity)}
{
    set_color({0xff, 0x00, 0x00});
}

void Bullet::update(float delta_time)
{
    auto acceleration = direction_ / MASS;

    auto extra = acceleration * delta_time * B_SPEED_MODIFIER;

    velocity_ = velocity_ + extra;

    velocity_.truncate(B_MAX_SPEED);

    move(velocity_);

    if (start_location_.distance(get_start()) > 700)
    {
        owner_.remove_bullet(*this);
    }
}
