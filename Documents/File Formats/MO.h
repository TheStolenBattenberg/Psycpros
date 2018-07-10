///
// MO Animated TMD File
// KING'S FIELD II, KING'S FIELD III
// VERSION 0.1
///

#include "KFTypes.h"

typedef struct {
	uint  uFileEOF;
	uint  unknown01;
	uint  uTMDOffset;
	uint  unknown02;
	uint  unknown03;
	uint  unknown04;
} MO_HEADER;

/** NOTES:
 *
 * 'unknown01' increments by 1, then unknown04 increments by 4.
 * 'unknown03' always appears to be '0x14000000'
**/