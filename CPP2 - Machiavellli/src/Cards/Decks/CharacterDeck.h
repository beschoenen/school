#ifndef MACHIAVELLI_CHARACTERDECK_H
#define MACHIAVELLI_CHARACTERDECK_H

#include <vector>
#include "../../Utilities/File/CsvReader.h"
#include "../Base/CharacterCard.h"

class CharacterDeck : public CsvReader
{
    void make_card(const std::string& name, int order) noexcept;

    std::vector<std::shared_ptr<CharacterCard>> original_cards;

public:
    std::vector<std::shared_ptr<CharacterCard>> cards;

    void reset() noexcept;

    friend std::ifstream& operator>>(std::ifstream& ifstream, CharacterDeck& deck) noexcept;

    friend std::ostream& operator<<(std::ostream& ostream, CharacterDeck& deck) noexcept;

    std::string to_string() const noexcept;

    std::vector<std::shared_ptr<CharacterCard>> get_original_cards() const noexcept;

    std::string print_original_cards() const noexcept;
};

#endif //MACHIAVELLI_CHARACTERDECK_H
