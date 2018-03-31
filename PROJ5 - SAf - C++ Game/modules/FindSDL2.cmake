#
# author: Nicolas Van der Noot
# Feb 27 2014
#
# This file finds the include folders (header files) and the libraries
# related to SDL2
#
# SDL2_FOUND :        1 if all required files found (0 otherwise)
# SDL2_INCLUDE_PATH : include paths (for the header files) -> for compilation
# SDL2_LIBRARIES :    libraries -> for linkage
#

if (APPLE)

    ## ---- MAC OS ---- ##

    # possible paths for: 'SDL_assert.h'
    set(TRIAL_PATHS_INC_SDL_ASSERT
            /Library/Frameworks/SDL2.framework/Versions/A/Headers
            /usr/local/include/SDL2
    )

    # possible paths for: 'libSDL2.dylib'
    set(TRIAL_PATHS_LIB_SDL2 /usr/local/lib)

    find_path(SDL2_INCLUDE_PATH SDL_assert.h ${TRIAL_PATHS_INC_SDL_ASSERT})

    find_library(SDL2_LIBRARIES SDL2)

    if (NOT SDL2_LIBRARIES)
        find_library(SDL2_LIBRARIES libSDL2.dylib ${TRIAL_PATHS_LIB_SDL2})
    endif ()

    set(SDL2_LIBRARIES ${SDL2_LIBRARIES} "-framework Cocoa")

    ## ---------------- ##

else (APPLE)

    ## ---- WINDOWS ---- ##

    set(WIN_LIB_DIRECTORY "SDL2-2.0.7")
    set(MACH x86)

    set(TRIAL_PATHS_INC ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/include)
    set(TRIAL_PATHS_LIB ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/lib/${MACH})

    find_path(SDL2_INCLUDE_PATH SDL.h ${TRIAL_PATHS_INC})

    find_library(SDL2_LIBRARIES_SDL2 SDL2.lib ${TRIAL_PATHS_LIB})
    find_library(SDL2_LIBRARIES_MAIN SDL2main.lib ${TRIAL_PATHS_LIB})
    find_library(SDL2_LIBRARIES_TEST SDL2test.lib ${TRIAL_PATHS_LIB})

    set(SDL2_LIBRARIES ${SDL2_LIBRARIES_SDL2} ${SDL2_LIBRARIES_MAIN} ${SDL2_LIBRARIES_TEST})

    set(SDL2_DLLS "${TRIAL_PATHS_LIB}/SDL2.dll")

    mark_as_advanced(SDL2_DLLS)

    ## ----------------- ##

endif (APPLE)

# flag put to 1 if all required files are found
if (SDL2_INCLUDE_PATH AND SDL2_LIBRARIES)
    set(SDL2_FOUND 1)
else ()
    set(SDL2_FOUND 0)
endif ()

# outputs
mark_as_advanced(
        SDL2_FOUND
        SDL2_INCLUDE_PATH
        SDL2_LIBRARIES
)
