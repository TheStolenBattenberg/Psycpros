using System.Windows.Forms;
using System.IO;

namespace Psycpros.Reader
{
    class Utility
    {
        public string GetOpenDirectory(string title) {
            FolderBrowserDialog oD = new FolderBrowserDialog();
            oD.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
            oD.Description = title;
            oD.ShowDialog();

            return oD.SelectedPath;
        }
        public string GetOpenFilename(string title, string filter) {
            //Set up Open Dialog
            OpenFileDialog oF = new OpenFileDialog();
            oF.InitialDirectory = "";
            oF.Title = title;
            oF.Filter = filter;
            oF.FilterIndex = 2;
            oF.RestoreDirectory = true;
            oF.ShowDialog();
         
            return oF.FileName;
        }
    }
}
