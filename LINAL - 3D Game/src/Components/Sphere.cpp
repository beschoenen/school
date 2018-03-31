#include "Sphere.h"

Sphere::Sphere(Point center, float radius) : radius_{radius}
{
    int degrees = 180 / AMOUNT_OF_CIRCLES;

    for (int i = 0, angle = 0; i < AMOUNT_OF_CIRCLES; i++, angle += degrees)
    {
        auto circle = std::make_shared<Circle>(center, radius);
        circle->rotate(Y, angle);

        circles_.push_back(circle);
    }

    std::vector<std::shared_ptr<Circle>> horizontal_circles;

    for (auto j = 1; j < circles_.size(); j++)
    {
        std::vector<Point> starts;
        std::vector<Point> ends;

        for (auto& circle : circles_)
        {
            auto lines = circle->get_lines();

            auto half = (lines.size() / 4) + (lines.size() / 2);

            starts.push_back(lines[(half + j) % lines.size()]->get_start());
            ends.push_back(lines[(half - j) % lines.size()]->get_start());
        }

        starts.insert(starts.end(), ends.begin(), ends.end());
        starts.push_back(starts[0]);

        std::vector<std::shared_ptr<Line>> lines;

        Point last = {0, 0, 0};
        for (auto& point : starts)
        {
            if (last.is_zero())
            {
                last = point;
                continue;
            }

            lines.push_back(std::make_shared<Line>(last, point));
            last = point;
        }

        horizontal_circles.push_back(std::make_shared<Circle>(lines));
    }

    circles_.insert(circles_.end(), horizontal_circles.begin(), horizontal_circles.end());

    origin_ = std::make_shared<Origin>(Point{rotation_point()}, 30);
}

Point Sphere::rotation_point()
{
    return circles_[0]->rotation_point();
}

std::shared_ptr<Matrix> Sphere::to_matrix(Dimensions dimension)
{
    return nullptr;
}

void Sphere::rotate(Axis axis, float degrees, Point r)
{
    for (auto& circle : circles_)
    {
        circle->rotate(axis, degrees, r);
    }

    origin_->rotate(axis, degrees, r);
}

void Sphere::rotate(Axis axis, float degrees)
{
    rotate(axis, degrees, rotation_point());
}

void Sphere::scale(float size, Point r)
{
    scale_ *= size;

    for (auto& circle : circles_)
    {
        circle->scale(size, r);
    }
}

void Sphere::scale(float size)
{
    scale(size, rotation_point());
}

void Sphere::move(Vector& vector)
{
    for (auto& circle : circles_)
    {
        circle->move(vector);
    }

    origin_->move(vector);
}

void Sphere::draw(Window& window, Vector& camera, Vector& target, bool show_origin)
{
    for (auto& circle : circles_)
    {
        circle->draw(window, camera, target, false);
    }

    if (show_origin)
    {
        origin_->draw(window, camera, target, false);
    }
}

void Sphere::set_color(SDL_Color color)
{
    color_ = color;
    for (auto& circle : circles_)
    {
        circle->set_color(color);
    }
}

std::vector<std::shared_ptr<Circle>> Sphere::get_circles()
{
    return circles_;
}

float Sphere::get_radius()
{
    return radius_;
}
