///mo_load(mo);

printf("Reading Mo File.");

//Copy arguments to temporary variables.
var fMo = argument0;

/* Read Mo Header */ {
    printf("Mo -> Reading Header...");
    
    //File Information
    var uEOF       = buffer_read(fMo, buffer_u32);
    var uNumAnim   = buffer_read(fMo, buffer_u32);
    var uTmdOffset = buffer_read(fMo, buffer_u32);
    
    printf("            File End: 0x"      + dec2hex(uEOF));
    printf("            Animation Count: " + string(uNumAnim));
    printf("            TMD Location: 0x"  + dec2hex(uTmdOffset));
    printf("Mo -> Finished Reading Header...");
    printf("");
}

/* Read Mo Animation Header */ {
    if(uNumAnim > 0) {
        printf("Mo -> Reading Animation Header...");
        
        //Animation Header
        var uVertData   = buffer_read(fMo, buffer_u32);
        var uAnimOffset = buffer_read(fMo, buffer_u32);
        
        printf("            Vertex Data Offset: 0x"    + dec2hex(uVertData));
        printf("            Animation Data Offset: 0x" + dec2hex(uAnimOffset));
        printf("Mo -> Finished Reading Animation Header.");
        printf("");
    }
}

/* Read Mo Animation Data */ {
    if(uNumAnim > 0) {
        printf("Mo -> Reading Animation Data...");
        
        //Read the animation data into a list.
        var lAnimList = ds_list_create();
        
        var i = 0;
        while(i < uNumAnim) {
            //Get the location of the animation.
            var animPtr = buffer_read(fMo, buffer_u32);
            var animOff = 4;
            
            //Read the animation.
            var animFrameNum = buffer_peek(fMo, animPtr, buffer_u32);
            
            printf("        Animation [ID: "+string(1 + i)+"] {");
            printf("                Frames: "+string(animFrameNum)+", {");
            var j = 0;
            while(j < animFrameNum) {                                
                var animFrameOff = buffer_peek(fMo, animPtr + animOff, buffer_u32);
                printf("                    ["+string(1+j)+"] 0x" + dec2hex(animFrameOff)+", {");
                
                var frameLoc = 0;
                
                var k = 0;
                while(k < 8) {
                    var animFrameLoc = buffer_peek(fMo, animFrameOff + frameLoc, buffer_u8);
                    printf("                        ["+string(1 + k)+"] 0x"+dec2hex(animFrameLoc)+", ");
                    
                    frameLoc += 1;
                ++k; }
                
                printf("                    }");
                animOff += 4;
            ++j; }
            printf("                }");
            printf("        }");
            
            ++i; }
        printf("");
    }
}
