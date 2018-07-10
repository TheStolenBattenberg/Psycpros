///
// Data Types commonly used in FromSoft games.
///

typedef unsigned char  ubyte;
typedef   signed char  sbyte;
typedef unsigned short uword;
typedef   signed short sword;
typedef unsigned int   uint;
typedef   signed int   sint;

template <typename T> struct Vec3 {
	union {
		T data[3];
		struct {
			T x; 
			T y; 
			T z;	
		}
	}
}; 