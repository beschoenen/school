#include "gtest/gtest.h"
#include "../src/Models/Matrix.h"
#define USE_MATH_DEFINES
#include <math.h>

class ResultsPrinter : public testing::EmptyTestEventListener 
{
    // Called before any test activity starts.
    void OnTestProgramStart(const testing::UnitTest& unit_test) override
    {
        std::cout << "Starting tests for LINAL." << std::endl;
    }

    // Called after all test activities have ended.
    void OnTestProgramEnd(const testing::UnitTest& unit_test) override
    {
        std::cout << std::endl << "Test program: " << (unit_test.Passed() ? "PASSED" : "FAILED") << std::endl;

#ifdef _WINDOWS
        std::cin.ignore();
#endif
    }
    
    // Called before test case start
    void OnTestCaseStart(const testing::TestCase& test_case) override
    {
        std::cout << std::endl << "Starting Test Case " << test_case.name() << std::endl;
    }

    // Called after test case is done
    void OnTestCaseEnd(const testing::TestCase& test_case) override
    {
        std::cout << std::endl << "Ended Test Case " << test_case.name() << std::endl;
    }

    // Called before a test starts.
    void OnTestStart(const testing::TestInfo& test_info) override
    {
        std::cout << std::endl << "Test " << test_info.name() << ": Started." << std::endl;
    }

    // Called after a failed assertion or a SUCCEED() invocation.
    void OnTestPartResult(const testing::TestPartResult& test_part_result) override
    {
        if (test_part_result.failed())
        {
            std::cout << " - Summary: ";
            std::cout << std::endl << test_part_result.summary() << std::endl;
            std::cout << "Failure in ";
            std::cout << test_part_result.file_name() << ":" << test_part_result.line_number() << std::endl;
        }
    }

    // Called after a test ends.
    void OnTestEnd(const testing::TestInfo& test_info) override
    {
        if(test_info.result()->Passed()) std::cout << " - Summary: " << "Succeeded" << std::endl;

        std::cout << "Test " << test_info.name() << ": Ended." << std::endl;
    }
};

int main(int argc, char** argv)
{
    ::testing::InitGoogleTest(&argc, argv);
    auto& listener = testing::UnitTest::GetInstance()->listeners();
    listener.Release(listener.default_result_printer());
    listener.Append(new ResultsPrinter);

    return RUN_ALL_TESTS();
}
