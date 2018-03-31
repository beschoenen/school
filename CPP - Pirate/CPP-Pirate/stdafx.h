// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

//debuggin
#include <cstdlib>
#include <crtdbg.h>

// https://msdn.microsoft.com/en-us/library/x98tx3cf.aspx for more info on memory leak dumps
#define _CRTDBG_MAP_ALLOC

#ifdef _DEBUG
#define new new ( _NORMAL_BLOCK , __FILE__ , __LINE__ )
#endif

// TODO: reference additional headers your program requires here
