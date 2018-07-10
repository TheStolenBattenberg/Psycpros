///TArch_Close(tarch);

var fl = argument0[@ 4];
var fb = argument0[@ 3];

buffer_delete(fb);
ds_list_destroy(fl);

argument0 = -1;
