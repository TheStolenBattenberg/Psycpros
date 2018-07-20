///gui_listbox_draw(listbox, x, y, w, h);

//Get Mouse Position
var wmx = window_mouse_get_x();
var wmy = window_mouse_get_y();

//Get arguments
var lb = argument0;

//Draw frame
gui_draw_frame(argument1, argument2, argument3, argument4, guic_white2, guic_grey1);

//Draw list items
var s = ds_list_size(lb[@ 0]);
var l = lb[@ 0];

var lBegin = lb[@ 2] > 0;
var lEnd   = clamp(lBegin+32, 0, s);

for(var i = lBegin; i < lEnd; ++i) {
    if(i == lb[@ 1]) {
        draw_set_colour(guic_blue1);
            draw_rectangle((argument1 + 3), (argument2 + 2) + (12 * (i-lBegin)),
        (argument1 + 3) + (argument3-3), (argument2 + 2) + (12 * (i-lBegin) + 12), false);   
    }else
    if(point_in_rectangle(wmx, wmy,
        (argument1 + 3), (argument2 + 2) + (12 * (i-lBegin)),
        (argument1 + 3) + (argument3-3), (argument2 + 2) + (12 * (i-lBegin) + 12))) {
        
        draw_set_colour(c_silver);
            draw_rectangle((argument1 + 3), (argument2 + 2) + (12 * (i-lBegin)),
        (argument1 + 3) + (argument3-3), (argument2 + 2) + (12 * (i-lBegin) + 12), false);
        
        if(mouse_check_button_pressed(mb_left)) {
            lb[@ 1] = clamp(i, 0, s);
        }
    }
    
    var elem = l[| i];
    
    draw_set_colour(c_black);
    draw_text((argument1 + 3), (argument2 + 2) + (12 * (i-lBegin)), elem[@ 0]);
}

//Draw scrollbar
var sbh = argument4/(clamp(s-32, 1, $FFFF));
var sby = argument2 + (sbh * lb[@ 2]);
var sbx = argument1+(argument3-12);
gui_draw_frame(argument1+(argument3-12), argument2, 12, argument4, guic_white1, guic_black);
gui_draw_frame(argument1+(argument3-12), sby, 12, sbh, guic_blue1, guic_grey1);
if(point_in_rectangle(wmx, wmy, sbx, sby-(sbh*4), sbx + 12, (sby + (sbh*4)))) {

    if(mouse_check_button(mb_left)) {
        var wmscroll = lb[@ 2] + floor((wmy - sby)/sbh);
        lb[@ 2] = clamp(wmscroll, 0, s-32);
    }
}
