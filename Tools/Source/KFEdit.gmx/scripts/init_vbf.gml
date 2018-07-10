///init_vbf();

vertex_format_begin() {
    vertex_format_add_position_3d();
    vertex_format_add_normal();
    vertex_format_add_textcoord();
    vertex_format_add_colour();
} global.VF_TMD = vertex_format_end();
