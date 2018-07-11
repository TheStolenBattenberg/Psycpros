///gui_tabs_select_by_name(ds_list, name);

for(var i = 0; i < ds_list_size(argument0); ++i) {
    var tab = argument0[| i];
        tab[@ 2] = false;
        
    if(tab[@ 0] == argument1) {
        tab[@ 2] = true;
    }
}
var tab = argument0[| argument1];
    tab[@ 2] = true;
