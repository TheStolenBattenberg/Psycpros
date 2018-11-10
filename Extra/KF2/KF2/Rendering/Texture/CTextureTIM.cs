using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace KF2.Rendering.Texture {
    //Struct for TIM Palettes
    public struct TIMColour {
        public byte R, G, B, A;

        public TIMColour(byte r, byte g, byte b, byte a) {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    public class CTextureTIM : CTexture2D {
        //Header Information
        private uint iBPPMode;
        private uint iHasClut;

        //CLUT Information
        private List<TIMColour> lClut;

        //PIXEL Information
        private uint iDataWidth;
        private uint iDataHeight;

        //Thread for loading
        private Thread tHandle;

        //
        // Constructor
        //
        public CTextureTIM(string fname) {
            tHandle = new Thread(() => LoadTim(fname));
            tHandle.Start();
        }

        //
        // 'Thread safe' load function
        //
        private void LoadTim(string fname) {
            BinaryReader pTim = OpenTIM(fname);

            //Varify the Tim File
            if(!VarifyTIM(pTim)) {
                pTim.Close();
                return;
            }

            //Read Tim Info
            uint timFlag = pTim.ReadUInt32();
            iBPPMode = (timFlag & 0x7);
            iHasClut = (timFlag & 0x8) >> 3;

            //Read Tim Clut
            if(iHasClut == 1) {
                ReadCLUT(pTim);
            }

            //Read Tim Image Header
            ReadImageHeader(pTim);

            //Read Tim Pixel Data
            switch (iBPPMode) {
                case 0:
                    ReadImage4BPP(pTim);
                    break;
            }

            bReady = true;
        }

        //
        // functions used to load Tim Files
        //
        private BinaryReader OpenTIM(string fname) {
            //Open the Tim File
            BinaryReader pTimFile;

            try {
                pTimFile = new BinaryReader(File.Open(fname, FileMode.Open));
            } catch (Exception E) {
                throw E;
            }

            return pTimFile;
        }
        private bool VarifyTIM(BinaryReader tim) {
            //Read Tim ID from file
            uint timID = tim.ReadUInt32();

            //Check ID
            if((timID & 0xFF) != 0x10) {
                TIMERR("File is not a known TIM Graphic type.");
                return false;
            }

            //Check Version
            if(((timID >> 8) & 0xFF) != 0x00){
                TIMERR("This version of TIM is not supported.");
                return false;
            }
            return true;
        }
        private void ReadCLUT(BinaryReader tim) {
            //Read BNum from Tim
            uint BNum = tim.ReadUInt32();

            //Read VRAM Location (unused)
            ushort VRamX = tim.ReadUInt16();
            ushort VRamY = tim.ReadUInt16();

            //Read Clut Size
            ushort ClutW = tim.ReadUInt16();
            ushort ClutH = tim.ReadUInt16();

            //Initilize List used to store colours
            lClut = new List<TIMColour>(ClutW * ClutH);

            //Read Clut Data
            for (ushort i = 0; i < ClutH; ++i) {
                for(ushort j = 0; j < ClutW; ++j) {
                    //Read a 16-Bit Colour from the palette.
                    ushort col16 = tim.ReadUInt16();

                    //Convert colour to RGBA, add it to list.
                    lClut.Add(
                    new TIMColour((byte)((col16 & 0x1F) << 3),
                        (byte)(((col16 >> 5) & 0x1F) << 3),
                        (byte)(((col16 >> 10) & 0x1F) << 3),
                        255));
                }
            }
        }
        private void ReadImageHeader(BinaryReader tim) {
            //Read BNum from Tim (Data Length)
            uint BNum = tim.ReadUInt32();

            //Read VRAM Location (unused)
            ushort VRamX = tim.ReadUInt16();
            ushort VRamY = tim.ReadUInt16();

            //Read Image Size
            iDataWidth  = tim.ReadUInt16();
            iDataHeight = tim.ReadUInt16();
        }
        private void ReadImage4BPP(BinaryReader tim) {
            //Set Actual texture size
            iWidth = iDataWidth * 4;
            iHeight = iDataHeight;

            //Create Bitmap
            pBitMap = new byte[(iWidth * iHeight) * 4];

            //Loop through texture
            for(ushort j = 0; j < iDataHeight; ++j) {
                for(ushort i = 0; i < iDataWidth; i++) {
                    //Get colour indices from pixel map
                    ushort indices = tim.ReadUInt16();

                    ushort mask = 0xF;
                    ushort xloc = 0;
                    ushort xoff = 0;
                    ushort yloc = 0;

                    for (ushort k = 0; k < 4; ++k) {
                        TIMColour col = lClut[(indices & mask) >> (4 * k)];

                        yloc = (ushort)((iWidth * (iDataHeight - (j+1))) * 4);
                        xloc = (ushort)(16 * i);
                        xoff = (ushort)(4 * k);

                        //Write Pixel
                        pBitMap[yloc + (xloc + xoff) + 0] = col.R;
                        pBitMap[yloc + (xloc + xoff) + 1] = col.G;
                        pBitMap[yloc + (xloc + xoff) + 2] = col.B;
                        pBitMap[yloc + (xloc + xoff) + 3] = col.A;

                        mask = (ushort) (mask << 4);
                    }
                }
            }
        }

        //
        //Utility functions for Tim Loading
        //
        private static void TIMERR(string err) {
            Console.WriteLine("TimTexture --> Error: ");
            Console.WriteLine("    '" + err + "'");
        }
    }
}
