#ifndef MACHIAVELLI_STATE_H
#define MACHIAVELLI_STATE_H

#include <memory>
#include "../Server/ClientInfo.h"
#include "../Server/ClientCommand.h"

class State
{
protected:
    std::shared_ptr<State> old_state;

    bool switch_state = false;

public:
    State() noexcept;

    explicit State(const std::shared_ptr<State> old_state) noexcept;

    virtual ~State() = default;

    virtual void enter_state() noexcept {}

    virtual void leave_state() noexcept {}

    virtual void render(std::shared_ptr<ClientInfo>& client) noexcept {}

    virtual void handle_input(std::shared_ptr<ClientCommand>& command) noexcept {}

    virtual void update() noexcept {}

    std::shared_ptr<State> get_old_state() const noexcept;

    void set_old_state(const std::shared_ptr<State> state) noexcept;
};

#endif //MACHIAVELLI_STATE_H
