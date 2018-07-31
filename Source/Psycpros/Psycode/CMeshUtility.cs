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
     * Primitive Type
    **/
    public struct Primitive {
        public byte numVIndices;
        public int[] Indices;

        public Primitive(int ind1, int ind2, int ind3) { //Creator for Triangle Types
            this.numVIndices = 3;  //Triangles only have 3 indecies, so..
            Indices = new int[3];  //We need to initiate the array with 3 elements.

            //Copy index information.
            Indices[0] = ind1;
            Indices[1] = ind2;
            Indices[2] = ind3;
        }

        public Primitive(int ind1, int ind2, int ind3, int ind4) { //Creator for Quad Types
            this.numVIndices = 4;  //Quads have 4 indices, so...
            Indices = new int[4];  //We need to initiate the array with 4 elements.

            //Copy index information.
            Indices[0] = ind1;
            Indices[1] = ind2;
            Indices[2] = ind3;
            Indices[3] = ind4;
        }
    }
}