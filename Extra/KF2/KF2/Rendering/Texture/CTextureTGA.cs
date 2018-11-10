using System;
using System.Threading;
using System.IO;

namespace KF2.Rendering.Texture
{
    class CTextureTGA : CTexture2D
    {
        private Thread tHandle;

        public CTextureTGA(string fpath)
        {
            tHandle = new Thread(() => LoadTGA(fpath));
            tHandle.Start();
        }

        private void LoadTGA(string fpath)
        {
            BinaryReader brFile;

            //Open the file
            try
            {
                brFile = new BinaryReader(File.Open(fpath, FileMode.Open));
            }
            catch (Exception E)
            {
                throw E;
            }

            //Read the file
            brFile.ReadUInt16();

            ushort compmeth = brFile.ReadUInt16();
            if(compmeth != 2)
            {
                throw (new Exception("Can't load compressed TGA."));
            }

            brFile.ReadUInt32();
            brFile.ReadUInt32();
            iWidth = brFile.ReadUInt16();
            iHeight = brFile.ReadUInt16();

            byte bpp = brFile.ReadByte();
            brFile.ReadByte();

            pBitMap = new byte[(iWidth * iHeight) * 4];

            int pX = 0;
            int pY = 0;

            for (int yy = 0; yy < iHeight; ++yy) {
                pY = (int)(((4 * iWidth) * iHeight) - ((4 * iWidth) * (yy+1)));

                for (int xx = 0; xx < iWidth; ++xx) {
                    pX = 4 * xx;                   

                    switch(bpp)
                    {
                        case 24:
                            pBitMap[(pY + pX) + 2] = brFile.ReadByte();
                            pBitMap[(pY + pX) + 1] = brFile.ReadByte();
                            pBitMap[(pY + pX) + 0] = brFile.ReadByte();
                            pBitMap[(pY + pX) + 3] = 0xFF;
                            break;

                        default:
                            throw (new Exception("Non 24BPP TGA is not supported."));
                    }
                }
            }

            bReady = true;
        }
    }
}
