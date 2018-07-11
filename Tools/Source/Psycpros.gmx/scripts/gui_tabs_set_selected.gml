///gui_tabs_set_selected(tabs, ind);

//Deselect all other tabs.
for(var i = 0; i < ds_list_size(argument0); ++i) {
    var tab = argument0[| i];
    
    tab[@ 2] = false;
}

//Select the tab we want.
var tab = argument0[| argument1];
tab[@ 2] = true;
