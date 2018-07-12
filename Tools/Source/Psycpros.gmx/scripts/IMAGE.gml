///IMAGE();

enum Image {
    Width  = $00,
    Height = $01,
    Mode   = $02,
    IData  = $03,
    CData  = $04,
    Param  = $05,
    SPtr   = $06
};

enum ImageMode {
    BPP4  = 0,
    BPP8  = 1,
    BPP16 = 2,
    BPP24 = 3,
    BPP32 = 4
}

var img2d;
    img2d[Image.Width]  = 0;    //Image Width
    img2d[Image.Height] = 0;    //Image Height
    img2d[Image.Mode]   = 0;    //Image BPP
    img2d[Image.IData]  = null; //Image pixel data pointer
    img2d[Image.CData]  = null; //Image colour data pointer
    img2d[Image.Param]  = 0;    //Image Parameters
    img2d[Image.SPtr]   = null  //Image sprite
return img2d;
