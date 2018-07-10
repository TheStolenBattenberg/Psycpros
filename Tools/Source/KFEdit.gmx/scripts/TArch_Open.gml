///TArch_Open(tarch, filepath);

//Make sure the file exists.
if(!file_exists(argument1)) {
    return ERROR_NOFILE;
}
argument0[@ 0] = argument1;

//Open File
var b = buffer_load(argument1); {
    //Read File
    var fCount = buffer_read(b, buffer_u16);
    
        
    //Calculate File Starts and offsets.
    var fds = ds_list_create();
    
    var start = 0;
    var size = 0;
    for(var i = 0; i < fCount; ++i) {
        start = buffer_read(b, buffer_u16);
        size  = buffer_read(b, buffer_u16) - start;
        
        buffer_seek(b, buffer_seek_relative, -2);
        
        if(size == 0 || size < 0) {
            printf("TArchive -> Skipping File... [Offset: "+string(start)+", size: "+string(size)+"]");
            continue;
        }
        
        ds_list_add(fds, start, size);
    }
}

argument0[@ 1] = (ds_list_size(fds)) / 2;
argument0[@ 2] = buffer_get_size(b);
argument0[@ 3] = b;
argument0[@ 4] = fds;

return ERROR_OK;
