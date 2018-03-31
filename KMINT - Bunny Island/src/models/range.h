#ifndef KMINT_RANGE_H
#define KMINT_RANGE_H

namespace kmint
{
    class range
    {
        float _low;
        float _high;

        explicit range(float low, float high) : _low{low}, _high{high} {}

        void low(float low) { _low = low; }

        float low() { return _low; }

        void high(float high) { _high = high; }

        float high() { return _high; }
    };
}

#endif //KMINT_RANGE_H
