///rtim_open(filepath);

//Check if the file exists.
if(!file_exists(argument0)) {
    //If if doesn't, throw an error and exit early.
    return error_throw(Error.NoFile, argument0);
}

printf("Attempting to Open RTim File...");
//Open the file.
var fBuffer = buffer_load(argument0); {
    //Read Header...
    var mID1 = buffer_read(fBuffer, buffer_u32);
    var mID2 = buffer_read(fBuffer, buffer_u32);
    var mID3 = buffer_read(fBuffer, buffer_u32);
    var mID4 = buffer_read(fBuffer, buffer_u32);   
    buffer_seek(fBuffer, buffer_seek_start, 0);
     
    //Check if this is an RTim file
    if((mID1 + mID4) != (mID2 + mID3)) {
        buffer_delete(fBuffer);
        return error_throw(Error.BadID, argument0);
    }    
}

printf("RTim opened OK!");

return fBuffer;
