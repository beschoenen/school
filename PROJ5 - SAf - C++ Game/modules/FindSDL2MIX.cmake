#
# author: Nicolas Van der Noot
# Feb 27 2014
#
# This file finds the include folders (header files) and the libraries
# related to SDL2_mixer
#
# SDL2MIX_FOUND :        1 if all required files found (0 otherwise)
# SDL2MIX_INCLUDE_PATH : include paths (for the header files) -> for compilation
# SDL2MIX_LIBRARIES :    libraries -> for linkage
#

if (APPLE)

    ## ---- MAC OS ---- ##

    # possible paths for: 'SDL_mixer.h'
    set(TRIAL_PATHS_INC
            /Library/Frameworks/SDL2_mixer.framework/Versions/A/Headers
            /usr/local/include/SDL2
    )

    # possible paths for: 'libSDL2_mixer.dylib'
    set(TRIAL_PATHS_LIB /usr/local/lib)

    find_path(SDL2MIX_INCLUDE_PATH SDL_mixer.h ${TRIAL_PATHS_INC})
    find_library(SDL2MIX_LIBRARIES SDL2_mixer)

    if (NOT SDL2MIX_LIBRARIES)
        find_library(SDL2MIX_LIBRARIES libSDL2_mixer.dylib ${TRIAL_PATHS_LIB})
    endif ()

    ## ---------------- ##

else (APPLE)

    ## ---- WINDOWS ---- ##

    set(WIN_LIB_DIRECTORY "SDL2_mixer-2.0.2")
    set(MACH x86)

    set(TRIAL_PATHS_INC ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/include)
    set(TRIAL_PATHS_LIB ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/lib/${MACH})

    find_path(SDL2MIX_INCLUDE_PATH SDL_mixer.h ${TRIAL_PATHS_INC})

    find_library(SDL2MIX_LIBRARIES SDL2_mixer.lib ${TRIAL_PATHS_LIB})

    set(SDL2MIX_DLLS
            "${TRIAL_PATHS_LIB}/SDL2_mixer.dll"
            "${TRIAL_PATHS_LIB}/libmodplug-1.dll"
            "${TRIAL_PATHS_LIB}/libogg-0.dll"
            "${TRIAL_PATHS_LIB}/libvorbisfile-3.dll"
            "${TRIAL_PATHS_LIB}/libvorbis-0.dll"
    )

    mark_as_advanced(SDL2MIX_DLLS)

    ## ----------------- ##

endif (APPLE)

# flag put to 1 if all required files are found
if (SDL2MIX_INCLUDE_PATH AND SDL2MIX_LIBRARIES)
    set(SDL2MIX_FOUND 1)
else ()
    set(SDL2MIX_FOUND 0)
endif ()

# outputs
mark_as_advanced(
        SDL2MIX_FOUND
        SDL2MIX_INCLUDE_PATH
        SDL2MIX_LIBRARIES
)
