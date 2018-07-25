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

using OpenTK.Graphics.OpenGL4;

namespace Psycpros {
    public partial class Psycpros : Form {
        /**
         * Constructor.
        **/
        public Psycpros() {
            InitializeComponent();
        }

        /**
         * Functions get called when Psycpros initally loads.
        **/
        private void Psycpros_Load(object o, EventArgs e) {

        }

        /**
         * MenuBar Functionality.
        **/
        private void extractToolStripMenuItem_Click(object sender, EventArgs e) {            
            ITReader TFile = new ITReader(new Utility().GetOpenFilename(""));
            string path = new Utility().GetOpenDirectory();

            for (uint i = 0; i < TFile.iFileNumber; ++i) {
                TFile.Extract(i, path);
            }           
        }

        /**
         * OpenTK canvas
        **/
        private void glControl1_Paint(object sender, PaintEventArgs e) {
            glControl1.MakeCurrent();

            //Clear buffer.
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Render & Update code here.


            //Swap the internal render targets for double buffered glory.
            glControl1.SwapBuffers();
        }

        private void glControl1_Resize(object sender, EventArgs e) {
            GL.Viewport(0, 0, glControl1.ClientSize.Width, glControl1.ClientSize.Height);
        }
    }
}
