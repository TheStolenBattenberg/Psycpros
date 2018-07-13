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
	uint  uVertexHeapOffset;    //Offset to where the Vertex Heap Data table is located +4, probably wrong.
	uint  uAnimPtrsOffset; 	    //Always 0x14, as the first animation is at 0x14.
	uint *pAnimPtr;             //An array of ptrs to Animations. Depends on the value of 'uNumAnim'
} MO_Header;

typedef struct {
	uint uNumFrame;             //Number of frames in an animation
	uint *pFramePtr;            //Pointers to frames in the animation.
} MO_Anim;

typedef struct {
	ushort uUnknown01;
	ushort uUnknown02;
	ushort uUnknown03;
	ushort uUnknown04;
} MO_Frame;

typedef struct {
	uint uHeapStart;	   //Where vertex data begins.
	uint uHeapEnd;	           //Where vertex data ends / length / Unknown
} MO_VHeap;

typedef struct {
	uint uNumHeap;             //Number of vertex heaps.
	MO_VHeap vHeap[uNumHeap];  //
} MO_VertexHeapTable;

/** NOTES:
 *	A lot of this is still guesstimation, but we're close...
 *
**/
