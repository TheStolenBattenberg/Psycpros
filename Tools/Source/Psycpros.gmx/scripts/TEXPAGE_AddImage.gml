///TEXPAGE_AddImage(texpage, image);

var texl = argument0[@ 0];
if(texl == null) {
    argument0[@ 0] = ds_list_create();
    texl = argument0[@ 0]; 
}

ds_list_add(texl, argument1);
