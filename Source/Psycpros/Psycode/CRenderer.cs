using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;

namespace Psycpros.Psycode {
    //Main Class
    class CRenderer {
        //Private Variables
        private ICamera pCamera;

        //Public Variables
        public ISimpleMesh meshM8;
        public IShader shaderm8;

        /**
         * Constructors 
        **/
        public CRenderer(ICamera camera) {
            pCamera = camera;

            //Test Mesh
            meshM8 = new ISimpleMesh(Mesh.Pyramid, 2.0f);

            //Test Shader
            shaderm8 = new IShader("Shader\\forward.vshd", "Shader\\forward.fshd");
        }

        public void Update() {
            //Clear Scene
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Project the camera.
            pCamera.Project();

            //Render Scene.
            shaderm8.Set();
                shaderm8.SetUniform("MAT_WORLD", Matrix4.Identity, false);
                shaderm8.SetUniform("MAT_VIEW",  pCamera.GetViewMatrix(), false);
                shaderm8.SetUniform("MAT_PROJ",  pCamera.GetProjectionMatrix(), false);

                meshM8.Render();
            GL.UseProgram(0);
        }

        public void SetClearColour(float r, float g, float b, float a) {
            GL.ClearColor(r, g, b, a);
        }
    }
}
