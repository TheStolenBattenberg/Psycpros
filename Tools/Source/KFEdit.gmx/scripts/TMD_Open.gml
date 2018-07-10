///TMD_Open(TMD, path);

if(!file_exists(argument1)) {
    return ERROR_NOFILE;
}

var b = buffer_load(argument1); {
    //Check Magic Number
    var MID = buffer_read(b, buffer_u32);
    if(MID != $41) {
        buffer_delete(b);
        return ERROR_BADID;
    }
    
    //Get flags
    var flags = buffer_read(b, buffer_u32);
    
    //Get object number
    var objNum = buffer_read(b, buffer_u32);
    
    //Get Objects
    var objList = ds_list_create();
    for(var i = 0; i < objNum; ++i) {
        var obj = OBJECT();
            OBJECT_Read(obj, b);
            
        ds_list_add(objList, obj);
    }
}

argument0[@ 0] = flags;
argument0[@ 1] = objNum;
argument0[@ 2] = objList;
argument0[@ 3] = b;

return ERROR_OK;
