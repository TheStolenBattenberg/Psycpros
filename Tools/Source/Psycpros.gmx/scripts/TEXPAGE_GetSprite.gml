///TEXPAGE_GetSprite(texpage);

if(argument0[@ 1] == null) {
    var lImg  = argument0[@ 0];
    var iImgC = ds_list_size(lImg);
    
    //Generate the sprites.
    for(var i = 0; i < iImgC; ++i) {
        var img = lImg[| i];
        
        IMAGE_GetSprite(lImg[| i]);
    }
    
    var surf = surface_create(1024-320, 512);
    surface_set_target(surf);
    draw_clear_alpha(c_black, 0.5);
    //Render a shit ton of sprites
    for(var i = 0; i < iImgC; ++i) {
        var img = lImg[| i];
        
        //Get X and Y from params
        var params = img[Image.Param];
        var xdraw = params[0]-320;        
        var ydraw = params[1];
        
        //draw sprite to surface
        var wo = 0; //sprite_get_width(img[Image.SPtr])/2;
        var ho = 0; //sprite_get_height(img[Image.SPtr])/2;
        draw_sprite_stretched(img[Image.SPtr], 0, xdraw+wo, ydraw+ho, img[@ Image.Width], img[@ Image.Height]);
        show_debug_message("Drawing Sprite to: {"+string(xdraw)+", "+string(ydraw)+"}");
        
        draw_set_colour(choose(c_red, c_yellow, c_lime, c_purple));
        draw_rectangle(xdraw, ydraw, xdraw+(img[@ Image.Width]), ydraw+img[@ Image.Height], true);
    }
    surface_reset_target();
    
    //Get sprite from surface
    var spr = sprite_create_from_surface(surf,
            0, 0, 
            surface_get_width(surf), surface_get_height(surf),
            false,
            false,
            surface_get_width(surf)/2, surface_get_height(surf)/2);
            
    surface_free(surf);
    
    argument0[@ 1] = spr;
}

return argument0[@ 1]
