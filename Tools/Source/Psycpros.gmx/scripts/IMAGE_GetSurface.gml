///IMAGE_GetSurface(img);

//Get data from image.
var img = argument0;
var iWidth  = img[Image.Width];
var iHeight = img[Image.Height];
var iMode   = img[Image.Mode];
var pIData  = img[Image.IData];
var pCData  = img[Image.CData];
var aParam  = img[Image.Param];
var pSprite = img[Image.SPtr];

var iReadSize;
//Get Actual Width & Read Size
switch(iMode) {
    case ImageMode.BPP4: 
        iWidth = iWidth * 4;
        iReadSize = 1;
        break;
    
    case ImageMode.BPP8:
        iWidth = iWidth * 2;
        iReadSize = 1;    
    break;
    
    case ImageMode.BPP16:
        iReadSize = 1;
    break;
    
    case ImageMode.BPP24:
        iReadSize = 3;
    break;
    
    case ImageMode.BPP32:
        iReadSize = 2;
    break;
}

//Write Image Data to Surface
var iDataSize = buffer_get_size(pIData);
var pRT       = surface_create(iWidth, iHeight);

//Reset Buffers to start offsets.
buffer_seek(pIData, buffer_seek_start, 0);
buffer_seek(pCData, buffer_seek_start, 0);

//Set Surface
var ww = 0, hh = 0;
surface_set_target(pRT); {
    draw_clear_alpha(c_black, 0);
    
    //Loop through pixel data and draw to surface.
    var i = 0;
    while(i < (iDataSize / 2)) {
        var iPData;
        
        //Read a set amount of words, calculated using the mode.
        var j = 0;
        repeat(iReadSize) {
            iPData[j] = buffer_read(pIData, buffer_u16)
            ++j;
        }
        
        //Draw this data to the surface, using the mode.
        switch(iMode) {
            case ImageMode.BPP4:
                var j = 0;
                repeat(4) {
                    //Get pixel
                    var p = (iPData[0] >> (4 * j)) & $F;
                    
                    //Get alpha & colour.
                    var c = buffer_peek(pCData, 4 * p, buffer_u32);
                    var a = (c >> 24) & $FF;
                    var c = (c & $FFFFFF);
                    
                    //Draw final colour to screen.
                    draw_set_alpha((1 / 255 ) * a);
                        draw_point_colour(ww, hh, c);
                    draw_set_alpha(1);
                    
                    //Update draw positions.
                    ++ww; if(ww >= iWidth) { ++hh; ww = 0; }
                    ++j;
                }
            break;
        }
        
        ++i;
    }
} surface_reset_target();

return pRT;
