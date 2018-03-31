#include "CharacterDeck.h"
#include "../Thief.h"
#include "../Assassin.h"
#include "../Mage.h"
#include "../King.h"
#include "../Preacher.h"
#include "../Merchant.h"
#include "../Architect.h"
#include "../Mercenary.h"

void CharacterDeck::make_card(const std::string& name, const int order) noexcept
{
    std::shared_ptr<CharacterCard> card = nullptr;

    if (name == "Assassin")
    {
        card = std::make_shared<Assassin>();
    }
    else if (name == "Thief")
    {
        card = std::make_shared<Thief>();
    }
    else if (name == "Mage")
    {
        card = std::make_shared<Mage>();
    }
    else if (name == "King")
    {
        card = std::make_shared<King>();
    }
    else if (name == "Preacher")
    {
        card = std::make_shared<Preacher>();
    }
    else if (name == "Merchant")
    {
        card = std::make_shared<Merchant>();
    }
    else if (name == "Architect")
    {
        card = std::make_shared<Architect>();
    }
    else if (name == "Mercenary")
    {
        card = std::make_shared<Mercenary>();
    }

    card->order = order;
    original_cards.push_back(card);
}

void CharacterDeck::reset() noexcept
{
    // Reset properties
    for (const auto& card : original_cards)
    {
        card->robbed = false;
        card->killed = false;
    }

    cards = original_cards;
}

std::ifstream& operator>>(std::ifstream& ifstream, CharacterDeck& deck) noexcept
{
    try
    {
        const auto matrix = deck.process_csv(ifstream);

        for (const auto& vector : matrix)
        {
            deck.make_card(vector.at(1), atoi(vector.at(0).c_str()));
        }
    }
    catch (...)
    {
        std::cerr << "Invalid card file" << std::endl;
    }

    deck.reset();

    return ifstream;
}

std::ostream& operator<<(std::ostream& ostream, CharacterDeck& deck) noexcept
{
    for (const auto& card : deck.cards)
    {
        ostream << "(" << card->order << ") " << card->name << std::endl;
    }

    return ostream;
}

std::string CharacterDeck::to_string() const noexcept
{
    std::stringstream output;

    auto index = 1;
    for (const auto& card : cards)
    {
        if (card == nullptr) break;

        output << "(" << index++ << ") " << card->name << Socket::endl;
    }

    return output.str();
}

std::string CharacterDeck::print_original_cards() const noexcept
{
    std::stringstream output;

    auto index = 1;
    for (const auto& card : original_cards)
    {
        output << "(" << index++ << ") " << card->name << Socket::endl;
    }

    return output.str();
}

std::vector<std::shared_ptr<CharacterCard>> CharacterDeck::get_original_cards() const noexcept
{
    return original_cards;
}
