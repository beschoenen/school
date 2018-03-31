#include "stdafx.h"
#include "str.h"
#include <algorithm>

/**
 * https://codereview.stackexchange.com/questions/98329/stdstring-implementation
 */

str::str() : string_(new char[SIZE])
{
    memset(string_, 0x00, SIZE);
}

str::str(const char* string)
{
    const auto size = strlen(string) + 1;

    string_ = new char[size];
    memset(string_, 0x00, size);

    strcpy_s(string_, size, string);
}

str::str(const str& instance)
{
    const auto size = instance.size() + 1;

    string_ = new char[size];
    memset(string_, 0x00, size);

    strcpy_s(string_, size, instance.data());
}

/////////////
// + operator

str& str::operator+(char* string) const
{
    const auto size = strlen(string) + this->size() + 1;

    const auto temp = new char[size];

    _snprintf_s(temp, size, _TRUNCATE, "%s%s", data(), string);

    const auto instance = new str(temp);
    delete[] temp;

    return *instance;
}

str& str::operator+(str& instance) const
{
    return this->operator+(instance.data());
}

str& str::operator+(str* instance) const
{
    return this->operator+((*instance).data());
}

//////////////
// += operator

void str::operator+=(char* string)
{
    const auto size = strlen(string) + this->size();

    const auto temp = new char[size]{};

    _snprintf_s(temp, size, _TRUNCATE, "%s%s", data(), string);

    string_ = std::move(temp);
}

void str::operator+=(const int number)
{
    char c[3];
    _itoa_s(number, c, 3);

    this->operator+=(c);
}

void str::operator+=(str& instance)
{
    this->operator+=(instance.data());
}

void str::operator+=(str* instance)
{
    this->operator+=((*instance).data());
}

//////////////
// == operator

bool str::operator==(char const* string) const
{
    return strcmp(string, data()) == 0;
}

bool str::operator==(str& instance) const
{
    return this->operator==(instance.data());
}

bool str::operator==(str* instance) const
{
    return this->operator==((*instance).data());
}

bool str::operator==(nullptr_t) const
{
    return !this || size() == 0;
}

//////////////
// != operator

bool str::operator!=(char const* string) const
{
    return !this->operator==(string);
}

bool str::operator!=(str& instance) const
{
    return !this->operator==(instance.data());
}

bool str::operator!=(str* instance) const
{
    return !this->operator==((*instance).data());
}

////////
// Other

size_t str::find(const char* input) const
{
    const auto pc_substr_result = strstr(string_, input);
    if (pc_substr_result)
    {
        return size() - strlen(pc_substr_result);
    }

    return -1;
}

size_t str::size() const
{
    if (this)
    {
        return strlen(string_);
    }

    return 0;
}

bool str::empty() const
{
    if (!this) return true;

    return memcmp(string_, "", strlen(string_)) == 0;
}

char* str::data() const
{
    if (this)
    {
        return string_;
    }

    return nullptr;
}

str::~str()
{
    delete[] string_;
}
