using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace Psycpros.Psycode {
    class IMesh {
        public int siVertexCount = 0;

        //Private Variables
        private int siVertexArrayID = 0;
        private int siVertexBufferID = 0;

        private float[] faVertices = null;

        /**
         * Constructor.
        **/
        public IMesh(int vcount) {
            //Generate a vertex array, return on fail.
            GL.GenVertexArrays(1, out siVertexArrayID);
            if (siVertexArrayID < 0) {
                Console.Write("IMesh -> ! WTF ! -> Failed to generate Vertex Array!\n");
                return;
            }


            //Generate a vertex buffer, return on fail.
            GL.GenBuffers(1, out siVertexBufferID);
            if(siVertexBufferID < 0) {
                Console.Write("IMesh -> ! WTF ! -> Failed to generate Vertex Buffer!\n");
                return;
            }

            //Set mesh information.
            siVertexCount = vcount;

            //Create mesh data arrays.
            faVertices = new float[3 * siVertexCount];

            Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString() + "] -> Created Successfully.\n");
        }

        /**
         * Get Vertex Data Array
        **/
        public float[] GetVertices() {
            return faVertices;
        }

        /**
         * Add a vertex to the mesh
        **/
        public void SetVertex(uint index, float x, float y, float z) {
            faVertices[(3 * index) + 0] = x;
            faVertices[(3 * index) + 1] = y;
            faVertices[(3 * index) + 2] = z;
        }

        /**
         * Build Mesh for rendering.
        **/
        public void Build() {
            //Bind Mesh Data pointers
            GL.BindVertexArray(siVertexArrayID);

            //Copy vertices to vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, siVertexBufferID);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr) (sizeof(float) * faVertices.Length), faVertices, BufferUsageHint.StaticDraw);

            //Unbind mesh data
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            Console.Write("["+ this.ToString()+" : "+this.GetHashCode().ToString()+"] -> Build Successful.\n");
        }
        
        /**
         * Render mesh.
        **/
        public void Submit() {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 3, 0);

            //Set up Vertex Attribute defition
            GL.EnableVertexAttribArray(0);

            //Bind Data
            GL.BindVertexArray(siVertexArrayID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, siVertexBufferID);
            
            GL.VertexAttribPointer(0,
                3,
                VertexAttribPointerType.Float,
                false,
                0,
                0);

            //Submit the mesh data
            GL.DrawArrays(PrimitiveType.Triangles, 0, siVertexCount);

            //Disable vertex attribute definition
            GL.DisableVertexAttribArray(0);

            //Unbind data.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);   
        }
    }
}