#include <iostream>
#include "Camera.h"

Camera::Camera(Point location) : circle_{location, 100.f}
{
    circle_.rotate(Y, 90);

    circle_.rotate(X, -30);
    rotation_x_ = -30;
}

bool Camera::follow()
{
    return follow_;
}

void Camera::follow(bool follow)
{
    follow_ = follow;
}

Vector Camera::get_location()
{
    auto p = circle_.get_lines()[0]->get_start();

    return {p.x(), p.y(), p.z()};
}

Vector Camera::get_target()
{
    auto p = circle_.rotation_point();

    return {p.x(), p.y(), p.z()};
}

void Camera::move(Vector& vector)
{
    circle_.move(vector);
}

void Camera::move(int x, int y)
{
    if (x < 0)
    {
        move_left(x / 2);
    }
    else if (x > 0)
    {
        move_right(-(x / 2));
    }

    if (y < 0)
    {
        move_forward(y / 2);
    }
    else if (y > 0)
    {
        move_backward(-(y / 2));
    }
}

void Camera::move_forward(int multiplier)
{
    follow_ = false;

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, -rotation_x_);
    }

    auto location = get_location();
    auto target = get_target();

    auto v = (target - location).normalized() * multiplier;

    circle_.move(v);

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, rotation_x_);
    }
}

void Camera::move_backward(int multiplier)
{
    follow_ = false;

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, -rotation_x_);
    }

    auto location = get_location();
    auto target = get_target();

    auto v = (location - target).normalized() * multiplier;

    circle_.move(v);

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, rotation_x_);
    }
}

void Camera::move_left(int multiplier)
{
    follow_ = false;

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, -rotation_y_);
    }

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, -rotation_x_);
    }

    circle_.rotate(Y, 90);

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, rotation_x_);
    }

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, rotation_y_);
    }

    move_forward(multiplier);

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, -rotation_y_);
    }

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, -rotation_x_);
    }

    circle_.rotate(Y, -90);

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, rotation_x_);
    }

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, rotation_y_);
    }
}

void Camera::move_right(int multiplier)
{
    follow_ = false;

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, -rotation_y_);
    }

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, -rotation_x_);
    }

    circle_.rotate(Y, -90);

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, rotation_x_);
    }

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, rotation_y_);
    }

    move_forward(multiplier);

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, -rotation_y_);
    }

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, -rotation_x_);
    }

    circle_.rotate(Y, 90);

    if (rotation_x_ != 0)
    {
        circle_.rotate(X, rotation_x_);
    }

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, rotation_y_);
    }
}

void Camera::move_up(int multiplier)
{
    follow_ = false;

    Vector v = {0, 2 * multiplier, 0};

    circle_.move(v);
}

void Camera::move_down(int multiplier)
{
    follow_ = false;

    Vector v = {0, -2 * multiplier, 0};

    circle_.move(v);
}

void Camera::look(int x, int y)
{
    if (x < 0)
    {
        look_left(x / 2);
    }
    else if (x > 0)
    {
        look_right(-(x / 2));
    }

    if (y < 0)
    {
        look_up(y / 2);
    }
    else if (y > 0)
    {
        look_down(-(y / 2));
    }
}

void Camera::look_up(int multiplier)
{
    if (rotation_x_ > 60) return;

    auto speed = ROTATE_SPEED * multiplier;

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, -rotation_y_);
    }

    circle_.rotate(X, -speed);

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, rotation_y_);
    }

    rotation_x_ -= speed;
}

void Camera::look_down(int multiplier)
{
    if (rotation_x_ < -60) return;

    auto speed = ROTATE_SPEED * multiplier;

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, -rotation_y_);
    }

    circle_.rotate(X, speed);

    if (rotation_y_ != 0)
    {
        circle_.rotate(Y, rotation_y_);
    }

    rotation_x_ += speed;
}

void Camera::look_left(int multiplier)
{
    auto speed = ROTATE_SPEED * multiplier;

    circle_.rotate(Y, -speed);

    rotation_y_ -= speed;
}

void Camera::look_right(int multiplier)
{
    auto speed = ROTATE_SPEED * multiplier;

    circle_.rotate(Y, speed);

    rotation_y_ += speed;
}

void Camera::zoom_out(int multiplier)
{
    circle_.scale(1 + (0.02f * multiplier));
}

void Camera::zoom_in(int multiplier)
{
    circle_.scale(1 - (0.02f * multiplier));
}
