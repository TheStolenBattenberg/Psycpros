using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Psycpros.Reader;
using Psycpros.Psycode;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Psycpros {
    public partial class Psycpros : Form {
        private Timer T;
        double dLastTime = 0;
        uint iFrames = 0;

        private CRenderer pRenderer;
        private ICamera pCamera;
        double iRotCam = 0;

        /**
         * Constructor.
        **/
        public Psycpros() {
            InitializeComponent();
        }

        /**
         * MenuBar Functionality.
        **/
        private void extractToolStripMenuItem_Click(object sender, EventArgs e) {
            //Open the T File and read it's content.
            ITReader TFile = new ITReader(new Utility().GetOpenFilename("Select a T file to extract", "From Software Archive (*.T)|*.T"));

            //Get a path to extract the files to.
            string path = new Utility().GetOpenDirectory("Extract to where?");

            //Extract each file.
            for (uint i = 0; i < TFile.iFileNumber; ++i) {
                TFile.Extract(i, path);
            }           
        }

        /**
         * OpenTK canvas
        **/
        private void glControl1_Paint(object sender, PaintEventArgs e) {
            glControl1.MakeCurrent();
        }

        private void glControl1_Resize(object sender, EventArgs e) {
            if (glControl1.ClientSize.Height == 0)
                glControl1.ClientSize = new System.Drawing.Size(glControl1.ClientSize.Width, 1);

            GL.Viewport(glControl1.ClientRectangle.X, glControl1.ClientRectangle.Y, 
                glControl1.ClientSize.Width, glControl1.ClientSize.Height);

        }

        private void glControl1_Load(object sender, EventArgs e) {
            glControl1_Resize(sender, e);

            //Create a Camera
            pCamera = new ICamera(new Vector4(-1f, 1f, -1f, 1f), Vector3.UnitY);
            pCamera.SetProjectionPerspective(
                glControl1.ClientSize.Width / glControl1.ClientSize.Height, //Aspect
                75, //FOV
                0.1f, 1024.0f); //Near, Far

            //Set up the Renderer.
            pRenderer = new CRenderer(pCamera);
            pRenderer.SetClearColour(0.0f, 0.0f, 0.4f, 1.0f);

            //Set up a timer
            T = new Timer();
            T.Interval = 1000 / 50;
            T.Tick += new EventHandler(glUpdate);
            T.Start();

            dLastTime = Environment.TickCount;
        }


        //
        // OpenGL Updates
        //
        void glUpdate(object sender, EventArgs e) {
            //Rotate Camera
            iRotCam -= 2.0f;
            if (iRotCam > 180.0d) { iRotCam = -180.0d; }

            float rotVal = (float)Math.PI * ((float)iRotCam / 180.0f);
            pCamera.Position(new Vector3((float)Math.Cos(rotVal) * 8.0f, 0.0f, (float)-Math.Sin(rotVal) * 8.0f));

            //Render
            pRenderer.Update();

            //Calculate Frames Per Second
            double ct = Environment.TickCount;
            iFrames++;
            if((ct - dLastTime) >= 1000.0d) {
                toolStripStatusLabel1.Text = "FPS: " + iFrames.ToString() +", MS: "+(ct - dLastTime).ToString();
                dLastTime = ct;
                iFrames = 0;
            }

            //Swap buffers, for double buffered glory.  
            glControl1.SwapBuffers();
        }
    }
}
