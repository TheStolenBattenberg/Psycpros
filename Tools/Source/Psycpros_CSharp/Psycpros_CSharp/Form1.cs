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

namespace Psycpros_CSharp
{
    public partial class Psycpros : Form
    {
        public Psycpros()
        {
            InitializeComponent();
        }

        private void Psycpros_Load(object sender, EventArgs e) {
            
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e) {            
            ITReader TFile = new ITReader(new Utility().GetOpenFilename(""));

            for(uint i = 0; i < TFile.iFileNumber; ++i) {
                TFile.Extract(i, "");
            }           
        }
    }
}
