using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace KF2.Rendering.Model {
    public struct MPMVertex {
        float px, py, pz;
        float nx, ny, nz;
        float tu, tv, ti;
        float cr, cg, cb, a;
        

        public void Default()
        {
            px = 0.0f;
            py = 0.0f;
            pz = 0.0f;
            nx = 0.0f;
            ny = 0.0f;
            nz = 0.0f;
            cr = 1.0f;
            cg = 1.0f;
            cb = 1.0f;
            a  = 1.0f;
            tu = 0.0f;
            tv = 0.0f;
            ti = -1.0f;
        }

        public Vector3 Position
        {
            get
            {
                return new Vector3(px, py, pz);
            }

            set
            {
                px = value.X;
                py = value.Y;
                pz = value.Z;
            }
        }
        public Vector3 Normal
        {
            get
            {
                return new Vector3(nx, ny, nz);
            }

            set
            {
                nx = value.X;
                ny = value.Y;
                nz = value.Z;
            }
        }
        public Vector3 Colour
        {
            get
            {
                return new Vector3(cr, cg, cb);
            }

            set
            {
                cr = value.X;
                cg = value.Y;
                cb = value.Z;
            }
        }
        public Vector3 Texture
        {
            get
            {
                return new Vector3(tu, tv, ti);
            }

            set
            {
                tu = value.X;
                tv = value.Y;
                ti = value.Z;
            }
        }
        public float Alpha
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public static int SizeInBytes
        {
            get
            {
                return sizeof(float) * 13;
            }
        }
    }
    public struct MPMMesh
    {
        public int  ptrVAO;
        public int  ptrVBO;
        public int numVertex;

        public MPMMesh(int vao, int vbo, int vc)
        {
            ptrVAO = vao;
            ptrVBO = vbo;
            numVertex = vc;
        }

        public int  VAO
        {
            get
            {
                return ptrVAO;
            }
        }
        public int  VBO
        {
            get
            {
                return ptrVBO;
            }
        }
        public int VertexCount
        {
            get
            {
                return numVertex;
            }
        }
    }

    public class CMPMStatic {
        public List<List<MPMVertex>> lVertex;
        public List<List<MPMVertex>> lVertexAlpha;

        private List<MPMMesh> dsMesh;
        private List<MPMMesh> dsMeshAlpha;

        public CMPMStatic() {
            dsMesh = new List<MPMMesh>();
            dsMeshAlpha = new List<MPMMesh>();
        }

        protected void Build() {
            for(int i = 0; i < lVertex.Count; ++i) {
                List<MPMVertex> vList = lVertex[i];

                int vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, (MPMVertex.SizeInBytes * vList.Count), vList.ToArray(), BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                int vao = GL.GenVertexArray();

                GL.BindVertexArray(vao);
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                GL.EnableVertexAttribArray(2);
                GL.EnableVertexAttribArray(3);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.VertexAttribPointer( //Attribute Location 0 Reserved for Position
                    0,
                    3,
                    VertexAttribPointerType.Float,
                    false,
                    MPMVertex.SizeInBytes,
                    0);
                GL.VertexAttribPointer( //Attribute Location 1 Reserved for Normal
                    1,
                    3,
                    VertexAttribPointerType.Float,
                    true,
                    MPMVertex.SizeInBytes,
                    sizeof(float) * 3);
                GL.VertexAttribPointer( //Attribute Location 2 Reserved for Texture
                    2,
                    3,
                    VertexAttribPointerType.Float,
                    false,
                    MPMVertex.SizeInBytes,
                    sizeof(float) * 6);
                GL.VertexAttribPointer( //Attribute Location 3 Reserved for Colour
                    3,
                    4,
                    VertexAttribPointerType.Float,
                    false,
                    MPMVertex.SizeInBytes,
                    sizeof(float) * 9);
                GL.BindVertexArray(0);

                dsMesh.Add(new MPMMesh(vao, vbo, vList.Count));
            }

            for (int i = 0; i < lVertexAlpha.Count; ++i)
            {
                List<MPMVertex> vList = lVertexAlpha[i];

                int vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, (MPMVertex.SizeInBytes * vList.Count), vList.ToArray(), BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                int vao = GL.GenVertexArray();

                GL.BindVertexArray(vao);
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                GL.EnableVertexAttribArray(2);
                GL.EnableVertexAttribArray(3);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.VertexAttribPointer( //Attribute Location 0 Reserved for Position
                    0,
                    3,
                    VertexAttribPointerType.Float,
                    false,
                    MPMVertex.SizeInBytes,
                    0);
                GL.VertexAttribPointer( //Attribute Location 1 Reserved for Normal
                    1,
                    3,
                    VertexAttribPointerType.Float,
                    true,
                    MPMVertex.SizeInBytes,
                    sizeof(float) * 3);
                GL.VertexAttribPointer( //Attribute Location 2 Reserved for Texture
                    2,
                    3,
                    VertexAttribPointerType.Float,
                    false,
                    MPMVertex.SizeInBytes,
                    sizeof(float) * 6);
                GL.VertexAttribPointer( //Attribute Location 3 Reserved for Colour
                    3,
                    4,
                    VertexAttribPointerType.Float,
                    false,
                    MPMVertex.SizeInBytes,
                    sizeof(float) * 9);
                GL.BindVertexArray(0);

                dsMeshAlpha.Add(new MPMMesh(vao, vbo, vList.Count));
            }

            lVertex.Clear();
            lVertexAlpha.Clear();
        }

        public void Draw() {
            for(int i = 0; i < dsMesh.Count; ++i) {
                Draw(i);
            }
        }

        public void Draw(int ind) {
            if (ind < dsMesh.Count) {
                GL.BindVertexArray(dsMesh[ind].ptrVAO);
                GL.DrawArrays(PrimitiveType.Triangles, 0, dsMesh[ind].VertexCount);
                GL.BindVertexArray(0);
            }
        }

        public void DrawAlpha() {
            for (int i = 0; i < dsMeshAlpha.Count; ++i) {
                DrawAlpha(i);
            }
        }

        public void DrawAlpha(int ind)
        {
            if (ind < dsMeshAlpha.Count)
            {
                GL.BindVertexArray(dsMeshAlpha[ind].ptrVAO);
                GL.DrawArrays(PrimitiveType.Triangles, 0, dsMeshAlpha[ind].VertexCount);
                GL.BindVertexArray(0);
            }
        }
    }
}
