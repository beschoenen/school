#include <iostream>
#include <algorithm>
#include "Spaceship.h"
#include "Square.h"
#include "Bullet.h"

Spaceship::Spaceship(Point front)
{
    Point top = {front.x(), front.y() + 10, front.z() - 30};
    Point left = {top.x() - 5, front.y(), top.z()};
    Point right = {top.x() + 5, front.y(), top.z()};

    auto back = std::make_shared<Triangle>(top, left, right);
    auto bottom = std::make_shared<Triangle>(front, left, right);
    auto connector = std::make_shared<Line>(top, front);
    auto bottom_connector = std::make_shared<Line>(Point{front.x(), front.y(), front.z() + 2}, left.center_to(right));
    auto back_connector = std::make_shared<Line>(top, left.center_to(right));

    auto center_z = (left.z() + front.z()) / 2;

    auto wings = std::make_shared<Square>(Point{left.x() - 4, left.y(), center_z}, Point{right.x() + 4, left.y(), center_z - 10});

    auto left_top_flap = std::make_shared<Square>(Point{left.x() - 4, left.y() + 5, center_z}, Point{left.x() - 4, left.y(), center_z - 10});
    left_top_flap->rotate(Z, -25, {left.x() - 4, left.y(), center_z + 5});

    auto left_bottom_flap = std::make_shared<Square>(Point{left.x() - 4, left.y() - 5, center_z}, Point{left.x() - 4, left.y(), center_z - 10});
    left_bottom_flap->rotate(Z, 25, {left.x() - 4, left.y(), center_z + 5});

    auto right_top_flap = std::make_shared<Square>(Point{right.x() + 4, right.y() + 5, center_z}, Point{right.x() + 4, right.y(), center_z - 10});
    right_top_flap->rotate(Z, 25, {right.x() + 4, right.y(), center_z + 5});

    auto right_bottom_flap = std::make_shared<Square>(Point{right.x() + 4, right.y() - 5, center_z}, Point{right.x() + 4, right.y(), center_z - 10});
    right_bottom_flap->rotate(Z, -25, {right.x() + 4, right.y(), center_z + 5});

    back_ = back;
    bottom_ = bottom;

    bottom_connector->set_color({0x00, 0x00 , 0xff});
    bottom->set_color({0x00, 0x00 , 0xff});

    back_connector->set_color({0xff, 0x00 , 0x00});
    back->set_color({0xff, 0x00 , 0x00});

    wings->set_color({0x55, 0x1a, 0x8b});
    left_top_flap->set_color({0x55, 0x1a, 0x8b});
    left_bottom_flap->set_color({0x55, 0x1a, 0x8b});
    right_top_flap->set_color({0x55, 0x1a, 0x8b});
    right_bottom_flap->set_color({0x55, 0x1a, 0x8b});

    components_ =
    {
        back,
        bottom,
        connector,
        back_connector,
        bottom_connector,
        wings,
        left_top_flap,
        left_bottom_flap,
        right_top_flap,
        right_bottom_flap
    };

    origin_ = std::make_shared<Origin>(Point{rotation_point()}, 30);
}

Point Spaceship::rotation_point()
{
    auto top = back_->get_lines()[0]->get_start();
    auto front = bottom_->get_lines()[0]->get_start();

    return top.center_to(front);
}

std::shared_ptr<Matrix> Spaceship::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Spaceship::rotate(Axis axis, float degrees, Point r)
{
    for (auto& component : components_)
    {
        component->rotate(axis, degrees, r);
    }

    origin_->rotate(axis, degrees, r);
}

void Spaceship::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Spaceship::scale(float size, Point r)
{
    scale_ *= size;

    for (const auto& component : components_)
    {
        component->scale(size, r);
    }
}

void Spaceship::scale(float size)
{
    scale(size, rotation_point());
}

void Spaceship::move(Vector& vector)
{
    for (const auto& component : components_)
    {
        component->move(vector);
    }

    origin_->move(vector);
}

void Spaceship::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (auto& component : components_)
    {
        component->draw(window, camera, target, false);
    }

    for (auto& bullet : bullets_)
    {
        bullet->draw(window, camera, target, false);
    }

    if (show_origin)
    {
        origin_->draw(window, camera, target, false);
    }
}

void Spaceship::set_color(SDL_Color color)
{
    color_ = color;
    for (const auto& component : components_)
    {
        component->set_color(color);
    }
}

void Spaceship::accelerate(float delta_time)
{
    heading_ = 5;
}

void Spaceship::reverse(float delta_time)
{
    heading_ = -2;
}

void Spaceship::free(float delta_time)
{
    velocity_ *= 1 - delta_time;
    velocity_.truncate(MAX_SPEED);

    heading_ = 0;
}

Vector Spaceship::direction()
{
    auto front = bottom_->get_lines()[0]->get_start();
    auto left = bottom_->get_lines()[0]->get_direction().point_from(front);
    auto right = bottom_->get_lines()[1]->get_direction().point_from(front);
    auto back_center = left.center_to(right);

    return back_center.vector_to(front).normalized();
}

void Spaceship::update(float delta_time)
{
    auto force = direction() * heading_;

    auto acceleration = force / MASS;

    auto extra = acceleration * delta_time;

    velocity_ = velocity_ + extra;

    velocity_.truncate(MAX_SPEED);

    move(velocity_);

    for (auto& bullet : bullets_)
    {
        if (bullet == nullptr) continue;

        bullet->update(delta_time);
    }
}

void Spaceship::shoot()
{
    auto front = bottom_->get_lines()[0]->get_start();
    auto left = bottom_->get_lines()[0]->get_direction().point_from(front);
    auto right = bottom_->get_lines()[1]->get_direction().point_from(front);
    auto bottom_connector = left.center_to(right).vector_to(front) / 5;

    auto bullet = std::make_shared<Bullet>(
        *this, bottom_->get_lines()[0]->get_start(), bottom_connector, direction(), velocity_
    );

    bullets_.push_back(bullet);
}

void Spaceship::remove_bullet(Bullet& bullet)
{
    auto b = std::find_if(bullets_.begin(), bullets_.end(), [&bullet](const std::shared_ptr<Bullet> bul) {
        return &bullet == bul.get();
    });

    bullets_.erase(std::remove(bullets_.begin(), bullets_.end(), *b), bullets_.end());
}

std::vector<std::shared_ptr<Bullet>> Spaceship::get_bullets()
{
    return bullets_;
}

void Spaceship::dive()
{
    if (rotation_y_ != 0)
    {
        rotate(Y, -rotation_y_);
    }

    if (rotation_z_ != 0)
    {
        rotate(Z, -rotation_z_);
    }

    if (rotation_x_ != 0)
    {
        rotate(X, -rotation_x_);
    }

    rotate(X, -ROTATION_SPEED);

    if (rotation_x_ != 0)
    {
        rotate(X, rotation_x_);
    }

    if (rotation_z_ != 0)
    {
        rotate(Z, rotation_z_);
    }

    if (rotation_y_ != 0)
    {
        rotate(Y, rotation_y_);
    }

    rotation_x_ -= ROTATION_SPEED;
}

void Spaceship::rise()
{
    if (rotation_y_ != 0)
    {
        rotate(Y, -rotation_y_);
    }

    if (rotation_z_ != 0)
    {
        rotate(Z, -rotation_z_);
    }

    rotate(X, ROTATION_SPEED);

    if (rotation_z_ != 0)
    {
        rotate(Z, rotation_z_);
    }

    if (rotation_y_ != 0)
    {
        rotate(Y, rotation_y_);
    }

    rotation_x_ += ROTATION_SPEED;
}

void Spaceship::dive_left()
{
    if (rotation_x_ != 0)
    {
        rotate(X, -rotation_x_);
    }

    if (rotation_y_ != 0)
    {
        rotate(Y, -rotation_y_);
    }

    rotate(Z, ROTATION_SPEED);

    if (rotation_y_ != 0)
    {
        rotate(Y, rotation_y_);
    }

    if (rotation_x_ != 0)
    {
        rotate(X, rotation_x_);
    }

    rotation_z_ += ROTATION_SPEED;
}

void Spaceship::dive_right()
{
    if (rotation_x_ != 0)
    {
        rotate(X, -rotation_x_);
    }

    if (rotation_y_ != 0)
    {
        rotate(Y, -rotation_y_);
    }

    rotate(Z, -ROTATION_SPEED);

    if (rotation_y_ != 0)
    {
        rotate(Y, rotation_y_);
    }

    if (rotation_x_ != 0)
    {
        rotate(X, rotation_x_);
    }

    rotation_z_ -= ROTATION_SPEED;
}

void Spaceship::rotate_left()
{
    if (rotation_z_ != 0)
    {
        rotate(Z, -rotation_z_);
    }

    if (rotation_x_ != 0)
    {
        rotate(X, -rotation_x_);
    }

    rotate(Y, ROTATION_SPEED);

    if (rotation_x_ != 0)
    {
        rotate(X, rotation_x_);
    }

    if (rotation_z_ != 0)
    {
        rotate(Z, rotation_z_);
    }

    rotation_y_ += ROTATION_SPEED;
}

void Spaceship::rotate_right()
{
    if (rotation_z_ != 0)
    {
        rotate(Z, -rotation_z_);
    }

    if (rotation_x_ != 0)
    {
        rotate(X, -rotation_x_);
    }

    rotate(Y, -ROTATION_SPEED);

    if (rotation_x_ != 0)
    {
        rotate(X, rotation_x_);
    }

    if (rotation_z_ != 0)
    {
        rotate(Z, rotation_z_);
    }

    rotation_y_ -= ROTATION_SPEED;
}
