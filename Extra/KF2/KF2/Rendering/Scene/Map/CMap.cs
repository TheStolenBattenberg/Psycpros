using System;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;

using OpenTK;
using KF2.Rendering.Model;
using KF2.Rendering.Shader;
using KF2.Rendering.Primitive;

namespace KF2.Rendering.Scene.Map
{
    public struct MapTile
    {
        private int mesh;
        private Transform matrix;
        public int height;

        public int MeshID
        {
            set
            {
                mesh = value;
            }

            get
            {
                return mesh;
            }
        }
        public Transform Transform
        {
            get
            {
                return matrix;
            }

            set
            {
                matrix = value;
            }
        }
    }
    public struct MapItem
    {
        private int mesh;
        private Transform matrix;

        public int MeshID
        {
            set
            {
                mesh = value;
            }

            get
            {
                return mesh;
            }
        }
        public Transform Transform
        {
            get
            {
                return matrix;
            }

            set
            {
                matrix = value;
            }
        }
    }

    public enum TilesetFormat {
        TMD,
    }

    class CMap
    {
        //Holds the map tiles in a 2D array.
        private MapTile[][][] dsTiles;
        private List<MapItem> dsItems;

        //Holds the map tileset
        private CMPMStatic mpmTileset;

        private CShader shdTile;

        private CPrimitive prmPlaceholder;
      

        /**
         * Constructor
         * 
        **/
        public CMap() {
            shdTile = new CShader("Resource\\Shader\\tile.vshd", "Resource\\Shader\\tile.fshd");

            prmPlaceholder = new CPrimitivePyramid(32.0F, 32.0F, 32.0F, new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
        }


        /**
         * Loads a King's Field II & King's Field III Map
        **/
        public void LoadKF(string path)
        {
            BinaryReader BINMAP = new BinaryReader(File.Open(path, FileMode.Open));

            uint Length = BINMAP.ReadUInt32();

            uint xPos, zPos;

            dsTiles = new MapTile[2][][];
            dsTiles[0] = new MapTile[80][];
            dsTiles[1] = new MapTile[80][];

            for (uint zz = 0; zz < 80; ++zz)
            {
                zPos = 64 * (79 - zz);

                dsTiles[0][zz] = new MapTile[80];
                dsTiles[1][zz] = new MapTile[80];

                for (uint xx = 0; xx < 80; ++xx)
                {              

                    xPos = 64 * xx;

                    for (uint layer = 0; layer < 2; ++layer)
                    {

                        byte MeshID    = BINMAP.ReadByte();
                        byte Elevation = BINMAP.ReadByte();
                        sbyte Direction = (sbyte) (BINMAP.ReadByte() - 0);
                        ushort ukn1 = BINMAP.ReadUInt16();

                        float degtorad = (float)(Math.PI / 180.0);

                        Transform trans = new Transform();
                        trans.localPosition = new Vector3(xPos, ((128.0f / 1024.0f) * 32.0f) * Elevation, zPos);
                        trans.localEulerRotation = new Vector3(180.0f * degtorad, (90.0f * degtorad) * Direction, 0.0f);
                        trans.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                        MapTile mt = new MapTile();
                        mt.MeshID = MeshID;
                        mt.Transform = trans;
                        mt.height = ukn1;

                        dsTiles[layer][zz][xx] = mt;
                    }
                }
            }

            BINMAP.Close();
        }


        /**
         * Loads a King's Field II & King's Field III ItemDB
        **/
        public void LoadKFItem(string path)
        {
            BinaryReader BINDB = new BinaryReader(File.Open(path, FileMode.Open));

            dsItems = new List<MapItem>();

            for(int i = 0; i < 350; ++i) {
                BINDB.BaseStream.Seek(24 * i, SeekOrigin.Begin);

                byte ObjectType  = BINDB.ReadByte();
                byte ObjectGridZ = (byte)(79 - BINDB.ReadByte());
                byte ObjectGridX = BINDB.ReadByte();
                byte ObjectUkwn1  = BINDB.ReadByte();
                byte ObjectMesh  = BINDB.ReadByte();
                byte ObjectUkwn2 = BINDB.ReadByte();
                byte ObjectUkwn3 = BINDB.ReadByte();
                byte ObjectUkwn4 = BINDB.ReadByte();
                byte ObjectUkwn5 = BINDB.ReadByte();
                byte ObjectUkwn6 = BINDB.ReadByte();
                byte ObjectUkwn7 = BINDB.ReadByte();
                byte ObjectUkwn8 = BINDB.ReadByte();
                byte ObjectUkwn9 = BINDB.ReadByte();
                byte ObjectUkwnA = BINDB.ReadByte();
                byte ObjectUkwnB = BINDB.ReadByte();
                byte ObjectUkwnC = BINDB.ReadByte();
                byte ObjectUkwnD = BINDB.ReadByte();
                byte ObjectUkwnE = BINDB.ReadByte();
                byte ObjectUkwnF = BINDB.ReadByte();
                byte ObjectUkwnG = BINDB.ReadByte();
                byte ObjectUkwnH = BINDB.ReadByte();
                byte ObjectUkwnI = BINDB.ReadByte();
                byte ObjectUkwnJ = BINDB.ReadByte();
                byte ObjectUkwnK = BINDB.ReadByte();

                if (ObjectType != 0xFF) {
                    /*
                    Console.WriteLine("Object [" + i.ToString() + "]: ");
                    Console.WriteLine("     Type: " + ObjectType.ToString());
                    Console.WriteLine("     Grid X Pos: " + ObjectGridX.ToString());
                    Console.WriteLine("     Grid Z Pos: " + ObjectGridZ.ToString());
                    Console.WriteLine("     Unknown 1: " + ObjectUkwn1.ToString());
                    Console.WriteLine("     Mesh: " + ObjectMesh.ToString());
                    Console.WriteLine("     Unknown 2: " + ObjectUkwn2.ToString());
                    Console.WriteLine("     Unknown 3: " + ObjectUkwn3.ToString());
                    Console.WriteLine("     Unknown 4: " + ObjectUkwn4.ToString());
                    Console.WriteLine("     Unknown 5: " + ObjectUkwn5.ToString());
                    Console.WriteLine("     Unknown 6: " + ObjectUkwn6.ToString());
                    Console.WriteLine("     Unknown 7: " + ObjectUkwn7.ToString());
                    Console.WriteLine("     Unknown 8: " + ObjectUkwn8.ToString());
                    Console.WriteLine("     Unknown 9: " + ObjectUkwn9.ToString());
                    Console.WriteLine("     Unknown 10: " + ObjectUkwnA.ToString());
                    Console.WriteLine("     Unknown 11: " + ObjectUkwnB.ToString());
                    Console.WriteLine("     Unknown 12: " + ObjectUkwnC.ToString());
                    Console.WriteLine("     Unknown 13: " + ObjectUkwnD.ToString());
                    Console.WriteLine("     Unknown 14: " + ObjectUkwnE.ToString());
                    Console.WriteLine("     Unknown 15: " + ObjectUkwnF.ToString());
                    Console.WriteLine("     Unknown 16: " + ObjectUkwnG.ToString());
                    Console.WriteLine("     Unknown 17: " + ObjectUkwnH.ToString());
                    Console.WriteLine("     Unknown 18: " + ObjectUkwnI.ToString());
                    Console.WriteLine("     Unknown 19: " + ObjectUkwnJ.ToString());
                    Console.WriteLine("     Unknown 20: " + ObjectUkwnK.ToString());
                    Console.WriteLine("");
                    */

                    //Get Map Tile Transform
                    float ObjectY = 
                        dsTiles[ObjectType-1][ObjectGridZ][ObjectGridX].Transform.localPosition.Y;


                    MapItem mi = new MapItem();
                    mi.MeshID = ObjectMesh;
                    mi.Transform = new Transform(
                        new Vector3(64.0f * ObjectGridX, ObjectY, 64.0f * ObjectGridZ),
                        new Vector3(0.0f, 0.0f, 0.0f),
                        new Vector3(1.0f, 1.0f, 1.0f));

                    dsItems.Add(mi);
                }
            }
        }

        /**
         * Loads a tileset to be used by the map.
        **/
        public void LoadTileset(TilesetFormat type, string path) {
            switch(type)
            {
                case TilesetFormat.TMD:
                    mpmTileset = new CModelTMD(path, false);
                    break;
            }
        }


        /**
         * Draw Map
        **/
        public void DrawMap(CCamera camera, int Dist) {
            //Calculate Camera Position in map
            int CamGridX = (int)Math.Floor(camera.GetFrom().X / 64.0f);
            int CamGridZ = 80 - (int)Math.Floor(camera.GetFrom().Z / 64.0f);

            //Temporary Clamp
            if (CamGridX < Dist) {
                CamGridX = Dist;
            }
            if(CamGridX > 79 - Dist) {
                CamGridX = 79 - Dist;
            }
            if(CamGridZ < Dist) {
                CamGridZ = Dist;
            }
            if(CamGridZ > 79 - Dist) {
                CamGridZ = 79 - Dist;
            }

            //Actual Drawing
            shdTile.Set();
            shdTile.SetSampler("sDiffuse", 0);

            shdTile.SetUniform("mView", camera.GetView());
            shdTile.SetUniform("mProj", camera.GetPerspective());

            for (int zz = CamGridZ - Dist; zz <= CamGridZ + Dist; ++zz)
            {
                for (int xx = CamGridX - Dist; xx <= CamGridX + Dist; ++xx)
                {
                    for (uint layer = 0; layer < 2; ++layer)
                    {
                        Transform trs = 
                            dsTiles[layer][zz][xx].Transform;

                        int Mesh = 
                            dsTiles[layer][zz][xx].MeshID;
                       
                        shdTile.SetUniform("mWorld", trs.localTransform);
                        mpmTileset.Draw(Mesh);                     
                    }
                }
            }

            for (int zz = CamGridZ - Dist; zz <= CamGridZ + Dist; ++zz)
            {
                for (int xx = CamGridX - Dist; xx <= CamGridX + Dist; ++xx)
                {
                    for (uint layer = 0; layer < 2; ++layer)
                    {
                        Transform trs =
                            dsTiles[layer][zz][xx].Transform;

                        int Mesh =
                            dsTiles[layer][zz][xx].MeshID;

                        shdTile.SetUniform("mWorld", trs.localTransform);
                        mpmTileset.DrawAlpha(Mesh);
                    }
                }
            }

            foreach(MapItem itm in dsItems) {
                shdTile.SetUniform("mWorld", itm.Transform.localTransform);

                prmPlaceholder.Draw();
            }

            shdTile.Reset();
        }
    }
}
