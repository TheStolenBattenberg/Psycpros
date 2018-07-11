///gui_tab_destroy(tab);

//Destroy any sub-widgets
var widgets = argument0[4];
for(var i = 0; i < ds_list_size(widgets); ++i) {
    //Get Widget
    var widget = widgets[| i];
    
    switch(widget[1]) { //Get the widget type.
        case 99: return 0;
    }
}

//Finally, clear and destroy the list.
ds_list_clear(widgets);
ds_list_destroy(widgets);
