using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Psycpros.Reader
{
    class Utility
    {
        public string GetOpenDirectory() {
            FolderBrowserDialog oD = new FolderBrowserDialog();
            oD.ShowDialog();

            return oD.SelectedPath;
        }
        public string GetOpenFilename(string filter) {
            //Set up Open Dialog
            OpenFileDialog oF = new OpenFileDialog();
            oF.InitialDirectory = "";
            oF.Filter = filter;
            oF.FilterIndex = 2;
            oF.RestoreDirectory = true;
            oF.ShowDialog();
         
            return oF.FileName;
        }
    }
}
