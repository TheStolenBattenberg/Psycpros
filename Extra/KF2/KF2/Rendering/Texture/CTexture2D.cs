using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL4;
namespace KF2.Rendering.Texture {
    public class CTexture2D {
        //Store a list of which stages this texture occupies.
        private List<uint> lStages;

        //Store the OpenGL texture ID.
        private int iTextureID = -8;

        //Thread Ready boolean.
        protected bool bReady;

        //Texture Info
        protected uint iWidth;
        protected uint iHeight;

        //Bitmap
        protected byte[] pBitMap;

        //
        // Constructor
        //
        public CTexture2D() {
            //Create the stage list.
            lStages = new List<uint>();
        }

        //
        // Function Generates a 2x2 black & white checkerboard.
        //
        public void GenerateBasic() {
            //Create OGL Texture
            iTextureID = GL.GenTexture();

            //Size width and height accordingly.
            iWidth  = 2;
            iHeight = 2;

            //Create temporary array with bitmap data
            float[] pixelData = {
                0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f,
            };

            //Generate the OpenGL texture
            GL.BindTexture(TextureTarget.Texture2D, iTextureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexImage2D(TextureTarget.Texture2D, //Target Texture
                0, //Level 0 = MipMap 0
                PixelInternalFormat.Rgb,
                (int)iWidth,
                (int)iHeight,
                0,
                PixelFormat.Rgb,
                PixelType.Float,
                pixelData);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            bReady = true;
        }

        public void Generate() {
            //Create OGL Texture
            iTextureID = GL.GenTexture();

            //Generate the OpenGL texture
            GL.BindTexture(TextureTarget.Texture2D, iTextureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapNearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8.0f);

            GL.TexImage2D(TextureTarget.Texture2D, //Target Texture
                0, //Level 0 = MipMap 0
                PixelInternalFormat.Rgba,
                (int)iWidth,
                (int)iHeight,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pBitMap);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void SetStage(uint Stage) {
            if(!bReady) { return; }
            if(bReady & iTextureID == -8) {
                Generate();
            }

            lStages.Add((33984 + Stage));
            GL.ActiveTexture((TextureUnit)(33984 + Stage));
            GL.BindTexture(TextureTarget.Texture2D, iTextureID);
        }
        public void Reset() {
            //Loop through all the stages this texture occupies and unbind them.
            for(int i = 0; i < lStages.Count; ++i) {
                GL.ActiveTexture((TextureUnit)lStages[i]);
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }

            //Clear Bound Texture List
            lStages.Clear();
        }

        public uint GetWidth() {
            return iWidth;
        }
        public uint GetHeight() {
            return iHeight;
        }
        public uint GetPixelFormat() {
            return 0;
        }
    }
}
