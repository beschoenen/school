#ifndef SDL_PROJECT_DICE_H
#define SDL_PROJECT_DICE_H

#include <random>

extern std::random_device rd;
extern std::mt19937 mt;

class Random
{
    Random();

    ~Random() = default;

    static Random* instance_;
public:
    static Random& instance();

    int get(int max) const;

    int get(int min, int max) const;

    float getf(float max) const;

    float getf(float min, float max) const;
};

#endif //SDL_PROJECT_DICE_H
