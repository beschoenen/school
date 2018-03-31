#ifndef LINAL_COMPONENT_H
#define LINAL_COMPONENT_H


#include <SDL_events.h>
#include <SDL_system.h>
#include "Window.h"
#include "../Models/Point.h"
#include "../defines.h"

class Component
{
protected:
    SDL_Color color_ = {0, 0, 0, 0};
    float scale_ = 1.f;

public:
    std::shared_ptr<Matrix> to_origin_matrix(Point r_point, Dimensions dimension = Two);

    std::shared_ptr<Matrix> from_origin_matrix(Point r_point, Dimensions dimension = Two);

    virtual Point rotation_point() = 0;

    virtual std::shared_ptr<Matrix> to_matrix(Dimensions dimension = Two) = 0;

    virtual void rotate(Axis axis, float degrees, Point rotation_point) = 0;

    virtual void rotate(Axis axis, float degrees) = 0;

    virtual void scale(float size, Point r) = 0;

    virtual void scale(float size) = 0;

    virtual void move(Vector& vector) = 0;

    virtual void set_color(SDL_Color color) = 0;

    SDL_Color get_color();

    float get_scale();

    virtual void draw(Window& window, Vector& camera, Vector& target, bool show_origin) = 0;

    Point calculate_3d_location(Point location, Vector& camera, Vector& target);
};


#endif //LINAL_COMPONENT_H
