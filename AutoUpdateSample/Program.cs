using System;
using System.Diagnostics;

namespace AutoUpdateSample
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Hello World!");
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Process is exiting");
            Process.Start("notepad.exe");
        }
    }
}
