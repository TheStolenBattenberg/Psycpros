///gui_menu_create(name, submenu);

var menu;
    menu[0] = argument0;        //Menu title.
    menu[1] = ds_list_create(); //List of elements.
    menu[2] = false;            //If this one is open
    menu[3] = -1;
    menu[4] = argument1;
    menu[5] = -4;               //State
return menu;
