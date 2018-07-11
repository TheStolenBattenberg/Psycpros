///gui_update_needed();

var ww = window_get_width();
var hh = window_get_height();

if(view_wview[0] != ww || view_hview[0] != hh) {
    return true;
}
return false;
