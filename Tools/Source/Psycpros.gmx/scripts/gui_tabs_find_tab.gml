///gui_tabs_find_tab(list, tab);

for(var i = 0; i < ds_list_size(argument0); ++i) {
    var tab = argument0[| i];
    
    if(tab[0] == argument1[0]) {
        return i;
    }
}
return -1;
