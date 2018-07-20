///gui_tabs_draw(ds_list, default, x, y, w, h);

//Store arguments as temporary variables.
var list = argument0;
var def  = argument1;
var xx   = argument2;
var yy   = argument3;
var ww   = argument4;
var hh   = argument5;

//Get Mouse Position
var wmx = window_mouse_get_x();
var wmy = window_mouse_get_y();

//Draw Background
draw_set_colour(guic_white1);
draw_rectangle(xx, yy+16, (xx+ww)-1, yy+hh, false); //Fill
draw_set_colour(guic_grey1);
draw_rectangle(xx, yy+16, (xx+ww)-1, yy+hh, true);  //Frame

//Draw Tabs
var tabOffset = xx;
for(var i = 0; i < ds_list_size(list); ++i) {
    //Get tab Information
    var tab         = list[| i];
    var tabName     = tab[0];
    var tabLength   = (7 * tab[1]);
    var tabSelected = tab[2];

    //Set the tab colours.
    draw_set_colour(guic_white2);
    if(tabSelected) {
        draw_set_colour(guic_white1);
    }else
    if(point_in_rectangle(wmx, wmy, tabOffset, yy, tabOffset + tabLength, (yy + 16))) {
        draw_set_colour(guic_blue1);
        
        //Here we also set selection on mouse click.
        if(mouse_check_button_pressed(mb_left)) {
            //Deselect all other tabs
            for(var j = 0; j < ds_list_size(list); ++j) {
                var tabr = list[| j];
                    tabr[@ 2] = false;
            }
            
            //Select this tab.
            tab[@ 2] = true;
        }
    }
    
    //Draw fill
    draw_rectangle(tabOffset, yy, tabOffset + tabLength, (yy + 16) + (2 * tabSelected), false);
    
    //draw frame
    draw_set_colour(guic_grey1);
    draw_line(tabOffset, yy, tabOffset+tabLength, yy);
    draw_line(tabOffset, yy, tabOffset, yy+16);
    draw_line(tabOffset+tabLength, yy, tabOffset+tabLength, yy+16);
    if(!tabSelected) {
        draw_line(tabOffset, yy+16, tabOffset+tabLength, yy+16); 
    }
    //Draw text
    draw_set_halign(fa_center);
    draw_set_font(guif_ui9);
    draw_set_colour(guic_black);
    draw_text(tabOffset + (tabLength/2), yy, tabName);
    
    
    //Handle close button
    if(tab[5] == true && tabSelected) {
        if(point_in_rectangle(wmx, wmy, (xx + ww) - 12, yy + 20, (xx + ww) - 4, yy + 28)) {
            if(mouse_check_button_pressed(mb_left)) {
                if(tab[2] == true) {
                    ds_list_delete(list, i);
                }
                
                gui_tabs_select(argument0, i-1);
            }
        }
        
        gui_draw_frame((xx + ww) - 12, yy + 20, 8, 8, c_red, guic_grey1);
    }
    
    //Reset all draw commands
    draw_set_halign(fa_left);
    draw_set_colour(c_black);
    
    //Add to offset.
    tabOffset += tabLength + 4;    
}
    
    
