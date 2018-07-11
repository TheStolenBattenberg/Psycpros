///gui_menubar_draw(list, x, y, w, h);

//Get the arguments.
var list = argument0;
var xx   = argument1;
var yy   = argument2;
var ww   = argument3;
var hh   = argument4;

//Get Mouse Position
var wmx = window_mouse_get_x();
var wmy = window_mouse_get_y();

//Draw Background
draw_set_colour(guic_white1);
draw_rectangle(xx, yy, (xx+ww)-1, yy+hh, false); //Fill

//Check if any menus are open
var anyOpen = false;
for(var i = 0; i < ds_list_size(list); ++i) {
    var ele = list[| i];
        anyOpen |= ele[@ 2];
}

//Draw the Menus
var offs = 0;
for(var i = 0; i < ds_list_size(list); ++i) {
    //Get menu from list
    var menu = list[| i];
    
    //Get menu title width
    var tw = 7 *string_length(menu[0]);
    
    //Handle Menu Click
    if(point_in_rectangle(wmx, wmy, (xx + offs), yy, (xx + offs) + tw, yy+hh)) {
        //If we are over, highlight text.
        draw_set_colour(guic_blue1);
        draw_rectangle((xx + offs), yy, (xx + offs) + tw, yy+hh, false);
        
        //Click
        if(mouse_check_button_pressed(mb_left) || anyOpen) {
            //Close all open menus.
            for(var j = 0; j < ds_list_size(list); ++j) {
                var ele = list[| j];
                    ele[@ 2] = false;
                    
                    //And submenus...
                    var elem = ele[@ 1];
                    for(var k = 0; k < ds_list_size(elem); ++k) {
                        var subm = elem[| k];
                        
                        if(subm[0] = Element.Menu) {
                            var oEm = subm[2];
                            oEm[@ 2] = false;
                        }
                    }
            }
            //Open this menu.
            menu[@ 2] = true;
        }
        
    }
    
    //If the menu is open, we need to highlight and draw the menu.
    var menuret = -1;
    if(menu[2]) {
        //Draw Highlight
        draw_set_colour(guic_blue1);
        draw_rectangle((xx + offs), yy, (xx + offs) + tw, yy+hh, false);
        
        //Draw menu
        menuret = gui_menu_draw(menu, (xx + offs), yy + 16);
        
        //close the menu if we have a return.
        if(menuret != -1) {
            menu[@ 2] = false;
        }
    }
    
    //Update the selected menu
    menu[@ 3] = menuret;
    
    //Draw menu title
    draw_set_colour(c_black);
    draw_set_halign(fa_center);
    draw_text((xx + offs) + (tw / 2), yy, menu[0]);   
    draw_set_halign(fa_left);
    
    //Add menu title width to offset.
    offs += tw;
}

//Close menus if we click outside of it
if(anyOpen) {
    if(mouse_check_button_pressed(mb_left)) {
        for(var i = 0; i < ds_list_size(list); ++i) {
            var ele = list[| i];
                ele[@ 2] = false;
        }        
    }
}
