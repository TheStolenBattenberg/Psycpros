///pool_add(pool, dataptr, datatype);

//Get data lists.
var l1 = argument0[@ 0];
var l2 = argument0[@ 1];

//Add file to list, and get its index.
ds_list_add(l1, argument1);
var fInd = ds_list_size(l1) - 1;

//Create a pool entry.
var poolEnt = (argument2 << 24) | (fInd & $FFFFFF);

//Add pool entry to list.
ds_list_add(l2, poolEnt);
