cmake_minimum_required(VERSION 3.8)
project(Machiavelli)

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
add_executable(${PROJECT_NAME} ${SOURCE_FILES} ${SOURCE_DIR})

# Add OpenGL for macOS
if (NOT WIN32)
    find_package(OpenGL REQUIRED)
endif ()

add_custom_command(TARGET ${PROJECT_NAME} PRE_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_directory
        ${CMAKE_SOURCE_DIR}/assets $<TARGET_FILE_DIR:${PROJECT_NAME}>)
