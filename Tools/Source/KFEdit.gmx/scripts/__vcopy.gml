///__vcopy(vb, buffer, voff, noff, u, v, c, alpha);

var vx = buffer_peek(argument1, argument2 + 0, buffer_s16);
var vy = buffer_peek(argument1, argument2 + 2, buffer_s16);
var vz = buffer_peek(argument1, argument2 + 4, buffer_s16);
var nx = buffer_peek_f16(argument1, argument3 + 0);
var ny = buffer_peek_f16(argument1, argument3 + 2);
var nz = buffer_peek_f16(argument1, argument3 + 4);

printf("                VERTEX ["+string(argument2)+"]: [X: "+string(vx)
                   +"], [Y: "+string(vy)
                   +"], [Z: "+string(vz)+"]");

vertex_position_3d(argument0, vx, vy, vz);       
vertex_normal(argument0, nx, ny, nz); 
vertex_texcoord(argument0, argument4, argument5);
vertex_colour(argument0, argument6, argument7);
