///gui_tabs_get_selected(ds_list);

for(var i = 0; i < ds_list_size(argument0); ++i) {
    var tab = argument0[| i];
    
    if(tab[2] == true) {
        return tab[3];
    }
}
return -1;
