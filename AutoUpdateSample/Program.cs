using System;
using System.Diagnostics;
using System.Reflection;

namespace AutoUpdateSample
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Hello World! You are currently running " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Process is exiting");
            Process.Start("notepad.exe");
        }
    }
}
