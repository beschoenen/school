#include "edge.h"
#include "node.h"

namespace kmint
{
    float edge::distance() const
    {
        return from().location().distance(to().location()) * weight();
    }
}
