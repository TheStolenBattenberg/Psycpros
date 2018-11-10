using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Psycpros.Psycode {
    //Main Class
    class CRenderer {
        //Private Variables
        private ICamera pCamera;

        //Public Variables
        public List<IModel> pModels;
        public int iSelectedModel = 0;

        public IShader shaderm8;

        /**
         * Constructors 
        **/
        public CRenderer(ICamera camera) {
            pModels = new List<IModel>();

            pCamera = camera;

            //Test Shader
            shaderm8 = new IShader("Shader\\forward.vshd", "Shader\\forward.fshd");

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CRendererClear);
        }

        public void CRendererClear(object o, EventArgs e) {
            Console.WriteLine("Free'd renderer.");
            pModels.Clear();
        }

        public void Update() {
            //Clear Scene
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Project the camera.
            pCamera.Project();


            //Enable Some GL Functions
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            //Render Scene.
            shaderm8.Set(); {
                shaderm8.SetUniform("MAT_WORLD", Matrix4.Identity, false);
                shaderm8.SetUniform("MAT_VIEW", pCamera.GetViewMatrix(), false);
                shaderm8.SetUniform("MAT_PROJ", pCamera.GetProjectionMatrix(), false);

                //Render the current selected model.
                if (pModels.Count > 0) {
                    pModels[iSelectedModel].Render();
                }
            } GL.UseProgram(0);

            //Disable GL Functions
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
        }

        public void SetClearColour(float r, float g, float b, float a) {
            GL.ClearColor(r, g, b, a);
        }

        private void DrawAxis() {
            GL.Begin(PrimitiveType.Lines); {
                //X Axis
                GL.Vertex3(0.0f, 0.0f, 0.0f); GL.Color4(255, 0, 0, 255);
                GL.Vertex3(1.0f, 0.0f, 0.0f); GL.Color4(255, 0, 0, 255);

                //Y Axis
                GL.Vertex3(0.0f, 0.0f, 0.0f); GL.Color4(0, 255, 0, 255);
                GL.Vertex3(0.0f, -1.0f, 0.0f); GL.Color4(0, 255, 0, 255);

                //Z Axis
                GL.Vertex3(0.0f, 0.0f, 0.0f); GL.Color4(0, 0, 255, 255);
                GL.Vertex3(0.0f, 0.0f, 1.0f); GL.Color4(0, 0, 255, 255);
            } GL.End();
        }
    }
}
