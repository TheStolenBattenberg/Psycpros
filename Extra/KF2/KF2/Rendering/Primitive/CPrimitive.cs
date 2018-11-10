using System;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace KF2.Rendering.Primitive {
    public class CPrimitive {
        //Vertex Struct, used to define vertices
        public struct Vertex {
            float x, y, z;
            float u, v, t;
            float r, g, b, a;

            public Vertex(Vector3 Position, Vector3 Texture, Vector4 Colour) {
                x = Position.X;
                y = Position.Y;
                z = Position.Z;

                u = Texture.X;
                v = Texture.Y;
                t = Texture.Z;

                r = Colour.X;
                g = Colour.Y;
                b = Colour.Z;
                a = Colour.W;
            }

            public Vector3 Position {
                set { x = value.X; y = value.Y; z = value.Z; }
                get { return new Vector3(x, y, z); }
            }

            public static int SizeInBytes {
                get {
                    return sizeof(float) * 10;
                }
            }
        }

        //OpenGL Context
        private int iVBOIndex = 0;
        private int iIBOIndex = 0;
        private int iVBAIndex = 0;

        //Primitive Data
        public Vertex[] pVertices = null;
        public ushort[] pIndices  = null;

        protected virtual void Build() {
            //Build VBO
            iVBOIndex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, iVBOIndex);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertex.SizeInBytes * pVertices.Length, pVertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //Build IBO
            iIBOIndex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, iIBOIndex);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(ushort) * pIndices.Length, pIndices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            //Build VBA Object
            iVBAIndex = GL.GenVertexArray();
            GL.BindVertexArray(iVBAIndex);
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(2);
                GL.EnableVertexAttribArray(3);
                GL.BindBuffer(BufferTarget.ArrayBuffer, iVBOIndex);
                GL.VertexAttribPointer( //Attribute Location 0 Reserved for Position
                    0,
                    3,
                    VertexAttribPointerType.Float,
                    false,
                    Vertex.SizeInBytes,
                    0);
                GL.VertexAttribPointer( //Attribute Location 2 Reserved for Texture
                    2,
                    3,
                    VertexAttribPointerType.Float,
                    false,
                    Vertex.SizeInBytes,
                    sizeof(float) * 3);
                GL.VertexAttribPointer( //Attribute Location 3 Reserved for Colour
                    3,
                    4,
                    VertexAttribPointerType.Float,
                    false,
                    Vertex.SizeInBytes,
                    sizeof(float) * 6);

                //IBO
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, iIBOIndex);
            GL.BindVertexArray(0);
        }

        public void Draw() {
            GL.BindVertexArray(iVBAIndex);
                GL.DrawElements(PrimitiveType.Triangles, pIndices.Length, DrawElementsType.UnsignedShort, 0);
            GL.BindVertexArray(0);
        }
    }
}