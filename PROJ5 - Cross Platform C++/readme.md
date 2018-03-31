### Windows (Visual Studio)

Install Cmake from

    https://cmake.org/files/v3.9/cmake-3.9.2-win64-x64.msi

Download the [SDL2 zip](https://www.libsdl.org/release/SDL2-devel-2.0.5-VC.zip) and place in the folder `extern` in the project root.

    C:/Path/to/project/root/extern/SDL2-2.0.5

### MacOS (Xcode)

Install the SDL2 headers and libraries using [Homebrew](https://www.brew.sh/).

    brew install cmake
    brew install sdl2

OpenGL headers and libraries are installed by Xcode. CMake will find them automatically.