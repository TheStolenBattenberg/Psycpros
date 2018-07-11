///IMAGE_Free(img);

buffer_delete(argument0[@ Image.IData]);
buffer_delete(argument0[@ Image.CData]);

if(!argument0[@ Image.SPtr] == null) {
    sprite_delete(argument0[@ Image.SPtr]);
}
