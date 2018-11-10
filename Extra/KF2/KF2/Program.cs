using System;

namespace KF2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            new Game().Run(60);
        }
    }
}
