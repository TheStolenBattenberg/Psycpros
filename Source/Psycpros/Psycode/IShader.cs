using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Psycpros.Psycode {
    class IShader {
        private int siVShdID = 0;
        private int siFShdID = 0;
        private int siGShdID = 0;
        private int siProgramID = 0;

        private Dictionary<string, int> mUniform;

        /**
         * Constructors
        **/
        public IShader(string VShaderPath, string FShaderPath) {
            //Create new shaders in the OpenGL driver.
            siVShdID = GL.CreateShader(ShaderType.VertexShader);
            siFShdID = GL.CreateShader(ShaderType.FragmentShader);

            //Read the Vertex Shader.
            string vc, fc;
            if(!ReadShader(VShaderPath, out vc)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to load Vertex shader.\n");
                return;
            }

            if (!ReadShader(FShaderPath, out fc)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to load Fragment shader.\n");
                return;
            }


            //Compile the Shaders.
            if (!CompileShader(vc, siVShdID)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to compile Vertex shader.\n");
                return;
            }

            if (!CompileShader(fc, siFShdID)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to compile Fragment shader.\n");
                return;
            }

            //Build Program
            Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                + "] -> Building Vert/Frag Program...\n");

            siProgramID = GL.CreateProgram();
            GL.AttachShader(siProgramID, siVShdID);
            GL.AttachShader(siProgramID, siFShdID);
            GL.LinkProgram(siProgramID);

            //Check Program
            int ll;
            GL.GetProgram(siProgramID, GetProgramParameterName.InfoLogLength, out ll);
            if(ll > 0) {
                string e = GL.GetProgramInfoLog(siProgramID);
                Console.WriteLine(e);
                return;
            }

            //Create uniform map
            mUniform = new Dictionary<string, int>();
        }
        public IShader(string VShaderPath, string GShaderPath, string FShaderPath) {
            //Create new shaders in the OpenGL driver.
            siVShdID = GL.CreateShader(ShaderType.VertexShader);
            siGShdID = GL.CreateShader(ShaderType.GeometryShader);
            siFShdID = GL.CreateShader(ShaderType.FragmentShader);

            //Read the Vertex Shader.
            string vc, gc, fc;
            if (!ReadShader(VShaderPath, out vc)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to load Vertex shader.\n");
                return;
            }

            //Read the geometry shader
            if (!ReadShader(GShaderPath, out gc)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to load Geometry shader.\n");
                return;
            }

            //Read the fragment shader
            if (!ReadShader(FShaderPath, out fc)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to load Fragment shader.\n");
                return;
            }


            //Compile the vertex shader.
            if (!CompileShader(vc, siVShdID)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to compile Vertex shader.\n");
                return;
            }

            //Compile the geometry shader.
            if (!CompileShader(gc, siGShdID)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to compile Geometry shader.\n");
                return;
            }

            //Compile the fragment shader.
            if (!CompileShader(fc, siFShdID)) {
                Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                    + "] -> !WOT! -> Failed to compile Fragment shader.\n");
                return;
            }

            //Build Program
            Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                + "] -> Building Vert/Geom/Frag Program...\n");

            siProgramID = GL.CreateProgram();
            GL.AttachShader(siProgramID, siVShdID);
            GL.AttachShader(siProgramID, siGShdID);
            GL.AttachShader(siProgramID, siFShdID);
            GL.LinkProgram(siProgramID);

            //Check Program
            int ll;
            GL.GetProgram(siProgramID, GetProgramParameterName.InfoLogLength, out ll);
            if (ll > 0) {
                string e = GL.GetProgramInfoLog(siProgramID);
                Console.WriteLine(e);
                return;
            }

            //Create uniform map
            mUniform = new Dictionary<string, int>();
        }

        /**
         * Read shader source into a string
        **/
        private bool ReadShader(string ShaderPath, out string ShdOut) {
            //Set ShdOut string to nothing
            ShdOut = "";

            //Reading Shader...
            Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString() 
                + "] -> Reading Shader: ");
            Console.WriteLine(Path.GetFileName(ShaderPath));

            try {
                //Create new StreamReader to read VShader.
                StreamReader sr = new StreamReader(ShaderPath);
                ShdOut = sr.ReadToEnd();

                //Close stream reader.
                sr.Close();
            } catch (Exception ex) {
                //We failed to read the file...
                Console.WriteLine(ex.Message);
                return false;
            }
    
            //Success! Return = )
            return true;
        }
        
        /**
         * Compile a shader from source
        **/
        private bool CompileShader(string ShdSrc, int shaderID) {
            int ll;

            //Compile Shader
            Console.Write("[" + this.ToString() + " : " + this.GetHashCode().ToString()
                + "] -> Compiling Shader...\n");
            
            GL.ShaderSource(shaderID, ShdSrc);
            GL.CompileShader(shaderID);

            //Is the shader good?
            GL.GetShader(shaderID, ShaderParameter.InfoLogLength, out ll);
            if(ll > 0) {
                string e = GL.GetShaderInfoLog(shaderID);
                Console.WriteLine(e);
                return false;
            }

            //Yay
            return true;
        }

        /**
         * Sets the program for the main renderer.
        **/
        public void Set() {
            GL.UseProgram(siProgramID);
        }

        /**
         * Sets the program for a pipeline.
        **/
        public void SetPipeline(int pl, ProgramStageMask mask) {
            GL.UseProgramStages(pl, mask, siProgramID);
        }

        /**
         * Set Uniform
        **/
        public void SetUniform(string name, double v) {
            //If the uniform doesn't exist, get it.
            if(!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniform1(siProgramID, mUniform[name], v);
        }
        public void SetUniform(string name, Vector2d v) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniform2(siProgramID, mUniform[name], v.X, v.Y);
        }
        public void SetUniform(string name, Vector3d v) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniform3(siProgramID, mUniform[name], v.X, v.Y, v.Z);
        }
        public void SetUniform(string name, Vector4d v) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniform4(siProgramID, mUniform[name], v.X, v.Y, v.Z, v.W);
        }
        public void SetUniform(string name, Matrix2 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix2(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix2x3 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix2x3(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix2x4 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix2x4(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix3 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix3(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix3x2 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix3x2(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix3x4 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix3x4(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix4 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix4(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix4x2 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix4x2(siProgramID, mUniform[name], transpose, ref v);
        }
        public void SetUniform(string name, Matrix4x3 v, bool transpose) {
            //If the uniform doesn't exist, get it.
            if (!mUniform.ContainsKey(name)) {
                mUniform.Add(name, GL.GetUniformLocation(siProgramID, name));
            }

            //Set the uniform.
            GL.ProgramUniformMatrix4x3(siProgramID, mUniform[name], transpose, ref v);
        }

        /**
         * Set Sampler
        **/
        public void SetSampler(TextureUnit sindex, ITexture tex) {
            GL.ActiveTexture(sindex);
        }
    }
}
