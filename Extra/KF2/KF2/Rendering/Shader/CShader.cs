using System;
using System.Collections.Generic;
using System.IO;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace KF2.Rendering.Shader {
    public class CShader {
        private int pVertexShader;
        private int pFragmentShader;
        private int pProgram;

        private Dictionary<string, int> mUniform;

        //Constructor
        public CShader(string VShdPath, string FShdPath) {
            //Read Vertex Shader
            string VShdCode = "";
            if(!ReadShader(VShdPath, out VShdCode)) {
                Console.WriteLine("<" + "CShader" + ":" + this.GetHashCode() + "> Failed to read Vertex Shader!");
                return;
            }

            //Read Fragment Shader
            string FShdCode = "";
            if(!ReadShader(FShdPath, out FShdCode)) {
                Console.WriteLine("<" + "CShader" + ":" + this.GetHashCode() + "> Failed to read Fragment Shader!");
                return;
            }

            //Compile Vertex Shader
            pVertexShader = GL.CreateShader(ShaderType.VertexShader);
            if(!CompileShader(VShdCode, pVertexShader)) {
                GL.DeleteShader(pVertexShader);
                return;
            }

            //Compile Fragment Shader
            pFragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            if (!CompileShader(FShdCode, pFragmentShader)) {
                GL.DeleteShader(pFragmentShader);
                return;
            }

            //Build Program
            pProgram = GL.CreateProgram();
            GL.AttachShader(pProgram, pVertexShader);
            GL.AttachShader(pProgram, pFragmentShader);
            GL.LinkProgram(pProgram);

            //Error check program
            int LL; GL.GetProgram(pProgram, GetProgramParameterName.InfoLogLength, out LL);
            if(LL > 0) {
                Console.WriteLine("<" + "CShader" + ":" + this.GetHashCode() + "> Failed.");
                Console.WriteLine(GL.GetProgramInfoLog(pProgram));

                GL.DeleteProgram(pProgram);
                GL.DeleteShader(pFragmentShader);
                GL.DeleteShader(pVertexShader);
                return;
            }

            //Create a <string, int> map to hold uniform data.
            mUniform = new Dictionary<string, int>();

            Console.WriteLine("<" + "CShader" + ":" + this.GetHashCode() + "> Shader Created.");

            return;
        }

        //Destory the Shader, freeing its memory.
        public void Destroy() {
            GL.DeleteProgram(pProgram);
            GL.DeleteShader(pFragmentShader);
            GL.DeleteShader(pVertexShader);
        }

        //Read Shader
        public bool ReadShader(string ShdPath, out string shader) {
            Console.Write("<" + "CShader" + ":" + this.GetHashCode() + "> Reading Shader: ");
            Console.WriteLine(ShdPath);

            //We need to set shader to nothing, incase of error.
            shader = "";

            //Read text into string
            try {
                StreamReader File = new StreamReader(ShdPath);
                    shader = File.ReadToEnd();
                File.Close();
            } catch (Exception E) {
                Console.WriteLine(E.Message);

                return false;
            }
            return true;
        }

        //Compile Shader
        public bool CompileShader(string ShdSource, int ShdID) {
            Console.Write("<" + "CShader" + ":" + this.GetHashCode() + "> Compiling Shader...");

            GL.ShaderSource(ShdID, ShdSource);
            GL.CompileShader(ShdID);

            int LL;  GL.GetShader(ShdID, ShaderParameter.InfoLogLength, out LL);
            if(LL > 0) {
                Console.Write(" Failed (");
                Console.WriteLine(GL.GetShaderInfoLog(ShdID) + ").");
                return false;
            }
            Console.WriteLine(" Success.");
            return true;
        }

        //Set Shader to OpenGL
        public void Set() {
            GL.UseProgram(pProgram);
        }

        //Reset the Shader
        public void Reset() {
            GL.UseProgram(0);
        }

        //Get Uniform
        public int GetUniform(string name) {
            if (!mUniform.ContainsKey(name)) {
                mUniform[name] = GL.GetUniformLocation(pProgram, name);
            }

            return mUniform[name];
        }

        //Set Uniform (float / Vector)
        public void SetUniform(string name, double f) {
            int u = GetUniform(name);

            GL.ProgramUniform1(pProgram, u, f);
        }
        public void SetUniform(string name, Vector2 v) {
            int u = GetUniform(name);

            GL.ProgramUniform2(pProgram, u, v);
        }
        public void SetUniform(string name, Vector3 v) {
            int u = GetUniform(name);

            GL.ProgramUniform3(pProgram, u, v);
        }
        public void SetUniform(string name, Vector4 v) {
            int u = GetUniform(name);

            GL.ProgramUniform4(pProgram, u, v);
        }

        //Set Uniform (Matrix4)
        public void SetUniform(string name, Matrix4 m) {
            int u = GetUniform(name);

            GL.ProgramUniformMatrix4(pProgram, u, false, ref m);
        }

        //Set Sampler
        public void SetSampler(string name, int index) {
            GL.Uniform1(GetUniform(name), index);
        }
    }
}
