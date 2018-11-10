//
// KF4 .DAT
//

typedef struct {
	uint  arcSize;  	//Size of the file in bytes
	uword numFile;		//Number of files in the archive
} DAT_Header;

typedef struct {
	char path[48];
	
	uint unknown;
	uint unknown;
	uint sizFile;
	uint offFile;
} DAT_File;