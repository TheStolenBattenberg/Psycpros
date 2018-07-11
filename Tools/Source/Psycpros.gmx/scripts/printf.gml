///printf(text);

show_debug_message(argument0);

ds_list_add(con_window.lLog, argument0);
if(ds_list_size(con_window.lLog) > 42) {
    ds_list_delete(con_window.lLog, 0);
}
