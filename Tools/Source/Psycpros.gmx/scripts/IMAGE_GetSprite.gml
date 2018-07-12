///IMAGE_GetSprite(img);

if(argument0[Image.SPtr] == null) {
    var surf = IMAGE_GetSurface(argument0);
    
    var spr = sprite_create_from_surface(surf,
                0, 0,
                surface_get_width(surf),
                surface_get_height(surf),
                false,
                false,
                surface_get_width(surf)  / 2, 
                surface_get_height(surf) / 2);
    
    surface_free(surf);
                
    argument0[@ Image.SPtr] = spr;
}

return argument0[@ Image.SPtr];
