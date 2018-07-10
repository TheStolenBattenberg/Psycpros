///TArch_Extract(TArch, path);

var fn = argument0[@ 1];

for(var i = 0; i < fn; ++i) {
    var f = TArch_GetFile(argument0, i);
    
    buffer_save(f, argument1+"\FILE_"+string(i)+".ukn");
    buffer_delete(f);
}
