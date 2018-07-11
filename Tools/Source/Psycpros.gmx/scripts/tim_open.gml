///tim_open(filepath);

//Check if the file exists.
if(!file_exists(argument0)) {
    //If if doesn't, throw an error and exit early.
    return error_throw(Error.NoFile, argument0);
}

printf("Attempting to Open Tim File...");
//Open the file.
var fBuffer = buffer_load(argument0); {
    //Read Magic Number
    var mID = buffer_read(fBuffer, buffer_u32);
    
    //Check the File ID.
    if((mID & $FF) != $10) {
        buffer_delete(fBuffer);
        return error_throw(Error.BadID, argument0);
    }
    
    //Check the file version.
    if(((mID >> 8) & $FF) != $00) {
        buffer_delete(fBuffer);
        return error_throw(Error.WrongVersion, argument0);
    }
}

printf("Tim opened OK!");

return fBuffer;
