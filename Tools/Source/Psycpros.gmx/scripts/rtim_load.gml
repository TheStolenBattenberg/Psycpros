///rtim_load(rtim);

printf("Reading RTim File.");

//Copy arguments to temporary variables.
var fTim = argument0;

//Get the total filesize
var fTSize = buffer_get_size(fTim);

//Create a tex page
var TPAGE = TEXPAGE();

while(buffer_tell(fTim) <= fTSize) {
    /* Read Tim Clut */ {
        printf("RTim -> Reading Clut Data...");
        var bColour = null;
        
        //Read Duplicate Information
        buffer_read(fTim, buffer_u16);
        buffer_read(fTim, buffer_u16);
        buffer_read(fTim, buffer_u16);
        buffer_read(fTim, buffer_u16);
        
        //Read Clut Information.
        var ClutX       = buffer_read(fTim, buffer_u16);
        var ClutY       = buffer_read(fTim, buffer_u16);
        var ClutColours = buffer_read(fTim, buffer_u16);
        var ClutNumber  = buffer_read(fTim, buffer_u16);
        
        if(ClutX == $FFFF && ClutY == $FFFF) {
            break;
        }
        
        //Read & Convert Clut Data
        var iColours = ClutColours;
        var ClutLen  = (iColours) * ClutNumber;
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
        
        //Calculate the BPP
        var PMODE = null;
        switch(ClutColours) {
            case $10:
                PMODE = ImageMode.BPP4;
            break;
            
            case $FF:
                PMODE = ImageMode.BPP8;
            break;
        }
        
        printf("            Length: " + string(ClutLen));
        printf("            X Position: " + string(ClutX));
        printf("            Y Position: " + string(ClutY));
        printf("            Clut Colours: " + string(ClutColours));
        printf("            Clut Number: "+string(ClutNumber));
        
        printf("RTim -> Finished reading Clut Data.");
        printf("");
    }
    
    /* Read Tim Image */ {
        printf("RTim -> Reading Pixel Data...");
        var bPixel = null;
        
        //Read Duplicate Information
        buffer_read(fTim, buffer_u16);
        buffer_read(fTim, buffer_u16);
        buffer_read(fTim, buffer_u16);
        buffer_read(fTim, buffer_u16);
        
        //Read Pixel Information
        var PixelX      = buffer_read(fTim, buffer_u16);
        var PixelY      = buffer_read(fTim, buffer_u16);
        var PixelW      = buffer_read(fTim, buffer_u16);
        var PixelH      = buffer_read(fTim, buffer_u16);
        
        //Read Pixel Data
        var iDataLength = (PixelH * PixelW) * 2;
            bPixel = buffer_create(iDataLength, buffer_fixed, 1);
            buffer_copy(fTim, buffer_tell(fTim), iDataLength, bPixel, 0);
            buffer_seek(fTim, buffer_seek_relative, iDataLength);
            
        printf("            Length: " + string(iDataLength));
        printf("            X Position: " + string(PixelX));
        printf("            Y Position: " + string(PixelY));
        printf("            Data Height: " + string(PixelH));
        printf("            Data Width: " + string(PixelW));
        printf("RTim -> Finished reading Pixel Data.");
        printf("");
    }
    
    printf("Reading RTim Read Okay!");
    
    //Put the data into the interal 'IMAGE' format.
    var img = IMAGE();
        img[Image.Width]  = PixelW;
        img[Image.Height] = PixelH;
        img[Image.Mode]   = PMODE;
        img[Image.IData]  = bPixel;
        img[Image.CData]  = bColour;
                
        var param = array_create(2);
            param[0] = PixelX;
            param[1] = PixelY;
        img[Image.Param]  = param;        
        
    TEXPAGE_AddImage(TPAGE, img);
}
//Done.
return TPAGE;
