///gui_submenu_get(menu, submenuindex);

var elems = argument0[@ 1];
var subm  = elems[| argument1];
var r = subm[4];
subm[@ 4] = -1;

return r;

