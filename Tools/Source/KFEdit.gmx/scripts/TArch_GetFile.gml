///TArch_GetFile(TArch, FNum);
gml_pragma("forceinline");

var fc = argument0[@ 1];
var fs = argument0[@ 2];
var b  = argument0[@ 3];
var fl = argument0[@ 4];

//Check if this file exists.
if(argument1 > fc) {
    return ERROR_FAIL;
}

//Get file location
var offset = 2048 * fl[| (2 * argument1) + 0]
var size   = 2048 * fl[| (2 * argument1) + 1]

show_debug_message("TArchive -> Getting File #"+string(1 + argument1)+" out of "+string(fc)+".");
show_debug_message("                Offset: 0x"+dec_to_hex(offset));
show_debug_message("                Size: 0x"+dec_to_hex(size));

if(size > fs) {
    return ERROR_OUTOFBOUNDS;
}

//Return the file

var f = buffer_create(size, buffer_fixed, 1);
buffer_copy(b, offset, size, f, 0);
return f;

