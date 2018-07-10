///
// T Archive
// KING'S FIELD II, KING'S FIELD III
// VERSION 1.0
///

typedef struct { //Size: Dynamic
	short numFile;           //Number of Pointers in pointer table.
	short ptrFile[numPtr];   //Offset to File	
	short eof;               //Offset to End of File.
} T_HEADER;

/** NOTES:
 *
 * In order to calculate the size of a file, read two file ptrs then subtract
 * the starting ptr from the end ptr.
 *
 * The archives sometimes have duplicate fileptrs inside. In order to extract
 * them properly, it is best to add a condition that the filesize must not be 0
 * or less than 0.
**/