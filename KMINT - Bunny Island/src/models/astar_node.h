#ifndef KMINT_ASTAR_NODE_H
#define KMINT_ASTAR_NODE_H

#include "node.h"

namespace kmint
{
    class astar_node
    {
        kmint::node* _node = nullptr;
        kmint::astar_node* _parent = nullptr;

        unsigned int _g_value = 0;
        unsigned int _h_value = 0;

    public:
        explicit astar_node(kmint::node* n) : _node(n) {}

        explicit astar_node(kmint::node n)
        {
            _node = new node{ n.location(), n.id() };
            for (auto edge : n.get_edges())
            {
                _node->add_edge(edge);
            }
        }

        unsigned int g() const noexcept { return _g_value; }

        void g(unsigned int value) noexcept { _g_value = value; }

        unsigned int h() const noexcept { return _h_value; }

        void h(unsigned int value) noexcept { _h_value = value; }

        unsigned int get_score() { return _g_value + _h_value; }

        kmint::node* get_node() noexcept { return _node; };

        void set_node(kmint::node* node) noexcept { _node = node; }

        kmint::astar_node* get_parent() noexcept { return _parent; };

        void set_parent(kmint::astar_node* parent) noexcept { _parent = parent; }

        bool operator==(astar_node* right) { return get_node()->id() == right->get_node()->id(); }

    };
}

#endif //KMINT_ASTAR_NODE_H
