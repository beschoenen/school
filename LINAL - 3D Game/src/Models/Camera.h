#ifndef LINAL_CAMERA_H
#define LINAL_CAMERA_H


#include "../Components/Circle.h"

#define ROTATE_SPEED 2

class Camera
{
    Circle circle_;

    int rotation_x_ = 0;
    int rotation_y_ = 0;

    bool follow_ = false;

public:
    explicit Camera(Point location);

    bool follow();

    void follow(bool follow);

    Vector get_location();

    Vector get_target();

    void move(Vector& vector);

    void move(int x, int y);

    void move_forward(int multiplier = 1);

    void move_backward(int multiplier = 1);

    void move_left(int multiplier = 1);

    void move_right(int multiplier = 1);

    void move_up(int multiplier = 1);

    void move_down(int multiplier = 1);

    void look(int x, int y);

    void look_up(int multiplier = 1);

    void look_down(int multiplier = 1);

    void look_left(int multiplier = 1);

    void look_right(int multiplier = 1);

    void zoom_out(int multiplier = 1);

    void zoom_in(int multiplier = 1);
};


#endif //LINAL_CAMERA_H
