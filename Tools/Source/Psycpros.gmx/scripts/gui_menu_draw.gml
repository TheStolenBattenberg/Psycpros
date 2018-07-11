///gui_menu_draw(menu, x, y);

//Get arguments
var elems = argument0[1];
var xx    = argument1;
var yy    = argument2;

//Get Mouse Position
var wmx = window_mouse_get_x();
var wmy = window_mouse_get_y();

//Get total width & height
var ww = 96;
var hh = 4;
for(var i = 0; i < ds_list_size(elems); ++i) {
    var elem = elems[| i];

    //Get element width & add height
    var ew = 0;
    if(elem[0] != Element.Break) {
        ew = string_width(elem[1]);
        hh += 20;
    }else{
        hh += 4;
    }
    
    //Check if this element width is greater than the current one.
    if((96 + ew) > ww) {
        ww = 96 + ew;
    } 
}

//Draw Shadow
draw_set_colour(c_black);
draw_set_alpha(0.5);
draw_rectangle(xx + 8, yy + 8, (xx+ww) - 2, (yy+hh) + 2, false);
draw_set_alpha(1.0);

//Draw frame
draw_set_colour(guic_white2);
draw_rectangle(xx, yy, (xx+ww), yy+hh, false);
draw_set_colour(guic_grey1);
draw_rectangle(xx, yy, (xx+ww), yy+hh, true);

//Draw Elements.
var eoff = 2;
for(var i = 0; i < ds_list_size(elems); ++i) {
    var elem = elems[| i];
    
    //Draw element highlight
    if(point_in_rectangle(wmx, wmy, xx + 2, yy + eoff, (xx + ww) - 2, (yy + eoff) + 20)) {
        //Deselect all submenus.
        for(var j = 0; j < ds_list_size(elems); ++j) {
            var subElem = elems[| j];
            
            if(subElem[0] == Element.Menu) {
                var oElem = subElem[@ 2];
                oElem[@ 2] = false;
            }
        }
        if(elem[0] == Element.Menu) {
            oElem      = elem[@ 2];                
            oElem[@ 2] = true;
        }
        
        
        //Make sure the element isn't a line break. 
        if(elem[0] != Element.Break) {
            draw_set_colour(guic_blue2);
            draw_rectangle(xx + 4, yy + eoff, (xx + ww) - 2, (yy + eoff) + 20, false);
            
            if(mouse_check_button_pressed(mb_left)) {  
                if(argument0[4] == true) {
                    argument0[@ 2] = false;                
                }              
                return i;
            }
        }
    }
    
    //Draw element
    switch(elem[0]) {
        case Element.Button:
            draw_set_colour(c_black);
            draw_text(xx + 32, yy + eoff + 2, elem[1]);
            
            eoff += 20;
        break;
        
        case Element.Break:
            draw_set_colour(c_ltgray);
            draw_line(xx + 24, yy + eoff + 2, (xx + ww) - 2, yy + eoff + 2);
            eoff += 4;
        break;
        
        case Element.Menu:
            draw_set_colour(c_black);
            draw_text(xx + 32, yy + eoff + 2, elem[1]);
            
            
            draw_triangle((xx+ww)-16, (yy + eoff + 8)-4,(xx+ww)-16, (yy + eoff + 8)+4, (xx+ww)-12, (yy + eoff + 8), false);
            
            oOlem = elem[2];
            if(oOlem[2] == true) {
                var ret = gui_menu_draw(oOlem, xx + ww - 4, yy + eoff + 2);
                
                elem[@ 4] = ret;
                
                if(ret != -1) {
                    oOlem[@ 2] = false;
                }
            }
            eoff += 20;        
        break;
    }
}
return -1;

