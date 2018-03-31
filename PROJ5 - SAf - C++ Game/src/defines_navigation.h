#ifndef DEFINES_NAVIGATION_H
#define DEFINES_NAVIGATION_H

#define ASTAR_DEBUG false

#define ASTAR_SEARCH_RANGE 4

#define NODE_MOVEMENT_COST 10

typedef enum
{
    DIR_UP = 1,
    DIR_UP_RIGHT = 2,
    DIR_RIGHT = 3,
    DIR_DOWN_RIGHT = 4,
    DIR_DOWN = 5,
    DIR_DOWN_LEFT = 6,
    DIR_LEFT = 7,
    DIR_UP_LEFT = 8
} Direction;

#endif //DEFINES_NAVIGATION_H
