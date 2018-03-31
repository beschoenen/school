#include "Player.h"
#include "Game.h"

Player::Player(const std::string& name) noexcept : Player(name, -1)
{
}

Player::Player(const std::string& name, const int age) noexcept : name(name), age(age)
{
    start_turn();
}

bool Player::is_complete() const noexcept
{
    return age > 0 && !name.empty();
}

Turn& Player::get_turn() noexcept
{
    return current_turn;
}

void Player::start_turn() noexcept
{
    current_turn = Turn();
}

std::shared_ptr<CharacterCard> Player::has_card(const std::shared_ptr<CharacterCard>& card) const noexcept
{
    for (const auto& pcard : character_cards)
    {
        if (pcard->name == card->name)
        {
            return pcard;
        }
    }

    return nullptr;
}

std::string Player::building_cards_view() const noexcept
{
    std::stringstream buildings;

    buildings << "Buildings:" << Socket::endl;

    int index = 1;

    for (const auto& card : building_cards)
    {
        if (!card->is_built) continue;

        buildings << "  " << card->to_string() << Socket::endl;
        index++;
    }

    if (index == 1)
    {
        buildings << "  None" << Socket::endl;
    }

    return buildings.str();
}

std::string Player::cards_view() const noexcept
{
    std::stringstream cards;

    cards << "Building cards:" << Socket::endl;

    int total_cards = 0;

    for (const auto& card : building_cards)
    {
        if (card->is_built) continue;

        cards << "  " << card->to_string() << Socket::endl;
        total_cards++;
    }

    if (total_cards == 0)
    {
        cards << "  None" << Socket::endl;
    }

    return cards.str();
}

int Player::number_built_buildings() const noexcept
{
    int number = 0;

    for (const auto& card : building_cards)
    {
        if (card == nullptr) break;

        if (card->is_built) number++;
    }

    return number;
}
