cmake_minimum_required(VERSION 3.8)
project(Kmint)

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

# Set CMake folder
set(CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/modules")

# Include CMake files
find_package(SDL2 REQUIRED COMPONENTS main)
find_package(SDL2TTF REQUIRED COMPONENTS main)
find_package(SDL2IMG REQUIRED COMPONENTS main)

# Define executable target
include_directories(
        ${OPENGL_INCLUDE_DIR}
        ${CMAKE_BINARY_DIR}
        ${SDL2_INCLUDE_PATH}
        ${SDL2TTF_INCLUDE_PATH}
        ${SDL2IMG_INCLUDE_PATH}
)

# Include lib vars from CMake files
target_link_libraries(${PROJECT_NAME}
        ${OPENGL_LIBRARIES}
        ${SDL2_LIBRARIES}
        ${SDL2TTF_LIBRARIES}
        ${SDL2IMG_LIBRARIES}
)

# Copy the assets folder.
add_custom_command(TARGET ${PROJECT_NAME} PRE_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_directory
        ${CMAKE_SOURCE_DIR}/assets $<TARGET_FILE_DIR:${PROJECT_NAME}>)

# Copy SDL2 DLLs to output folder on Windows, for each package.
if (WIN32)
    foreach (DLL ${SDL2_DLLS} ${SDL2TTF_DLLS} ${SDL2IMG_DLLS})
        add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
                COMMAND ${CMAKE_COMMAND} -E copy
                ${DLL} $<TARGET_FILE_DIR:${PROJECT_NAME}>)
    endforeach ()
endif ()
