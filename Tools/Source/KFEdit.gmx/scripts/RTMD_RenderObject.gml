///RTMD_RenderObject(TMD, objID);

var flags  = argument0[@ 0];
var oCount = argument0[@ 1];
var oList  = argument0[@ 2];
var b      = argument0[@ 3];

//Check if this object exists.
if(argument1 >= oCount) {
    return ERROR_FAIL;
}
var obj = oList[| argument1];

//Check if this object needs to be created
var res = ERROR_OK;
if(obj[7] == null) {
    res = OBJECT_GenMeshRTMD(obj, b, flags);
    printf("Loading Object Index "+string(1 + argument1));
}

//Render object
if(res == ERROR_OK) {
    vertex_submit(obj[7], pr_trianglelist, background_get_texture(no_tex));
}
return res;
