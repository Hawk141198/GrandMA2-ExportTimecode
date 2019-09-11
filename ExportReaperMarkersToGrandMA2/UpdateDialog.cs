﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportReaperMarkersToGrandMA2
{
    public partial class UpdateDialog : Form
    {
        public UpdateDialog()
        {
            InitializeComponent();

            lbl_DownloadInfo.Text = "";
            button1.Enabled = false;
        }

        private void UpdateDialog_Load(object sender, EventArgs e)
        {
            string response = "";
            using (WebClient webClient = new WebClient())
            {
                response = webClient.DownloadString("https://raw.githubusercontent.com/Hawk141198/GrandMA2-ExportTimecode/master/version");
            }

            response = response.Replace("\n", "");

            if (float.Parse(response) > float.Parse(Program.version))
            {
                lblInfo.Text = "Sie benutzten eine alte Version! Es ist bereits eine aktuellere Version verfügbar!" +
                    "\n" +
                    "Ihre Version: " + Program.version +"\n" +
                    "Neue Version: " + response + "\n" +
                    "Wollen Sie die neue Version herunterladen?";
                button1.Enabled = true;
            }
            else if(float.Parse(response) == float.Parse(Program.version))
            {
                lblInfo.Text = "Sie benutzen die aktuellste Verison!";
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            WebClient d = new WebClient();
            d.DownloadProgressChanged += DownloadProgressChanged;
            d.DownloadFileCompleted += DownloadFileCompleted;
            d.DownloadFileAsync(new Uri("https://github.com/Hawk141198/GrandMA2-ExportTimecode/raw/master/GrandMA2-ExportTimecode.exe"), "GrandMA2-ExportTimecode.exe");
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lbl_DownloadInfo.Text = "Download abgeschlossen!";
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lbl_DownloadInfo.Text = e.BytesReceived + " Bytes / " + e.TotalBytesToReceive + " Bytes";
        }
    }
}