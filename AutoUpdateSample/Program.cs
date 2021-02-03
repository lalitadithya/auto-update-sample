using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AutoUpdateSample
{
    class Program
    {
        static bool isUpdateAvailable = false;
        static string installerWebLocation = "";
        static string newVersion = "";

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string currentVersion = $"{version.Major}.{version.Minor}.{version.Revision}";
            Console.WriteLine("Hello World! You are currently running " + currentVersion);

            // check for updates
            isUpdateAvailable = true;
            installerWebLocation = "https://github.com";
            newVersion = "1.1.0";
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            if(isUpdateAvailable)
            {
                string installerLocation = Path.Join(Path.GetTempPath(), $"AutoUpdateSample.{newVersion}.msi");
                Process.Start(new ProcessStartInfo(installerLocation)
                {
                    UseShellExecute = true
                });
            }
        }
    }
}
