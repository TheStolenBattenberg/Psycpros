///TMD_Close(tmd);

var oList = argument0[@ 2];
for(var i = 0; i < argument0[@ 1]; ++i) {
    var obj = oList[| i];
    if(obj[@ 7] != null) {
        vertex_delete_buffer(obj[@ 7]);
    }
}
buffer_delete(argument0[@ 3]);
argument0 = -1;

return ERROR_OK;
    
