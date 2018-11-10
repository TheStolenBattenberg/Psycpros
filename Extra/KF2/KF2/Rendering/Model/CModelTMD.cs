using System;
using System.Collections.Generic;
using System.IO;

using OpenTK;

namespace KF2.Rendering.Model {
    public struct TMDVertex
    {
        float x, y, z;

        public TMDVertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Position
        {
            get
            {
                return new Vector3(x, y, z);
            }
        }
    }
    public struct TMDNormal
    {
        float x, y, z;

        public TMDNormal(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public struct TMDObject
    {
        uint VertexCount, VertexOffset;
        uint NormalCount, NormalOffset;
        uint PrimitiveCount, PrimitiveOffset;
        int Scale;

        public TMDObject(uint vc, uint vo, uint nc, uint no, uint pc, uint po, int s) {
            VertexCount = vc;
            VertexOffset = vo;
            NormalCount = nc;
            NormalOffset = no;
            PrimitiveCount = pc;
            PrimitiveOffset = po;
            Scale = s;
        }

        public uint VCount
        {
            get
            {
                return VertexCount;
            }
        }
        public uint VOffset
        {
            get
            {
                return VertexOffset;
            }
        }
        public uint PCount
        {
            get
            {
                return PrimitiveCount;
            }
        }
        public uint POffset
        {
            get
            {
                return PrimitiveOffset;
            }
        }
    }

    public class CModelTMD : CMPMStatic {
        private uint iOffset = 0x0C;
        private uint iNumObject = 0;

        private List<TMDObject> lObject;
        private Dictionary<uint, List<TMDVertex>> lTMDVertex;


        /**
         * This constuctor forces loading of a TMD file from a file.
        **/
        public CModelTMD (string fname, bool threaded)
        {
            LoadFromFile(fname);

            Build();
        }


        /**
         * This method will load the TMD data from a file.
        **/
        private void LoadFromFile (string fname)
        {
            BinaryReader rBin = OpenTMD(fname);

            if(!VarifyTMD(rBin))
            {
                rBin.Close();
                return;
            }

            ReadTMDObjects(rBin);
            ReadTMDVertices(rBin);
            ReadTMDPrimitives(rBin);

            lObject.Clear();
            lTMDVertex.Clear();
        }


        /**
         * This method opens the possible TMD file for reading.
        **/
        private BinaryReader OpenTMD (string fname)
        {
            BinaryReader tmdFile;

            try
            {
                tmdFile = new BinaryReader(File.Open(fname, FileMode.Open));
            }
            catch (Exception E)
            {
                throw E;
            }

            return tmdFile;
        }


        /**
         * This method varifies that a TMD file is actually a TMD.
         * It does this by checking the TMD ID, and the TMD flags.
         * 
         * Refer to the PDF 'File Format 47' from the PsyQ SDK for more information.
        **/
        private bool VarifyTMD (BinaryReader tmd)
        {
            uint tmdID    = tmd.ReadUInt32();
            uint tmdFlags = tmd.ReadUInt32();
            uint tmdFIXP  = (tmdFlags & 0x1);

            //If 'tmdID' doesn't match 0x41, throw an exception.
            if(tmdID != 0x41)
            {
                return false;
            }

            //If 'tmdFIXP' isn't 0, we cannot load it.
            if(tmdFIXP != 0)
            {
                return false;
            }
            return true;
        }


        /**
         * This method will load the object table from the TMD file.
         * It also reads the object count and initilizes 'lObject'.
         * 
         * Refer to the PDF 'File Format 47' from the PsyQ SDK for more information.
        **/
        private void ReadTMDObjects (BinaryReader tmd)
        {
            iNumObject = tmd.ReadUInt32();
            lObject = new List<TMDObject>((int) iNumObject);
            lTMDVertex = new Dictionary<uint, List<TMDVertex>>();

            lVertex = new List<List<MPMVertex>>();
            lVertexAlpha = new List<List<MPMVertex>>();

            //Read each object, then place it into the list. 'Figure 2-38: OBJ TABLE structure'
            for (uint i = 0; i < iNumObject; ++i)
            {
                uint objVptr  = tmd.ReadUInt32();
                uint objVnum  = tmd.ReadUInt32();
                uint objNptr  = tmd.ReadUInt32();
                uint objNnum  = tmd.ReadUInt32();
                uint objPptr  = tmd.ReadUInt32();
                uint objPnum  = tmd.ReadUInt32();
                int  objScale = tmd.ReadInt32();

                TMDObject obj = new TMDObject(0, 0, 0, 0, 0, 0, 0);
                lObject.Add(
                    new TMDObject(objVnum, iOffset + objVptr, objNnum, iOffset + objNptr, objPnum, iOffset + objPptr, objScale));
            }
        }


        /**
         * This method will read vertices from the TMD per object.
         * 
         * Refer to the PDF 'File Format 47' from the PsyQ SDK for more information.
        **/
        private void ReadTMDVertices (BinaryReader tmd)
        {
            for(uint i = 0; i < iNumObject; ++i)
            {
                tmd.BaseStream.Seek(lObject[(int)i].VOffset, SeekOrigin.Begin);

                lTMDVertex[i] = new List<TMDVertex>((int)lObject[(int)i].VCount);

                for(uint j = 0; j < lObject[(int)i].VCount; ++j)
                {
                    float pX = (tmd.ReadInt16() / 1024.0f) * 32.0f;
                    float pY = (tmd.ReadInt16() / 1024.0f) * 32.0f;
                    float pZ = (tmd.ReadInt16() / 1024.0f) * 32.0f;
                    tmd.ReadInt16();

                    lTMDVertex[i].Add(new TMDVertex(pX, pY, pZ));
                }
            }
        }


        /**
         * This method will read primitives from the TMD per object.
         * It adds them to the base MPMStatic.
         * 
         * Refer to the PDF 'File Format 47' from the PsyQ SDK for more information.
        **/
        private void ReadTMDPrimitives (BinaryReader tmd)
        {
            for (uint i = 0; i < iNumObject; ++i)
            {
                List<MPMVertex> lPrim      = new List<MPMVertex>();
                List<MPMVertex> lPrimAlpha = new List<MPMVertex>();

                ushort TSB = 24;
                ushort CBA = 24;

                tmd.BaseStream.Seek(lObject[(int)i].POffset, SeekOrigin.Begin);

                for (uint j = 0; j < lObject[(int)i].PCount; ++j)
                {
                    //Read Primitive Header
                    byte OLEN = tmd.ReadByte();
                    byte ILEN = tmd.ReadByte();
                    byte FLAG = tmd.ReadByte();
                    byte MODE = tmd.ReadByte();

                    byte mCode   = (byte)((MODE >> 5) & 0x7);
                    byte mOption = (byte)(MODE & 0x1F);

                    byte moTGE = (byte)((mOption >> 0) & 0x1); //Set when brightness calculation is performed
                    byte moABE = (byte)((mOption >> 1) & 0x1); //Set when transparent
                    byte moTME = (byte)((mOption >> 2) & 0x1); //Set when textured
                    byte moQUD = (byte)((mOption >> 3) & 0x1); //Set when Quad
                    byte moIIP = (byte)((mOption >> 4) & 0x1); //Shading Mode (Flat, Goraud)

                    byte fgGRD = (byte)((FLAG >> 2) & 0x1);    //Set when gradient colour
                    byte fgFCE = (byte)((FLAG >> 1) & 0x1);    //Set when double sided
                    byte fgLGT = (byte)((FLAG >> 0) & 0X1);    //Set when shaded


                    //Read Primitive Information
                    switch(mCode)
                    {
                        case 1: //Polygon data
                            switch (moQUD)
                            {
                                case 0: //3 Point Primitive
                                    MPMVertex vertex1 = new MPMVertex(); vertex1.Default();
                                    MPMVertex vertex2 = new MPMVertex(); vertex2.Default();
                                    MPMVertex vertex3 = new MPMVertex(); vertex3.Default();

                                    if (fgLGT == 0) //Data is Lit
                                    {
                                        if (moTME == 1)
                                        {
                                            float u1 = tmd.ReadByte() / 255.0f;
                                            float v1 = tmd.ReadByte() / 255.0f;
                                            CBA = tmd.ReadUInt16();
                                            float u2 = tmd.ReadByte() / 255.0f;
                                            float v2 = tmd.ReadByte() / 255.0f;
                                            TSB = tmd.ReadUInt16();
                                            float u3 = tmd.ReadByte() / 255.0f;
                                            float v3 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadUInt16();

                                            vertex1.Texture = new Vector3(u1, v1, TSB & 0x1F);
                                            vertex2.Texture = new Vector3(u2, v2, TSB & 0x1F);
                                            vertex3.Texture = new Vector3(u3, v3, TSB & 0X1f);
                                        }
                                        else
                                        {
                                            if (fgGRD == 1)
                                            {
                                                float r1 = tmd.ReadByte() / 255.0f;
                                                float g1 = tmd.ReadByte() / 255.0f;
                                                float b1 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                float r2 = tmd.ReadByte() / 255.0f;
                                                float g2 = tmd.ReadByte() / 255.0f;
                                                float b2 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                float r3 = tmd.ReadByte() / 255.0f;
                                                float g3 = tmd.ReadByte() / 255.0f;
                                                float b3 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                vertex1.Colour = new Vector3(r1, g1, b1);
                                                vertex2.Colour = new Vector3(r2, g2, b2);
                                                vertex3.Colour = new Vector3(r3, g3, b3);
                                            }
                                            else
                                            {
                                                float r1 = tmd.ReadByte() / 255.0f;
                                                float g1 = tmd.ReadByte() / 255.0f;
                                                float b1 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                vertex1.Colour = new Vector3(r1, g1, b1);
                                                vertex2.Colour = new Vector3(r1, g1, b1);
                                                vertex3.Colour = new Vector3(r1, g1, b1);
                                            }
                                        }

                                        if (moIIP == 1)
                                        {
                                            tmd.ReadUInt16();
                                            ushort vi1 = tmd.ReadUInt16();
                                            tmd.ReadUInt16();
                                            ushort vi2 = tmd.ReadUInt16();
                                            tmd.ReadUInt16();
                                            ushort vi3 = tmd.ReadUInt16();

                                            vertex1.Position = lTMDVertex[i][vi1].Position;
                                            vertex2.Position = lTMDVertex[i][vi2].Position;
                                            vertex3.Position = lTMDVertex[i][vi3].Position;

                                            vertex1.Normal = Vector3.Cross(
                                                vertex2.Position - vertex1.Position,
                                                vertex3.Position - vertex1.Position); //Generate flat normal
                                            vertex2.Normal = vertex1.Normal;
                                            vertex3.Normal = vertex1.Normal;
                                        }
                                        else
                                        {
                                            tmd.ReadUInt16();
                                            ushort vi1 = tmd.ReadUInt16();
                                            ushort vi2 = tmd.ReadUInt16();
                                            ushort vi3 = tmd.ReadUInt16();

                                            vertex1.Position = lTMDVertex[i][vi1].Position;
                                            vertex2.Position = lTMDVertex[i][vi2].Position;
                                            vertex3.Position = lTMDVertex[i][vi3].Position;

                                            vertex1.Normal = Vector3.Cross(
                                                vertex2.Position - vertex1.Position,
                                                vertex3.Position - vertex1.Position); //Generate flat normal
                                            vertex2.Normal = vertex1.Normal;
                                            vertex3.Normal = vertex1.Normal;
                                        }
                                    }
                                    else //Data is Unlit
                                    {
                                        if (moTME == 1)
                                        {
                                            float u1 = tmd.ReadByte() / 255.0f;
                                            float v1 = tmd.ReadByte() / 255.0f;
                                            CBA = tmd.ReadUInt16();
                                            float u2 = tmd.ReadByte() / 255.0f;
                                            float v2 = tmd.ReadByte() / 255.0f;
                                            TSB = tmd.ReadUInt16();
                                            float u3 = tmd.ReadByte() / 255.0f;
                                            float v3 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadUInt16();

                                            vertex1.Texture = new Vector3(u1, v1, TSB & 0x1F);
                                            vertex2.Texture = new Vector3(u2, v2, TSB & 0x1F);
                                            vertex3.Texture = new Vector3(u3, v3, TSB & 0X1f);
                                        }

                                        if (fgGRD == 1)
                                        {
                                            float r1 = tmd.ReadByte() / 255.0f;
                                            float g1 = tmd.ReadByte() / 255.0f;
                                            float b1 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            float r2 = tmd.ReadByte() / 255.0f;
                                            float g2 = tmd.ReadByte() / 255.0f;
                                            float b2 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            float r3 = tmd.ReadByte() / 255.0f;
                                            float g3 = tmd.ReadByte() / 255.0f;
                                            float b3 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            vertex1.Colour = new Vector3(r1, g1, b1);
                                            vertex2.Colour = new Vector3(r2, g2, b2);
                                            vertex3.Colour = new Vector3(r3, g3, b3);
                                        }
                                        else
                                        {
                                            float r1 = tmd.ReadByte() / 255.0f;
                                            float g1 = tmd.ReadByte() / 255.0f;
                                            float b1 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            vertex1.Colour = new Vector3(r1, g1, b1);
                                            vertex2.Colour = new Vector3(r1, g1, b1);
                                            vertex3.Colour = new Vector3(r1, g1, b1);

                                        }

                                        ushort vi1 = tmd.ReadUInt16();
                                        ushort vi2 = tmd.ReadUInt16();
                                        ushort vi3 = tmd.ReadUInt16();
                                        tmd.ReadUInt16();

                                        vertex1.Position = lTMDVertex[i][vi1].Position;
                                        vertex2.Position = lTMDVertex[i][vi2].Position;
                                        vertex3.Position = lTMDVertex[i][vi3].Position;

                                        vertex1.Normal = Vector3.Cross(
                                            vertex2.Position - vertex1.Position,
                                            vertex3.Position - vertex1.Position); //Generate flat normal
                                        vertex2.Normal = vertex1.Normal;
                                        vertex3.Normal = vertex1.Normal;
                                    }

                                    vertex1.Alpha = 1.0f - (0.5f * moABE);
                                    vertex2.Alpha = 1.0f - (0.5f * moABE);
                                    vertex3.Alpha = 1.0f - (0.5f * moABE);

                                    if (moABE == 0)
                                    {
                                        lPrim.Add(vertex3);
                                        lPrim.Add(vertex2);
                                        lPrim.Add(vertex1);

                                        if (fgFCE == 1)
                                        {
                                            lPrim.Add(vertex1);
                                            lPrim.Add(vertex2);
                                            lPrim.Add(vertex3);
                                        }
                                    }else
                                    {
                                        lPrimAlpha.Add(vertex3);
                                        lPrimAlpha.Add(vertex2);
                                        lPrimAlpha.Add(vertex1);

                                        if (fgFCE == 1)
                                        {
                                            lPrimAlpha.Add(vertex1);
                                            lPrimAlpha.Add(vertex2);
                                            lPrimAlpha.Add(vertex3);
                                        }
                                    }
                                    break;

                                case 1: //4 Point Primitive
                                    MPMVertex qvertex1 = new MPMVertex(); qvertex1.Default();
                                    MPMVertex qvertex2 = new MPMVertex(); qvertex2.Default();
                                    MPMVertex qvertex3 = new MPMVertex(); qvertex3.Default();
                                    MPMVertex qvertex4 = new MPMVertex(); qvertex4.Default();

                                    if (fgLGT == 0) //Data is Lit
                                    {
                                        if (moTME == 1)
                                        {
                                            float u1 = tmd.ReadByte() / 255.0f;
                                            float v1 = tmd.ReadByte() / 255.0f;
                                            CBA = tmd.ReadUInt16();
                                            float u2 = tmd.ReadByte() / 255.0f;
                                            float v2 = tmd.ReadByte() / 255.0f;
                                            TSB = tmd.ReadUInt16();
                                            float u3 = tmd.ReadByte() / 255.0f;
                                            float v3 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadUInt16();
                                            float u4 = tmd.ReadByte() / 255.0f;
                                            float v4 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadUInt16();


                                            qvertex1.Texture = new Vector3(u1, v1, TSB & 0x1F);
                                            qvertex2.Texture = new Vector3(u2, v2, TSB & 0x1F);
                                            qvertex3.Texture = new Vector3(u3, v3, TSB & 0x1f);
                                            qvertex4.Texture = new Vector3(u4, v4, TSB & 0x1f);
                                        }
                                        else
                                        {
                                            if (fgGRD == 1)
                                            {
                                                float r1 = tmd.ReadByte() / 255.0f;
                                                float g1 = tmd.ReadByte() / 255.0f;
                                                float b1 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                float r2 = tmd.ReadByte() / 255.0f;
                                                float g2 = tmd.ReadByte() / 255.0f;
                                                float b2 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                float r3 = tmd.ReadByte() / 255.0f;
                                                float g3 = tmd.ReadByte() / 255.0f;
                                                float b3 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                float r4 = tmd.ReadByte() / 255.0f;
                                                float g4 = tmd.ReadByte() / 255.0f;
                                                float b4 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                qvertex1.Colour = new Vector3(r1, g1, b1);
                                                qvertex2.Colour = new Vector3(r2, g2, b2);
                                                qvertex3.Colour = new Vector3(r3, g3, b3);
                                                qvertex4.Colour = new Vector3(r4, g4, b4);
                                            }
                                            else
                                            {
                                                float r1 = tmd.ReadByte() / 255.0f;
                                                float g1 = tmd.ReadByte() / 255.0f;
                                                float b1 = tmd.ReadByte() / 255.0f;
                                                tmd.ReadByte();

                                                qvertex1.Colour = new Vector3(r1, g1, b1);
                                                qvertex2.Colour = new Vector3(r1, g1, b1);
                                                qvertex3.Colour = new Vector3(r1, g1, b1);
                                                qvertex4.Colour = new Vector3(r1, g1, b1);
                                            }
                                        }

                                        if (moIIP == 1)
                                        {
                                            tmd.ReadUInt16();
                                            ushort vi1 = tmd.ReadUInt16();
                                            tmd.ReadUInt16();
                                            ushort vi2 = tmd.ReadUInt16();
                                            tmd.ReadUInt16();
                                            ushort vi3 = tmd.ReadUInt16();
                                            tmd.ReadUInt16();
                                            ushort vi4 = tmd.ReadUInt16();

                                            qvertex1.Position = lTMDVertex[i][vi1].Position;
                                            qvertex2.Position = lTMDVertex[i][vi2].Position;
                                            qvertex3.Position = lTMDVertex[i][vi3].Position;
                                            qvertex4.Position = lTMDVertex[i][vi4].Position;

                                            qvertex1.Normal = Vector3.Cross(
                                                qvertex2.Position - qvertex1.Position,
                                                qvertex3.Position - qvertex1.Position); //Generate flat normal
                                            qvertex2.Normal = qvertex1.Normal;
                                            qvertex3.Normal = qvertex1.Normal;
                                            qvertex4.Normal = qvertex1.Normal;
                                        }
                                        else
                                        {
                                            tmd.ReadUInt16();
                                            ushort vi1 = tmd.ReadUInt16();
                                            ushort vi2 = tmd.ReadUInt16();
                                            ushort vi3 = tmd.ReadUInt16();
                                            ushort vi4 = tmd.ReadUInt16();
                                            tmd.ReadUInt16();

                                            qvertex1.Position = lTMDVertex[i][vi1].Position;
                                            qvertex2.Position = lTMDVertex[i][vi2].Position;
                                            qvertex3.Position = lTMDVertex[i][vi3].Position;
                                            qvertex4.Position = lTMDVertex[i][vi4].Position;

                                            qvertex1.Normal = Vector3.Cross(
                                                qvertex2.Position - qvertex1.Position,
                                                qvertex3.Position - qvertex1.Position); //Generate flat normal
                                            qvertex2.Normal = qvertex1.Normal;
                                            qvertex3.Normal = qvertex1.Normal;
                                            qvertex4.Normal = qvertex1.Normal;
                                        }
                                    }
                                    else //Data is Unlit
                                    {
                                        if (moTME == 1)
                                        {
                                            float u1 = tmd.ReadByte() / 255.0f;
                                            float v1 = tmd.ReadByte() / 255.0f;
                                            CBA = tmd.ReadUInt16();
                                            float u2 = tmd.ReadByte() / 255.0f;
                                            float v2 = tmd.ReadByte() / 255.0f;
                                            TSB = tmd.ReadUInt16();
                                            float u3 = tmd.ReadByte() / 255.0f;
                                            float v3 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadUInt16();
                                            float u4 = tmd.ReadByte() / 255.0f;
                                            float v4 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadUInt16();


                                            qvertex1.Texture = new Vector3(u1, v1, TSB & 0x1F);
                                            qvertex2.Texture = new Vector3(u2, v2, TSB & 0x1F);
                                            qvertex3.Texture = new Vector3(u3, v3, TSB & 0x1f);
                                            qvertex4.Texture = new Vector3(u4, v4, TSB & 0x1f);
                                        }

                                        if (fgGRD == 1)
                                        {
                                            float r1 = tmd.ReadByte() / 255.0f;
                                            float g1 = tmd.ReadByte() / 255.0f;
                                            float b1 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            float r2 = tmd.ReadByte() / 255.0f;
                                            float g2 = tmd.ReadByte() / 255.0f;
                                            float b2 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            float r3 = tmd.ReadByte() / 255.0f;
                                            float g3 = tmd.ReadByte() / 255.0f;
                                            float b3 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            float r4 = tmd.ReadByte() / 255.0f;
                                            float g4 = tmd.ReadByte() / 255.0f;
                                            float b4 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            qvertex1.Colour = new Vector3(r1, g1, b1);
                                            qvertex2.Colour = new Vector3(r2, g2, b2);
                                            qvertex3.Colour = new Vector3(r3, g3, b3);
                                            qvertex4.Colour = new Vector3(r4, g4, b4);
                                        }
                                        else
                                        {
                                            float r1 = tmd.ReadByte() / 255.0f;
                                            float g1 = tmd.ReadByte() / 255.0f;
                                            float b1 = tmd.ReadByte() / 255.0f;
                                            tmd.ReadByte();

                                            qvertex1.Colour = new Vector3(r1, g1, b1);
                                            qvertex2.Colour = new Vector3(r1, g1, b1);
                                            qvertex3.Colour = new Vector3(r1, g1, b1);
                                            qvertex4.Colour = new Vector3(r1, g1, b1);
                                        }

                                        ushort vi1 = tmd.ReadUInt16();
                                        ushort vi2 = tmd.ReadUInt16();
                                        ushort vi3 = tmd.ReadUInt16();
                                        ushort vi4 = tmd.ReadUInt16();

                                        qvertex1.Position = lTMDVertex[i][vi1].Position;
                                        qvertex2.Position = lTMDVertex[i][vi2].Position;
                                        qvertex3.Position = lTMDVertex[i][vi3].Position;
                                        qvertex4.Position = lTMDVertex[i][vi4].Position;

                                        qvertex1.Normal = Vector3.Cross(
                                            qvertex2.Position - qvertex1.Position,
                                            qvertex3.Position - qvertex1.Position); //Generate flat normal

                                        qvertex2.Normal = qvertex1.Normal;
                                        qvertex3.Normal = qvertex1.Normal;
                                        qvertex4.Normal = qvertex1.Normal;
                                    }

                                    qvertex1.Alpha = 1.0f - (0.5f * moABE);
                                    qvertex2.Alpha = 1.0f - (0.5f * moABE);
                                    qvertex3.Alpha = 1.0f - (0.5f * moABE);
                                    qvertex4.Alpha = 1.0f - (0.5f * moABE);

                                    if (moABE == 0)
                                    {
                                        lPrim.Add(qvertex3);
                                        lPrim.Add(qvertex2);
                                        lPrim.Add(qvertex1);

                                        lPrim.Add(qvertex2);
                                        lPrim.Add(qvertex3);
                                        lPrim.Add(qvertex4);

                                        if (fgFCE == 1)
                                        {
                                            lPrim.Add(qvertex1);
                                            lPrim.Add(qvertex2);
                                            lPrim.Add(qvertex3);

                                            lPrim.Add(qvertex4);
                                            lPrim.Add(qvertex3);
                                            lPrim.Add(qvertex2);
                                        }
                                    }else
                                    {
                                        lPrimAlpha.Add(qvertex3);
                                        lPrimAlpha.Add(qvertex2);
                                        lPrimAlpha.Add(qvertex1);

                                        lPrimAlpha.Add(qvertex2);
                                        lPrimAlpha.Add(qvertex3);
                                        lPrimAlpha.Add(qvertex4);

                                        if (fgFCE == 1)
                                        {
                                            lPrimAlpha.Add(qvertex1);
                                            lPrimAlpha.Add(qvertex2);
                                            lPrimAlpha.Add(qvertex3);

                                            lPrimAlpha.Add(qvertex4);
                                            lPrimAlpha.Add(qvertex3);
                                            lPrimAlpha.Add(qvertex2);
                                        }
                                    }
                                    break;
                            }
                            break;

                        case 2: //Line data
                            throw (new Exception("Line Primitives are not supported."));

                        case 3: //Sprite Data
                            throw (new Exception("Sprite Primitives are not supported."));

                        default: //Unknown Data
                            throw (new Exception("Unknown Primitive!"));
                    }
                }

                lVertex.Add(lPrim);
                lVertexAlpha.Add(lPrimAlpha);
            }
        }
    }
}
