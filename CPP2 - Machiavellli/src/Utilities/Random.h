#ifndef SDL_PROJECT_DICE_H
#define SDL_PROJECT_DICE_H

#include <random>
#include <memory>

extern std::random_device rd;
extern std::mt19937 mt;

class Random
{
    Random();

    ~Random() = default;

    static std::shared_ptr<Random> instance_;

public:
    static Random& instance();

    int get(int max) const;

    int get(int min, int max) const;
};

#endif //SDL_PROJECT_DICE_H
