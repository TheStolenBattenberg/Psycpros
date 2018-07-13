///
// MO Animated TMD File
// KING'S FIELD II, KING'S FIELD III
// VERSION 0.1
///

#include "KFTypes.h"

typedef struct {
	uint  uFileEOF;             //Offset to the end of file.
	uint  uNumAnim;             //Number of animations.
	uint  uTMDOffset;           //Offset to the base mesh TMD.
	uint  uUnknown;
} MO_Header;

typedef struct {
	uint uAnimPtrsOffset; //Always 0x14, as the first animation is at 0x14.
	uint *pAnimPtr;       //Depends on the value of uNumAnim.
} MO_AnimHeader;

typedef struct {
	uint uNumFrame;
	
} MO_Anim;


/** NOTES:
 *  That unknown int in the header is bugging me, but we won't know what it is until
 *  we have discovered more of the file.
**/
