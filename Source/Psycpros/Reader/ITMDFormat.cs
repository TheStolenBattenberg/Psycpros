using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Psycpros.Psycode;

namespace Psycpros.Reader {
    struct TMDOBJ {
        public uint vert_top;
        public uint n_vert;
        public uint normal_top;
        public uint n_normal;
        public uint primitive_top;
        public uint n_primitive;
        public int scale;
    };

    class ITMDFormat {
        private List<ISimpleMesh> pMesh;

        /**
         * Constructor 
        **/
        public ITMDFormat() {
            pMesh = new List<ISimpleMesh>();
        }

        /**
         * Import 
        **/
        public bool ImportFromFile(string filename) {
            //Try to open & read the file.
            try {
                //Open file in a binary reader.
                BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open));

                //Varify TMD File.
                uint mN = b.ReadUInt32();
                if(mN != 0x00000041) {
                    //File is wrong, close buffer and throw exception.
                    b.Close();
                    throw new Exception("Not a TMD file!");
                }

                //Read Header
                uint flags = b.ReadUInt32();
                uint nObj  = b.ReadUInt32();

                //Print Header [Debug]
                Console.WriteLine("TMDFile -> Starting Import...");
                Console.WriteLine("    ID: " + mN.ToString());
                Console.WriteLine("    Flags: " + flags.ToString());
                Console.WriteLine("    NumObj: " + nObj.ToString());

                //Read Object Table
                Console.WriteLine();
                Console.WriteLine("TMDFile -> Reading Object Table...");
                TMDOBJ[] obj = new TMDOBJ[nObj];
                for(uint i = 0; i < nObj; ++i) {
                    obj[i].vert_top = b.ReadUInt32();
                    obj[i].n_vert = b.ReadUInt32();
                    obj[i].normal_top = b.ReadUInt32();
                    obj[i].n_normal = b.ReadUInt32();
                    obj[i].primitive_top = b.ReadUInt32();
                    obj[i].n_primitive = b.ReadUInt32();
                    obj[i].scale = b.ReadInt32();

                    Console.WriteLine("TMDFile -> Object Index [" + i.ToString() + "] {");
                    Console.WriteLine("    Vertex Number: " + obj[i].n_vert.ToString());
                    Console.WriteLine("    Normal Number: " + obj[i].n_normal.ToString());
                    Console.WriteLine("    Primitive Number: " + obj[i].n_primitive.ToString());
                    Console.WriteLine("}");
                }
                Console.WriteLine("TMDFile -> Done.");

                Console.WriteLine();
                Console.WriteLine("TMDFile -> Reading Objects.");
                //Read Objects
                uint seek;
                for(uint i = 0; i < nObj; ++i) {
                    ISimpleMesh tObj = new ISimpleMesh();

                    //Relative or direct seek?
                    seek = (uint)((flags == 0) ? 0x0C : 0x00);

                    //Read Vertices
                    b.BaseStream.Seek(seek + obj[i].vert_top, SeekOrigin.Begin);
                    for(uint j = 0; j < obj[i].n_vert; ++j) {
                        float x = (float) b.ReadInt16() * 0.01f;
                        float y = (float) b.ReadInt16() * 0.01f;
                        float z = (float) b.ReadInt16() * 0.01f;
                        b.ReadInt16();

                        tObj.VertexAdd(x, y, z);
                    }

                    //Read Normals
                    b.BaseStream.Seek(seek + obj[i].normal_top, SeekOrigin.Begin);
                    for(uint j = 0; j < obj[i].n_normal; ++j) {
                        float nx = (float) b.ReadInt16() / 4096.0f;
                        float ny = (float) b.ReadInt16() / 4096.0f;
                        float nz = (float) b.ReadInt16() / 4096.0f;
                        b.ReadInt16();

                        tObj.NormalAdd(nx, ny, nz);
                    }

                    //Read Primitives
                    b.BaseStream.Seek(seek + obj[i].primitive_top, SeekOrigin.Begin);
                    for(uint j = 0; j < obj[i].n_primitive; ++j) {
                        //Read Primitive ID
                        uint primID = b.ReadUInt32();

                        Primitive prim;
                        float u0 = 0, v0 = 0, u1 = 0, v1 = 0, u2 = 0, v2 = 0, u3 = 0, v3 = 0;
                        ushort nInd0, nInd1, nInd2, nInd3;
                        ushort vInd0, vInd1, vInd2, vInd3;
                        byte r0 = 255, g0 = 255, b0 = 255, 
                             r1 = 255, g1 = 255, b1 = 255, 
                             r2 = 255, g2 = 255, b2 = 255, 
                             r3 = 255, g3 = 255, b3 = 255;
                        byte alpha = 255;


                        switch (primID) {
                            case 536871684: //3-POINT, FLAT, SOLID-COLOUR
                                Console.WriteLine("    Reading Primitive: 3-POINT, FLAT, SOLID-COLOUR");
                                r0 = b.ReadByte();
                                g0 = b.ReadByte();
                                b0 = b.ReadByte();
                                b.ReadByte();

                                //Read indices
                                nInd0 = b.ReadUInt16();
                                vInd0 = b.ReadUInt16();
                                vInd1 = b.ReadUInt16();
                                vInd2 = b.ReadUInt16();

                                prim = new Primitive(vInd0, vInd1, vInd2);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddNormal(nInd0);
                                prim.AddNormal(nInd0);
                                prim.AddNormal(nInd0);
                                prim.AddTexcoord(0, 0);
                                prim.AddTexcoord(0, 0);
                                prim.AddTexcoord(0, 0);
                                prim.SetAlpha(alpha);
                                break;

                            case 805307398: //3-POINT, SHADED, SOLID-COLOUR
                                Console.WriteLine("    Reading Primitive: 3-POINT, SHADED, SOLID-COLOUR");
                                r0 = b.ReadByte();
                                g0 = b.ReadByte();
                                b0 = b.ReadByte();
                                b.ReadByte();

                                //Read indices
                                nInd0 = b.ReadUInt16();
                                vInd0 = b.ReadUInt16();
                                nInd1 = b.ReadUInt16();
                                vInd1 = b.ReadUInt16();
                                nInd2 = b.ReadUInt16();
                                vInd2 = b.ReadUInt16();

                                prim = new Primitive(vInd0, vInd1, vInd2);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddNormal(nInd0);
                                prim.AddNormal(nInd1);
                                prim.AddNormal(nInd2);
                                prim.AddTexcoord(0, 0);
                                prim.AddTexcoord(0, 0);
                                prim.AddTexcoord(0, 0);
                                prim.SetAlpha(alpha);
                                break;

                            case 637535495: //3-POINT, FLAT, TEXTURE, SEMI-TRANSPARENT
                            case 603981063: //3-POINT, FLAT, TEXTURE
                                Console.WriteLine("    Reading Primitive: 3-POINT, FLAT, TEXTURE");

                                //Read Primitive Texture Data.
                                u0 = (float)b.ReadByte() / 255.0f;
                                v0 = (float)b.ReadByte() / 255.0f;
                                b.ReadUInt16();
                                u1 = (float)b.ReadByte() / 255.0f;
                                v1 = (float)b.ReadByte() / 255.0f;
                                b.ReadUInt16();
                                u2 = (float)b.ReadByte() / 255.0f;
                                v2 = (float)b.ReadByte() / 255.0f;
                                b.ReadUInt16();

                                //Read Indices
                                nInd0 = b.ReadUInt16();
                                vInd0 = b.ReadUInt16();
                                vInd1 = b.ReadUInt16();
                                vInd2 = b.ReadUInt16();

                                //Build primitive
                                prim = new Primitive(vInd0, vInd1, vInd2);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddNormal(nInd0);
                                prim.AddNormal(nInd0);
                                prim.AddNormal(nInd0);
                                prim.AddTexcoord(u0, v0);
                                prim.AddTexcoord(u1, v1);
                                prim.AddTexcoord(u2, v2);
                                prim.SetAlpha(alpha);
                                break;

                            case 905971209: //3-POINT, SHADED, TEXTURE, SEMI-TRANSPARENT
                            case 872416777: //3-POINT, SHADED, TEXTURE
                                Console.WriteLine("    Reading Primitive: 3-POINT, SHADED, TEXTURE");

                                //Read Primitive Texture Data.
                                u0 = (float)b.ReadByte() / 255.0f;
                                v0 = (float)b.ReadByte() / 255.0f;
                                b.ReadUInt16();
                                u1 = (float)b.ReadByte() / 255.0f;
                                v1 = (float)b.ReadByte() / 255.0f;
                                b.ReadUInt16();
                                u2 = (float)b.ReadByte() / 255.0f;
                                v2 = (float)b.ReadByte() / 255.0f;
                                b.ReadUInt16();

                                //Read Indices
                                nInd0 = b.ReadUInt16();
                                vInd0 = b.ReadUInt16();
                                nInd1 = b.ReadUInt16();
                                vInd1 = b.ReadUInt16();
                                nInd2 = b.ReadUInt16();
                                vInd2 = b.ReadUInt16();

                                prim = new Primitive(vInd0, vInd1, vInd2);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddColour(r0, g0, b0);
                                prim.AddNormal(nInd0);
                                prim.AddNormal(nInd1);
                                prim.AddNormal(nInd2);
                                prim.AddTexcoord(u0, v0);
                                prim.AddTexcoord(u1, v1);
                                prim.AddTexcoord(u2, v2);
                                prim.SetAlpha(alpha);
                                break;

                            default: throw new Exception("Unknown Primitive ID: " + primID.ToString());
                        }

                        tObj.PrimitiveAdd(prim);
                    }

                    //Add Mesh to Mesh array.
                    pMesh.Add(tObj);
                }
                Console.WriteLine("TMDFile -> Done.");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }

            Console.WriteLine();
            Console.WriteLine("TMDFile -> Finished Import.");
            return true;
        }

        /**
         * Get As a Model
        **/
        public IModel GetModel() {
            return new IModel(pMesh);
        }

        public ISimpleMesh GetMesh(int index) {
            return pMesh[index];
        }
    }
}
