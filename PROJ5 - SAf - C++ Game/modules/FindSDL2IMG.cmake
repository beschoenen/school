#
# author: Nicolas Van der Noot
# Feb 27 2014
#
# This file finds the include folders (header files) and the libraries
# related to SDL2_image
#
# SDL2IMG_FOUND :        1 if all required files found (0 otherwise)
# SDL2IMG_INCLUDE_PATH : include paths (for the header files) -> for compilation
# SDL2IMG_LIBRARIES :    libraries -> for linkage
#

if (APPLE)

    ## ---- MAC OS ---- ##

    # possible paths for: 'SDL_image.h'
    set(TRIAL_PATHS_INC
            /Library/Frameworks/SDL2_image.framework/Versions/A/Headers
            /usr/local/include/SDL2
    )

    # possible paths for: 'libSDL2_image.dylib'
    set(TRIAL_PATHS_LIB /usr/local/lib)

    find_path(SDL2IMG_INCLUDE_PATH SDL_image.h ${TRIAL_PATHS_INC})
    find_library(SDL2IMG_LIBRARIES SDL2_image)

    if (NOT SDL2IMG_LIBRARIES)
        find_library(SDL2IMG_LIBRARIES libSDL2_image.dylib ${TRIAL_PATHS_LIB})
    endif ()

    ## ---------------- ##

else (APPLE)

    ## ---- WINDOWS ---- ##

    set(WIN_LIB_DIRECTORY "SDL2_image-2.0.1")
    set(MACH x86)

    set(TRIAL_PATHS_INC ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/include)
    set(TRIAL_PATHS_LIB ${PROJECT_SOURCE_DIR}/libraries/${WIN_LIB_DIRECTORY}/lib/${MACH})

    find_path(SDL2IMG_INCLUDE_PATH SDL_image.h ${TRIAL_PATHS_INC})

    find_library(SDL2IMG_LIBRARIES SDL2_image.lib ${TRIAL_PATHS_LIB})

    set(SDL2IMG_DLLS
            "${TRIAL_PATHS_LIB}/SDL2_image.dll"
            "${TRIAL_PATHS_LIB}/libjpeg-9.dll"
            "${TRIAL_PATHS_LIB}/libpng16-16.dll"
    )

    mark_as_advanced(SDL2IMG_DLLS)

    ## ----------------- ##

endif (APPLE)

# flag put to 1 if all required files are found
if (SDL2IMG_INCLUDE_PATH AND SDL2IMG_LIBRARIES)
    set(SDL2IMG_FOUND 1)
else ()
    set(SDL2IMG_FOUND 0)
endif ()

# outputs
mark_as_advanced(
        SDL2IMG_FOUND
        SDL2IMG_INCLUDE_PATH
        SDL2IMG_LIBRARIES
)
