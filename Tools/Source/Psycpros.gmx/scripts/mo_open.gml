///mo_open(filename);

//Check if the file exists.
if(!file_exists(argument0)) {
    //If if doesn't, throw an error and exit early.
    return error_throw(Error.NoFile, argument0);
}

printf("Attempting to Open Mo File...");

//Open the file.
var fBuffer = buffer_load(argument0); {
}

printf("Mo opened OK!");

return fBuffer;


