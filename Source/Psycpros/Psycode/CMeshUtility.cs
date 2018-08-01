using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psycpros.Psycode {
    /**
     * Vertex Type
    **/
    public struct Vertex {
        public float x, y, z;

        public Vertex(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    /**
     * Vertex Type
    **/
    public struct Normal {
        public float x, y, z;

        public Normal(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    /**
     * Texcoord Type
    **/
    public struct Texcoord {
        public float u, v;

        public Texcoord(float u, float v) {
            this.u = u;
            this.v = v;
        }
    }

    /**
     * Colour Type
    **/
    public struct Colour {
        public byte r;
        public byte g;
        public byte b;

        public Colour(byte r, byte g, byte b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }


    /**
     * Primitive Type
    **/
    public struct Primitive {
        //Vertex Set
        public byte numVIndices;
        public int[] Indices;

        //Other data
        public List<int> nIndex;
        public List<Colour> shade;
        public List<Texcoord> coords;
        public byte alpha;

        public Primitive(int ind1, int ind2, int ind3) { //Creator for Triangle Types
            this.numVIndices = 3;  //Triangles only have 3 indecies, so..
            Indices = new int[3];  //We need to initiate the array with 3 elements.

            //Copy index information.
            Indices[0] = ind1;
            Indices[1] = ind2;
            Indices[2] = ind3;

            nIndex = new List<int>();
            shade = new List<Colour>();
            coords = new List<Texcoord>();
            alpha = 255;
        }
        public Primitive(int ind1, int ind2, int ind3, int ind4) { //Creator for Quad Types
            this.numVIndices = 4;  //Quads have 4 indices, so...
            Indices = new int[4];  //We need to initiate the array with 4 elements.

            //Copy index information.
            Indices[0] = ind1;
            Indices[1] = ind2;
            Indices[2] = ind3;
            Indices[3] = ind4;

            nIndex = new List<int>();
            shade = new List<Colour>();
            coords = new List<Texcoord>();
            alpha = 255;
        }

        public void AddNormal(int nInd) {
            this.nIndex.Add(nInd);   
        }


        public void AddColour(byte r, byte g, byte b) {
            this.shade.Add(new Colour(r, g, b));
        }

        public void SetAlpha(byte a) {
            alpha = a;
        }

        public void AddTexcoord(float u, float v) {
            coords.Add(new Texcoord(u, v));
        }
    }
}