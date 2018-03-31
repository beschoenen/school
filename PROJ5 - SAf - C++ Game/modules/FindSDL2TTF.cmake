#
# author: Nicolas Van der Noot
# Feb 27 2014
#
# This file finds the include folders (header files) and the libraries
# related to SDL2_ttf
#
# SDL2TTF_FOUND :        1 if all required files found (0 otherwise)
# SDL2TTF_INCLUDE_PATH : include paths (for the header files) -> for compilation
# SDL2TTF_LIBRARIES :    libraries -> for linkage
#

if (APPLE)

    ## ---- MAC OS ---- ##

    # possible paths for: 'SDL_ttf.h'
    set(TRIAL_PATHS_INC
            /Library/Frameworks/SDL2_ttf.framework/Versions/A/Headers
            /usr/local/include/SDL2
    )

    # possible paths for: 'libSDL2_ttf.dylib'
    set(TRIAL_PATHS_LIB /usr/local/lib)

    find_path(SDL2TTF_INCLUDE_PATH SDL_ttf.h ${TRIAL_PATHS_INC})
    find_library(SDL2TTF_LIBRARIES SDL2_ttf)

    if (NOT SDL2TTF_LIBRARIES)
        find_library(SDL2TTF_LIBRARIES libSDL2_ttf.dylib ${TRIAL_PATHS_LIB})
    endif ()

    ## ---------------- ##

else (APPLE)

    ## ---- WINDOWS ---- ##

    set(WIN_LIB_DIRECTORY "SDL2_ttf-2.0.14")
    set(MACH x86)

    set(TRIAL_PATHS_INC ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/include)
    set(TRIAL_PATHS_LIB ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/lib/${MACH})

    find_path(SDL2TTF_INCLUDE_PATH SDL_ttf.h ${TRIAL_PATHS_INC})

    find_library(SDL2TTF_LIBRARIES SDL2_ttf.lib ${TRIAL_PATHS_LIB})

    set(SDL2TTF_DLLS
            "${TRIAL_PATHS_LIB}/libfreetype-6.dll"
            "${TRIAL_PATHS_LIB}/SDL2_ttf.dll"
            "${TRIAL_PATHS_LIB}/zlib1.dll"
    )

    mark_as_advanced(SDL2TTF_DLLS)

    ## ----------------- ##

endif (APPLE)

# flag put to 1 if all required files are found
if (SDL2TTF_INCLUDE_PATH AND SDL2TTF_LIBRARIES)
    set(SDL2TTF_FOUND 1)
else ()
    set(SDL2TTF_FOUND 0)
endif ()

# outputs
mark_as_advanced(
        SDL2TTF_FOUND
        SDL2TTF_INCLUDE_PATH
        SDL2TTF_LIBRARIES
)
