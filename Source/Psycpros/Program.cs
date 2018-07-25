using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Psycpros {
    static class Program {

        [STAThread]
        /**
         * Main Entry point.
        **/
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Execute Psycpros.
            Application.Run(new Psycpros());
        }
    }
}
