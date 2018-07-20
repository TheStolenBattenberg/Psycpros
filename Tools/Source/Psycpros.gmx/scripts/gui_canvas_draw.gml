///gui_canvas_draw(canvas, x, y, w, h);

//Get Arguments
var canvas = argument0;
var xx = argument1;
var yy = argument2;
var ww = argument3;
var hh = argument4;
var imgX = canvas[@ 5];
var imgY = canvas[@ 6];
var wlx  = canvas[@ 7];
var wly  = canvas[@ 8]; 

//Get Mouse Position
var wmx = window_mouse_get_x();
var wmy = window_mouse_get_y();

//Make sure the surface exists.
if(!surface_exists(canvas[2])) {
    canvas[@ 2] = surface_create(ww, hh);
}

//Handle Zoom
var ZoomOut = keyboard_check_pressed(vk_down);
var ZoomIn  = keyboard_check_pressed(vk_up);
canvas[@ 1] -= ZoomOut/4;
canvas[@ 1] += ZoomIn/4;
canvas[@ 1] = clamp(canvas[@ 1], 0.25, 8.0);

if(mouse_check_button(mb_left)) {
    imgX += (canvas[@ 7] - wmx) / 2;
    imgY += (canvas[@ 8] - wmy) / 2;
    canvas[@ 3] = true;
}
canvas[@ 7] = wmx;
canvas[@ 8] = wmy;
canvas[@ 6] = imgY;
canvas[@ 5] = imgX

//Handle Updates
if(ZoomOut || ZoomIn || gui_update_needed()) {
    canvas[@ 3] = true; //Set update flag to true
}


if(canvas[@ 3] == true) {
    //Set the draw target.
    surface_set_target(canvas[2]);
    draw_clear(c_black);
    
    for(var i = 0; i < floor(ww/32); ++i) {
        for(var j = 0; j < floor(hh/32); ++j) {            
            draw_sprite(spr_transbg, 0, 64 * i, 64 * j);
        }
    }
    
    texture_set_interpolation(false);
    draw_sprite_ext(canvas[0], 0, (ww/2)-imgX, (hh/2)-imgY, canvas[1], canvas[1], 0, c_white, 1);
    texture_set_interpolation(true);

    //Reset the draw target.
    surface_reset_target();
    
    //Set update flag to false.
    canvas[@ 3] = false;
}
//Draw the canvas.
gui_draw_frame(xx, yy, ww, hh, guic_white2, guic_grey1);
draw_surface_stretched(canvas[2], xx+1, yy+1, ww-1, hh-1);
