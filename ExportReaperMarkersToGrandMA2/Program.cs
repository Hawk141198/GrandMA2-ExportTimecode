﻿using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportReaperMarkersToGrandMA2
{
    static class Program
    {
        public static Version Version;

        [STAThread]
        static void Main()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                Version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            else
            {
                System.Reflection.Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
                if (assemblyInfo != null)
                    Version = assemblyInfo.GetName().Version;
            }

            //try
            //{
            //    string[] response;
            //    using (WebClient webClient = new WebClient())
            //    {
            //        string file = webClient.DownloadString("https://raw.githubusercontent.com/Hawk141198/GrandMA2-ExportTimecode/master/version");
            //        response = file.Split(new[] { "\n" }, StringSplitOptions.None);

            //    }

            //    if (float.Parse(response[1]) > float.Parse(version))
            //    {
            //        MessageBox.Show("Sie verwenden eine alte Version (" + version + ")!\n" +
            //            "Die Version " + response[1] + " ist bereits verfügbar!\n" +
            //            "Sie können unter 'Updates' in der Menüleiste die neue Version herunterladen!", "Neue Version verfügbar!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (WebException ex) { }
            //catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
