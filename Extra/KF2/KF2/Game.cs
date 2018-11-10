using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

using KF2.Component;

using KF2.Rendering;
using KF2.Rendering.Primitive;
using KF2.Rendering.Shader;
using KF2.Rendering.Texture;
using KF2.Rendering.Model;
using KF2.Rendering.Scene.Map;

namespace KF2 {
    public class Game : GameWindow {
        //Game Settings
        private static int iWidth  = 800;
        private static int iHeight = 600;
        private static string sWinTitle = "King's Field II";

        //Game Memory
        private CCamera pCamera;
        private CPrimitive Rectangle2D;
        private CTexture2D pTexture;

        private CMap pMap;

        //Components
        MInputManager pInput;

        //
        // Constructor. Load Libraries/Components here.
        //
        public Game() : base(iWidth, iHeight, GraphicsMode.Default, sWinTitle, GameWindowFlags.Default, DisplayDevice.Default, 4, 6, GraphicsContextFlags.ForwardCompatible) {
            pInput = new MInputManager(false); //Non-Threaded input. Load from config?

            //Input Binds, load from config when finalizing
            pInput.AddBind("Key.Forward",     Key.W);
            pInput.AddBind("Key.Backward",    Key.S);
            pInput.AddBind("Key.LookLeft",    Key.A);
            pInput.AddBind("Key.LookRight",   Key.D);
            pInput.AddBind("Key.LookUp",      Key.Q);
            pInput.AddBind("Key.LookDown",    Key.E);
        }

        //
        // Game Set up area
        //
        protected override void OnLoad(EventArgs e) {
            // Output some OpenGL and System info
            Console.WriteLine("Renderer: " + GL.GetString(StringName.Renderer));
            Console.WriteLine("OpGL Version: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion));
            Console.WriteLine("");

            //Create new camera
            pCamera = new CCamera(new Vector4(0.0f, 0.0f, Width, Height), Vector3.UnitY);
            pCamera.SetPosition(new Vector3(16.0f, 512.0f, 0.0f));
            pCamera.SetDirection(new Vector3(1.0f, 0.0f, 1.0f));

            //Create Primitives (testing);
            Rectangle2D = new CPrimitiveRectangle(64.0f, 64.0f, new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            //Load a TIM Texture. This is threaded.
            pTexture = new CTextureTGA("Resource\\Texture\\page5.TGA");

            //Load a map
            pMap = new CMap();
            pMap.LoadTileset(TilesetFormat.TMD, "Resource\\Tileset\\coast.tmd");
            pMap.LoadKF("Resource\\Map\\coast.map");
            pMap.LoadKFItem("Resource\\Map\\coast.idb");
        }
        protected override void OnClosed(EventArgs e) {

        }
        protected override void OnResize(EventArgs e) {
            pCamera.SetScreen(new Vector4(0.0f, 0.0f, Width, Height));

            GL.Viewport(0, 0, Width, Height);
        }

        //
        // Render Functions
        //
        private void On2D()
        {

        }

        private void On3D()
        {
            pTexture.SetStage(0);

            pMap.DrawMap(pCamera, 16);
        }

        //
        // Main Game area
        //
        protected override void OnUpdateFrame(FrameEventArgs e) {
            //Update the Input Handler
            pInput.Update();

            //Temporary Key Control
            if (pInput.GetHeld("Key.Forward"))   { pCamera.Move(8.0f);          }
            if (pInput.GetHeld("Key.Backward"))  { pCamera.Move(-8.0f);         }

            if (pInput.GetHeld("Key.LookLeft"))  { pCamera.Rotate(2.2f, 0.0f);  }
            if (pInput.GetHeld("Key.LookRight")) { pCamera.Rotate(-2.2f, 0.0f); }
            if (pInput.GetHeld("Key.LookUp"))    { pCamera.Rotate(0.0f, 1.6f);  }
            if (pInput.GetHeld("Key.LookDown"))  { pCamera.Rotate(0.0f, -1.6f); }

            //Project the camera with fresh matrices.
            pCamera.Project(75.0f, 0.01f, 1024.0f);

            //Temporary Mouse Control
            //MouseState mouseState = Mouse.GetCursorState();

            //System.Windows.Forms.Cursor.Position = new System.Drawing.Point(1280 / 2, 720 / 2);
        }
        protected override void OnRenderFrame(FrameEventArgs e) {
            //Clear
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL States
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.Blend);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha); 

            //Render
            On3D();
            //On2D();

            //Swap GL Buffers.
            SwapBuffers();
        }
    }
}
