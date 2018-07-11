///gui_tab_create(name, return);

var tab;
    tab[0] = argument0;                //Name, as a string.
    tab[1] = string_length(argument0); //The name length in characters
    tab[2] = false;                    //If the tab is selected.
    tab[3] = argument1;                //What this tab returns when selected.
    tab[4] = ds_list_create();         //Sub Widget List;
    tab[5] = argument2;                //If the tab is closeable.
return tab;
