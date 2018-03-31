#ifndef SDL_PROJECT_DEFINES_H
#define SDL_PROJECT_DEFINES_H

#include <SDL.h>

#define SCREEN_WIDTH 1280
#define SCREEN_HEIGHT 720

#define GAME_TITLE "Dungeon Hunter: The Lone Survivor"

#define LEVEL_HEIGHT 2
#define LEVEL_WIDTH 2

#define FONT "fonts/upheavtt.ttf"

#define TILE_SIZE 50
#define MOVE_DISTANCE (TILE_SIZE / 10)

#define LIGHT_PERCENTAGE 120
#define LIGHT_PERCENTAGE_MIN 60
#define LIGHT_PERCENTAGE_CHANGE 5

#define PLAYER_IMAGE "Character_Hero_Sword.png"
#define MOB_IMAGE "Character_Zombie_Sword.png"
#define CAMPFIRE_IMAGE "tiles/campfire.png"
#define PORTAL_IMAGE "Portal.png"
#define BACKGROUND_IMAGE "background.png"
#define WIN_IMAGE "win_screen_image.png"
#define LOSE_IMAGE "grim_reaper.png"
#define ARROW_IMAGE "compass.png"

#define KEYBINDINGS_DIR "Key Bindings"
#define SAVEFILES_DIR "Save Files"
#define AD_DIR "Advertisements"

#define GAME_ROUNDS 3
#define COUNT_DOWN 3
#define COUNT_DOWN_TICKS 60
#define COUNT_ROUND_TICKS 150
#define PLAYER_LOW_HEALTH 15

#define GAME_SPEED 1
#define ATTACK_DAMAGE_POINT 1
#define ATTACK_DAMAGE_DEAD 10
#define ATTACK_COUNT_DOWN_TICKS 60

#define DEAD_ZONE 10000
#define JOY_HAT "jhat"
#define JBUTTON "jbutton"
#define CBUTTON "cbutton"
#define KEYBOARD "keyboard"

#define SOUND_MAINMENU "sounds/dark_descent.ogg"
#define SOUND_PAUSEMENU "sounds/pause_menu.ogg"
#define SOUND_BUTTONCLICK "sounds/menu_button.wav"
#define SOUND_WIN "sounds/win.ogg"
#define SOUND_LOSE "sounds/game_over.ogg"
#define SOUND_BOTTLE "sounds/bottle.wav"
#define SOUND_CLOTH "sounds/cloth.wav"
#define SOUND_EATING "sounds/eating.wav"
#define SOUND_LOW_HEALTH "sounds/low_health.ogg"
#define SOUNDS_GAME_OVER "sounds/game_over.ogg"
#define SOUND_FOOTSTEP "sounds/footsteps.ogg"
#define SOUND_MOB_DEAD "sounds/mob_dead.ogg"
#define SOUND_MOB_HIT "sounds/mob_hit.ogg"
#define SOUND_PLAYER_HIT "sounds/player_hit.ogg"
#define SOUND_SWORD_ATTACK "sounds/sword_attack.ogg"
#define SOUND_PORTAL "sounds/portal.ogg"
#define SOUND_ACHIEVEMENT "sounds/achievement.ogg"
#define SOUND_STRANGE_SOUND "sounds/strange_sound.ogg"

#define SOUND_AMBIENCE_CAVE_0 "sounds/ambience_cave_0.ogg"
#define SOUND_AMBIENCE_CAVE_1 "sounds/ambience_cave_1.ogg"
#define SOUND_AMBIENCE_CAVE_2 "sounds/ambience_cave_2.ogg"
#define SOUND_AMBIENCE_CAVE_3 "sounds/ambience_cave_3.ogg"

typedef enum
{
   POS_TOP_LEFT = 1,
   POS_TOP_CENTER = 2,
   POS_TOP_RIGHT = 4,
   POS_CENTER_LEFT = 8,
   POS_CENTER_CENTER = 16,
   POS_CENTER_RIGHT = 32,
   POS_BOTTOM_LEFT = 64,
   POS_BOTTOM_CENTER = 128,
   POS_BOTTOM_RIGHT = 256
} Position;

typedef enum
{
    BACKGROUND_LAYER = 0,
    MAP_LAYER = 10,
    ENTITY_LAYER = 20,
    OVERLAY_LAYER = 30,
    HUD_LAYER = 40
} DrawLayer;

typedef struct ImageFrame
{
    int x, y;
    int w, h;
    SDL_RendererFlip flip = SDL_FLIP_NONE;
} ImageFrame;

typedef struct ScreenSize
{
    int w,h;
} ScreenSize;

typedef struct Coordinate
{
    int x,y;
} Coordinate;

#endif //SDL_PROJECT_DEFINES_H
