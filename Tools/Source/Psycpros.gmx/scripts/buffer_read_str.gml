///buffer_read_str(buff, len);
gml_pragma("forceinline");

var str = "";
repeat(argument1) {
    var val = buffer_read(argument0, buffer_u8);
    
    if(val == 0) {
        buffer_seek(argument0, buffer_seek_relative, argument1 - (string_length(str) + 1));
        return str;
    }
    str += chr(val);
}
