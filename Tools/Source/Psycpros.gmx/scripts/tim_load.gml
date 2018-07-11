///tim_load(tim);

printf("Reading Tim File.");

//Copy arguments to temporary variables.
var fTim = argument0;

/* Read Tim Header */ {
    printf("Tim -> Reading Header...");
    //Read Flags
    var Flag    = buffer_read(fTim, buffer_u32);
    var PMODE   = (Flag & $07);
    var hasClut = (Flag >> 3) & $01;
    
    printf("            Pixel Mode: " + string(PMODE));
    printf("            Has Clut: "   + string(hasClut));
    printf("Tim -> Finished reading Header.");
    printf("");
}

/* Read Tim Clut */ {
    //Check if the Tim has a clut or not.
    if(hasClut) {
        printf("Tim -> Reading Clut Data...");
        var bColour = null;
        
        //Read Clut Information.
        var ClutLength = buffer_read(fTim, buffer_u32);
        var ClutX      = buffer_read(fTim, buffer_u16);
        var ClutY      = buffer_read(fTim, buffer_u16);
        var ClutH      = buffer_read(fTim, buffer_u16);
        var ClutW      = buffer_read(fTim, buffer_u16);
        
        //Read & Convert Clut Data
        var iColours = ClutW * ClutH;
        var ClutLen  = (ClutLength - 12) / 2;
            bColour  = buffer_create(4 * ClutLen, buffer_fixed, 1);
        
        for(var i = 0; i < iColours; ++i) {
            //Read U16 colour from Tim
            var c = buffer_read(fTim, buffer_u16);
            
            //Convert U16 colour.
            var r, g, b, a;
                r = (c & $1F);
                g = (c >> 5)  & $1F;
                b = (c >> 10) & $1F;
                
            //Convert Alpha
            var colMass = r + g + b;            
                a = (c >> 15) & $1;

            switch(a) {
            case 0:
                if(colMass == 0) {
                    a = 0;
                }else{
                    a = 255;
                }
                break;
                    
            case 1:
                if(colMass = 0) {
                    a = 255;
                }else{
                    a = 127;
                }
                break;
            }
            
            //Calculate the final U32 colour.
            var finalColour = 
                    (a << 24) | 
                    ((b << 3) << 16) | 
                    ((g << 3) << 8) | 
                    (r << 3);
            
            //Write colour to buffer.
            buffer_write(bColour, buffer_u32, finalColour);
        }
        
        printf("            Length: " + string(ClutLength));
        printf("            X Position: " + string(ClutX));
        printf("            Y Position: " + string(ClutY));
        printf("            Data Height: " + string(ClutH));
        printf("            Data Width: " + string(ClutW));
        printf("Tim -> Finished reading Clut Data.");
        printf("");
    }
}

/* Read Tim Image */ {
    printf("Tim -> Reading Pixel Data...");
    var bPixel = null;
    
    //Read Pixel Information
    var PixelLength = buffer_read(fTim, buffer_u32);
    var PixelX      = buffer_read(fTim, buffer_u16);
    var PixelY      = buffer_read(fTim, buffer_u16);
    var PixelW      = buffer_read(fTim, buffer_u16);
    var PixelH      = buffer_read(fTim, buffer_u16);
    
    //Read Pixel Data
    var iDataLength = (PixelLength - 12);
        bPixel = buffer_create(iDataLength, buffer_fixed, 1);
        buffer_copy(fTim, buffer_tell(fTim), iDataLength, bPixel, 0);
    
    printf("            Length: " + string(PixelLength));
    printf("            X Position: " + string(PixelX));
    printf("            Y Position: " + string(PixelY));
    printf("            Data Height: " + string(PixelH));
    printf("            Data Width: " + string(PixelW));
    printf("Tim -> Finished reading Pixel Data.");
    printf("");
}

printf("Reading Tim Read Okay!");

//Put the data into the interal 'IMAGE' format.
var img = IMAGE();
    img[Image.Width]  = PixelW;
    img[Image.Height] = PixelH;
    img[Image.Mode]   = PMODE;
    img[Image.IData]  = bPixel;
    img[Image.CData]  = bColour;

//Done.
return img;
