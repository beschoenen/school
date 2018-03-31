#pragma once
#define SIZE 100

class str
{
    char* string_;
public:
    str();
    explicit str(const char* string);
    explicit str(const str& instance);

    str& str::operator+(char* string) const;
    str& str::operator+(str& instance) const;
    str& str::operator+(str* instance) const;

    void str::operator+=(char* string);
    void str::operator+=(const int number);
    void str::operator+=(str& instance);
    void str::operator+=(str* instance);

    bool str::operator==(char const* string) const;
    bool str::operator==(str& instance) const;
    bool str::operator==(str* instance) const;
    bool str::operator==(nullptr_t n_pointer) const;

    bool str::operator!=(char const* string) const;
    bool str::operator!=(str& instance) const;
    bool str::operator!=(str* instance) const;

    size_t find(const char* input) const;
    size_t size() const;
    bool empty() const;
    char* data() const;
    ~str();
};
