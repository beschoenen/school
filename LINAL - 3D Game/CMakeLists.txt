cmake_minimum_required(VERSION 3.8)
project(LINAL)

# C++ 2017 standard
set(CMAKE_CXX_STANDARD 17)

# Set output folders
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

# Include single files
set(SOURCE_FILES main.cpp)

# Include a whole folder and its subfolders (.cpp and .h files),
file(GLOB_RECURSE SOURCE_DIR "src/*.cpp" "src/*.h")

# Add the created variables as executables
add_executable(LINAL ${SOURCE_FILES} ${SOURCE_DIR})

# Add OpenGL for macOS
if (NOT WIN32)
    find_package(OpenGL REQUIRED)
endif ()

# Set CMake folder
set(CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/modules")

# Include CMake files
find_package(SDL2 REQUIRED COMPONENTS main)
find_package(SDL2TTF REQUIRED COMPONENTS main)

# Define executable target
include_directories(
        ${OPENGL_INCLUDE_DIR}
        ${CMAKE_BINARY_DIR}
        ${SDL2_INCLUDE_PATH}
        ${SDL2TTF_INCLUDE_PATH}
)

# Include lib vars from CMake files
target_link_libraries(LINAL
        ${OPENGL_LIBRARIES}
        ${SDL2_LIBRARIES}
        ${SDL2TTF_LIBRARIES}
)

# Copy the assets folder.
add_custom_command(TARGET LINAL PRE_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_directory
        ${CMAKE_SOURCE_DIR}/assets $<TARGET_FILE_DIR:LINAL>)

# testing
include(CTest)

if (CMAKE_VERSION VERSION_LESS 3.2)
    set(UPDATE_DISCONNECTED_IF_AVAILABLE "")
else()
    set(UPDATE_DISCONNECTED_IF_AVAILABLE "UPDATE_DISCONNECTED 1")
endif()

include(${CMAKE_MODULE_PATH}/DownloadProject.cmake)
download_project(
    PROJ                googletest
    GIT_REPOSITORY      https://github.com/google/googletest.git
    GIT_TAG             master
    ${UPDATE_DISCONNECTED_IF_AVAILABLE}
)

# Prevent GoogleTest from overriding our compiler/linker options
# when building with Visual Studio
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)

add_subdirectory(${googletest_SOURCE_DIR} ${googletest_BINARY_DIR})

# When using CMake 2.8.11 or later, header path dependencies
# are automatically added to the gtest and gmock targets.
# For earlier CMake versions, we have to explicitly add the
# required directories to the header search path ourselves.
if (CMAKE_VERSION VERSION_LESS 2.8.11)
    include_directories("${gtest_SOURCE_DIR}/include" "${gmock_SOURCE_DIR}/include")
endif()

# Include single files
set(SOURCE_FILES_TEST tests/tests.cpp tests/matrixTests.cpp tests/vectorTests.cpp)

# Unit tests using gtest and gmock
add_executable(Tests ${SOURCE_FILES_TEST} ${SOURCE_DIR})
target_link_libraries(Tests gtest)
add_test(NAME unit_tests COMMAND Tests)
target_link_libraries(Tests ${OPENGL_LIBRARIES} ${SDL2_LIBRARIES} ${SDL2TTF_LIBRARIES})

# Copy SDL2 DLLs to output folder on Windows, for each package.
if (WIN32)
    foreach (DLL ${SDL2_DLLS} ${SDL2TTF_DLLS})
        add_custom_command(TARGET LINAL POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy ${DLL} $<TARGET_FILE_DIR:LINAL>)
    endforeach ()
endif ()
