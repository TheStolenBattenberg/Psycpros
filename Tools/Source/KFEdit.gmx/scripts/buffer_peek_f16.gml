///buffer_peek_f16(buffer, off)

var i = buffer_peek(argument0, argument1, buffer_u16);
var s = (i >> 15) & $0001;
var d = (i >> 12) & $0007;
var f =  i        & $0FFF;

var j = 0;
var k = f;
while(k>0) { k/=10; ++j; }
var f16 = (d + f / power(10, j));
if(s) { f16 = f16 * -1; }

return f16;
