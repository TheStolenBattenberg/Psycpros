///
// MO Animated TMD File
// KING'S FIELD II, KING'S FIELD III
// VERSION 0.2
///

#include "KFTypes.h"

typedef struct {
	uint  uFileEOF;             //Offset to the end of file.
	uint  uNumAnim;             //Number of animations.
	uint  uTMDOffset;           //Offset to the base mesh TMD.
	uint  uVertexHeapOffset;    //Offset to where the Vertex Heap Data table is located.
	uint  uAnimPtrsOffset; 	    //Always 0x14, as the first animation is at 0x14.
	uint *pAnimPtr;             //An array of ptrs to Animations. Depends on the value of 'uNumAnim'
} MO_Header;

typedef struct {
	uint uNumFrame;             //Number of frames in an animation
	uint *pFramePtr;            //Pointers to frames in the animation.
} MO_Anim;

typedef struct {
	/* UNKNOWN... Size seems to change. */
} MO_Frame;

typedef struct { //They skipped out the end padding word from regular TMDs...
	sword sPosX;
	sword sPosY;
	sword sPosZ;
} MO_VERTEXPACK;

typedef struct { // 4 * TotalFrameCount. TotalFrameCount is equal to the total frames across all animations.
	uint* uVertexOffset;	//An array of pointers to vertex data, we don't understand yet.
} MO_VertexHeapTable;

/** NOTES:
 *	Seems to be some kind of compressed vertex morph file format, only saving the vertices 
 *	which changed between frames, rather than all of them.
 *	
 *	Every time it reads a frame, it probably reads a location from the VertexHeapOffset, because
 *	There are no pointers to this data anywhere aside from at the top.
**/
