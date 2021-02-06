using CincinnatiClientSdk;
using CincinnatiClientSdk.Builders;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace AutoUpdateSample
{
    class Program
    {
        static bool isUpdateAvailable = false;
        static string updateServer = "la-updateserver.eastus.azurecontainer.io";
        static string installerLocation = "";

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string currentVersion = $"{version.Major}.{version.Minor}.{version.Revision}";
            Console.WriteLine("Hello World! :) You are currently running " + currentVersion);

            // check for updates
            CincinnatiClient cincinnatiClient = CincinnatiClientBuilder.GetBuilder()
                                                    .WithServerUrl(updateServer)
                                                    .WithReleaseChannel("stable")
                                                    .Build(new HttpClient());
            var availableUpgrades = cincinnatiClient.GetNextApplicationVersions(currentVersion).GetAwaiter().GetResult();
            if(availableUpgrades.Count == 1)
            {
                isUpdateAvailable = true;
                string newVersion = availableUpgrades[0].Version;
                string installerWebLocation = availableUpgrades[0].Metadata["installer"];
                Console.WriteLine("Downloading update for version " + newVersion);
                installerLocation = Path.Join(Path.GetTempPath(), $"AutoUpdateSample.{newVersion}.msi");
                using var client = new WebClient();
                client.DownloadFile(installerWebLocation, installerLocation);
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            if(isUpdateAvailable)
            {
                Process.Start(new ProcessStartInfo(installerLocation)
                {
                    UseShellExecute = true
                });
            }
        }
    }
}
