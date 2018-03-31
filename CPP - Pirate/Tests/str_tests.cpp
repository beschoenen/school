#include "stdafx.h"
#include "CppUnitTest.h"
#include "../CPP-Pirate/str.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace Tests
{
    TEST_CLASS(StrTests)
    {
    public:
        TEST_METHOD(StrIsEmptyAfterCreationWithSize)
        {
            const auto string = new str();
            Assert::AreEqual("", string->data());
        }

        TEST_METHOD(StrHasCharArrayAfterConstructor)
        {
            const auto string = new str("test");
            Assert::AreEqual("test", string->data());
        }

        TEST_METHOD(StrCanBeComparedToCharArray)
        {
            const auto string = new str("t");
            Assert::IsTrue(*string == "t");

            const auto string2 = new str("test");
            Assert::IsTrue(*string2 == "test");
        }

        TEST_METHOD(StrCanBeComparedWithNotOperator)
        {
            const auto string = new str("t");
            Assert::IsFalse(*string != "t");

            const auto string2 = new str("test");
            Assert::IsFalse(*string2 != "test");
        }

        TEST_METHOD(StrValueCanBeAssigned)
        {
            const auto string = new str("t");
//            *string = "test";

            Assert::AreEqual("test", string->data());
        }

        TEST_METHOD(StrValueCanBeAssignedFromAnotherString)
        {
            const auto string = new str("t");
            const auto string2 = new str("test");

//            *string = string2;

            Assert::AreEqual("test", string->data());
            Assert::AreEqual("test", string2->data());
        }

        TEST_METHOD(StrLengthIsCorrect)
        {
            const auto string = new str("hello");
            const auto string2 = new str("bar");

            Assert::AreEqual(5, static_cast<int>(string->size()));
            Assert::AreEqual(3, static_cast<int>(string2->size()));
        }
    };
}
