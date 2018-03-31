cmake_minimum_required(VERSION 3.8)
project(week_5_graphs)

set(CMAKE_CXX_STANDARD 17)

set(SOURCE_FILES main.cpp src/tools.cpp src/tools.h src/game.cpp src/game.h)
add_executable(week_5_graphs ${SOURCE_FILES})