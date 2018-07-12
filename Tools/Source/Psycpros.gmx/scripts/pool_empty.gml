///pool_empty(pool);

//Get lists from pool.
var l1 = argument0[@ 0];
var l2 = argument0[@ 1];

//Loop through pool and free data.
for(var i = 0; i < ds_list_size(l2); ++i) {
    var poolEnt = l2[| i];
    var fInd  = (poolEnt & $FFFFFF);
    var dType = (poolEnt >> 24) & $FF;
    
    //Delete file
    switch(dType) {
        case Pool.Data2D:
            IMAGE_Free(l1[| fInd]);

            printf("2D Data Cleared");
            break;
        default: break;
    }
    
    //Delete index
    ds_list_delete(l1, fInd); 
}
