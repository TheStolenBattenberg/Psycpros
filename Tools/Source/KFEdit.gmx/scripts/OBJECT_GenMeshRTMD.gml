///OBJECT_GenMeshRTMD(obj, buffer, flags);

var obj   = argument0;
var b     = argument1;
var flags = argument2;

//Get the adress relativity
//var seek = $0;
//if(flags == 0) {
    seek = $C;
//}

//Calculate Offsets
var vOffsetPtr = seek + obj[0];
var nOffsetPtr = seek + obj[2];
var pOffsetPtr = seek + obj[4];

//Build Mesh
var vb = vertex_create_buffer();
vertex_begin(vb, global.VF_TMD);

printf("TMD -> Building Mesh...");

buffer_seek(b, buffer_seek_start, pOffsetPtr);

var i = 0;
printf("        Reading Primitives...");
while(i < obj[5]) {
    //Figure out primitives
    var primType = buffer_read(b, buffer_u32);
    
    var quad = false;
    var v1 = 0, v2 = 0, v3 = 0, v4 = 0;
    var n1 = 0, n2 = 0, n3 = 0, n4 = 0;
    var s1 = 0, s2 = 0, s3 = 0, s4 = 0;
    var t1 = 0, t2 = 0, t3 = 0, t4 = 0;
    var c1 = c_white, c2 = c_white, c3 = c_white, c4 = c_white;
    var a  = 1;
    
    //Primitive type sorting...
    switch(primType) {
        //3 Sided, Flat, Solid Colour
        case $22000304:
            a = 0.5;
        case $20000304:
            var red   = buffer_read(b, buffer_u8);
            var green = buffer_read(b, buffer_u8);
            var blue  = buffer_read(b, buffer_u8);
            buffer_read(b, buffer_u8);
            
            c1 = make_colour_rgb(red, green, blue);
            c2 = c1;
            c3 = c1;
            
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = n1;
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = n1;
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            
            printf("        Primitive ["+string(1+i)+"]"+ "[3 Sided, Flat, Solid Colour]");        
        break;
    
        //3 Sided, Gouraud, Solid Colour
        case $30000406:
            var red   = buffer_read(b, buffer_u8);
            var green = buffer_read(b, buffer_u8);
            var blue  = buffer_read(b, buffer_u8);
            buffer_read(b, buffer_u8);
            
            c1 = make_colour_rgb(red, green, blue);
            c2 = c1;
            c3 = c1;
            
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 2
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 3
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            
            printf("        Primitive ["+string(1+i)+"]"+ "[3 Sided, Gouraud, Solid Colour]");           
        break;
        
        //3 Sided, Flat, Textured
        case $26000507:
            a = 0.5;
        case $24000507:
            s1 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 1
            t1 = buffer_read(b, buffer_u8) / 255;                        
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s2 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 2
            t2 = buffer_read(b, buffer_u8) / 255;            
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s3 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 3
            t3 = buffer_read(b, buffer_u8) / 255;           
            buffer_read(b, buffer_u16);            //Un-needed data
            
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = n1;
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = n1;
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            
            printf("        Primitive ["+string(1+i)+"]"+ "[3 Sided, Flat, Textured]");
        break;
        
        //3 Sided, Gouraud, Textured
        case $36000609:
            a = 0.5;
        case $34000609:
            s1 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 1
            t1 = buffer_read(b, buffer_u8) / 255;                        
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s2 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 2
            t2 = buffer_read(b, buffer_u8) / 255;            
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s3 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 3
            t3 = buffer_read(b, buffer_u8) / 255;           
            buffer_read(b, buffer_u16);            //Un-needed data
            
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 2
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 3
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            
            printf("        Primitive ["+string(1+i)+"]"+ "[3 Sided, Gouraud, Textured]");
        break;
        
        //4 Sided, Flat, Textured
        case $2E000709:
            a = 0.5;
        case $2C000709:
            quad = true;
            
            s1 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 1
            t1 = buffer_read(b, buffer_u8) / 255;                        
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s2 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 2
            t2 = buffer_read(b, buffer_u8) / 255;            
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s3 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 3
            t3 = buffer_read(b, buffer_u8) / 255;           
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s4 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 3
            t4 = buffer_read(b, buffer_u8) / 255;           
            buffer_read(b, buffer_u16);            //Un-needed data  
                      
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = n1;
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = n1;
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            n4 = n1;
            v4 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 4
            
        
            buffer_read(b, buffer_u16); //Un-needed data 
            
            printf("        Primitive ["+string(1+i)+"]"+ "[4 Sided, Flat, Textured]");        
        break;
        
        //4 Sided, Gouraud, Solid Colour
        case $3A000508:
            alpha = 0.5;
        case $38000508:
            quad = true;
            
            var red   = buffer_read(b, buffer_u8);
            var green = buffer_read(b, buffer_u8);
            var blue  = buffer_read(b, buffer_u8);
            buffer_read(b, buffer_u8);
            
            c1 = make_colour_rgb(red, green, blue);
            c2 = c1;
            c3 = c1;
            c4 = c1;         
                      
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            n4 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v4 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 4
            
            printf("        Primitive ["+string(1+i)+"]"+ "[4 Sided, Gouraud, Solid Colour]");            
        break;
                
        //4 Sided, Gouraud, Textured
        case $3E00080C:
            alpha = 0.5;
        case $3C00080C:
            quad = true;
            
            s1 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 1
            t1 = buffer_read(b, buffer_u8) / 255;                        
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s2 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 2
            t2 = buffer_read(b, buffer_u8) / 255;            
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s3 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 3
            t3 = buffer_read(b, buffer_u8) / 255;           
            buffer_read(b, buffer_u16);            //Un-needed data
            
            s4 = buffer_read(b, buffer_u8) / 255;  //UV for Vertex 3
            t4 = buffer_read(b, buffer_u8) / 255;           
            buffer_read(b, buffer_u16);            //Un-needed data  
                      
            n1 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v1 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 1
            n2 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v2 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 2
            n3 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v3 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 3
            n4 = nOffsetPtr + (8 * buffer_read(b, buffer_u16)); //Normal 1
            v4 = vOffsetPtr + (8 * (buffer_read(b, buffer_u16) / 8)); //Vertex 4
            
            printf("        Primitive ["+string(1+i)+"]"+ "[4 Sided, Gouraud, Textured]");            
        break;
        
        default:
            printf("        Unknown Primitive Type ["+dec_to_hex(primType)+"]:");
                    printf("                Mode: "+dec_to_hex(primType & $FF));
                    printf("                Flag: "+dec_to_hex((primType >> 8) & $FF));
                    printf("                iLen: "+dec_to_hex((primType >> 16) & $FF));
                    printf("                oLen: "+dec_to_hex((primType >> 24) & $FF));
            printf("TMD -> Exiting...");
            
            vertex_end(vb);
            vertex_delete_buffer(vb);
        return ERROR_FAIL;
    }
    
    //Add data to buffer.
    if(quad) {
        __vcopy(vb, b, v1, n1, s1, t1, c1, a);
        __vcopy(vb, b, v2, n2, s2, t2, c2, a);
        __vcopy(vb, b, v3, n3, s3, t3, c3, a);
        
        __vcopy(vb, b, v2, n2, s2, t2, c2, a);
        __vcopy(vb, b, v3, n3, s3, t3, c3, a);
        __vcopy(vb, b, v4, n4, s4, t4, c4, a);
    }else{
        __vcopy(vb, b, v1, n1, s1, t1, c1, a);
        __vcopy(vb, b, v2, n2, s2, t2, c2, a);
        __vcopy(vb, b, v3, n3, s3, t3, c3, a);
    }
    ++i;
}

vertex_end(vb);

argument0[@ 7] = vb;

return ERROR_OK;

