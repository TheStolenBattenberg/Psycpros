using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace Psycpros.Psycode {

    //Enum for meshes
    public enum Mesh {
        Empty = 0x00,
        Triangle = 0x01,
        Pyramid  = 0x02
    }


    class ISimpleMesh {
        //Private Data
        private List<Vertex> lVertex;
        private List<Normal> lNormal;
        private List<Primitive> lPrimitive;

        /**
         * Constructors.
         **/
        public ISimpleMesh() {
            //Create Lists
            lVertex = new List<Vertex>();
            lNormal = new List<Normal>();
            lPrimitive = new List<Primitive>();
        }

        public ISimpleMesh(Mesh type, float size) {
            //Create Lists
            lVertex = new List<Vertex>();
            lNormal = new List<Normal>();
            lPrimitive = new List<Primitive>();

            //Build Meshes
            switch(type) {
                case Mesh.Empty:
                    return;

                case Mesh.Triangle:
                    GenTriangle(size);
                    return;

                case Mesh.Pyramid:
                    GenPyramid(size);
                    return;
            }
        }

        /**
         * Mesh Generators
         **/
        private void GenTriangle(float size) {
            lVertex.Add(new Vertex(size * -1.0f, size * -1.0f, size * 0.0f));
            lVertex.Add(new Vertex(size *  1.0f, size * -1.0f, size * 0.0f));
            lVertex.Add(new Vertex(size *  0.0f, size *  1.0f, size * 0.0f));
            lPrimitive.Add(new Primitive(0, 1, 2));
        }
        private void GenPyramid(float size) {
            lVertex.Add(new Vertex(size * -1.0f, size * -1.0f, size * -1.0f));
            lVertex.Add(new Vertex(size *  1.0f, size * -1.0f, size * -1.0f));
            lVertex.Add(new Vertex(size *  1.0f, size * -1.0f, size *  1.0f));
            lVertex.Add(new Vertex(size * -1.0f, size * -1.0f, size *  1.0f));
            lVertex.Add(new Vertex(size *  0.0f, size *  1.0f, size *  0.0f));

            lPrimitive.Add(new Primitive(0, 1, 2, 3));
            lPrimitive.Add(new Primitive(0, 1, 4));
            lPrimitive.Add(new Primitive(1, 2, 4));
            lPrimitive.Add(new Primitive(2, 3, 4));
            lPrimitive.Add(new Primitive(3, 0, 4));
        }
        /**
         * Add a Vertex to the Vertex list
         **/
        public void VertexAdd(Vertex v) {
            lVertex.Add(v);
        }
        public void VertexAdd(float x, float y, float z) {
            lVertex.Add(new Vertex(x, y, z));
        }

        /**
         * Add a Normal to the Normal list
         **/
        public void NormalAdd(Normal n) {
            lNormal.Add(n);
        }
        public void NormalAdd(float x, float y, float z) {
            lNormal.Add(new Normal(x, y, z));
        }

        /**
         * Add a Triangle to the Primitive list
         **/
        public void PrimitiveAdd(Primitive p) {
            lPrimitive.Add(p);
        }

        /**
         * Render the mesh
         **/
        private void RenderPrimitive(Primitive p) {
            //See if we're rendering a triangle or quad.
            switch (p.numVIndices) {
                case 3: //It's a triangle, so we just render it.
                    for(byte i = 0; i < p.numVIndices; ++i) {
                        GL.VertexAttrib3(0, 
                            lVertex[p.Indices[i]].x,
                            lVertex[p.Indices[i]].y,
                            lVertex[p.Indices[i]].z);
                    }
                    break;

                case 4: //We split quads into two triangles on render.
                    for (byte i = 0; i < p.numVIndices; ++i) { //First Tri
                        GL.VertexAttrib3(0, 
                            lVertex[p.Indices[i]].x,
                            lVertex[p.Indices[i]].y,
                            lVertex[p.Indices[i]].z);
                    }

                    for (byte i = 1; i < p.numVIndices; ++i) { //Second Tri
                        GL.VertexAttrib3(0, 
                            lVertex[p.Indices[i]].x,
                            lVertex[p.Indices[i]].y,
                            lVertex[p.Indices[i]].z);
                    }
                    break;
            }
        }
        public void Render() {
            //Begin Drawing Triangles in direct mode.
            GL.Begin(PrimitiveType.Triangles);

            //Loop through each primitives.
            foreach (Primitive prim in lPrimitive) {
                RenderPrimitive(prim);
            }

            //End Drawing Triangles.
            GL.End();
        }
    }
}
