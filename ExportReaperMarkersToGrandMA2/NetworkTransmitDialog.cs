﻿using SFTP;
using Telnet;
using System;
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
    public partial class NetworkTransmitDialog : Form
    {
        private Timecode Timecode;
        private TelnetInterface TelnetInterface;
        private SFTPClient SFTPClient;
        private string[] Cmds;
        

        public NetworkTransmitDialog(Timecode tc)
        {
            InitializeComponent();
            
            this.Timecode = tc;
            this.cB_Mode.SelectedIndex = 0;
            this.TelnetInterface = null;
        }

        private void cB_TransmitType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cB_Mode.SelectedIndex)
                {
                    case 0: // Build seq
                        Cmds = Timecode.GetMacroLines();
                        
                        TelnetInterface = new TelnetInterface(txt_ip.Text, Cmds, txt_username.Text, txt_password.Text);
                        TelnetInterface.OnConnectionChange += new EventHandler<TelnetConnectEventArgs>(OnTelnetConnectionChange);
                        await TelnetInterface.Connect();
                       
                        break;


                    case 1: // Build timecode

                        SFTPClient = new SFTPClient(txt_ip.Text, "data", "data", Timecode);
                        SFTPClient.OnConnectionChanged += OnFTPClientConnectionChange;
                        await SFTPClient.Connect();
                        
                        Cmds = new string[3];
                        int index = 0;
                        Cmds[index++] = "SelectDrive 1";
                        Cmds[index++] = "Import \"" + Timecode.GetTcName() + ".xml\" At Timecode " + Timecode.GetTc() + " /o";
                        Cmds[index++] = "Label Timecode " + Timecode.GetTc() + " \"" + Timecode.GetTcName() + "\" /o /nc";

                        TelnetInterface = new TelnetInterface(txt_ip.Text, Cmds, txt_username.Text, txt_password.Text);
                        TelnetInterface.OnConnectionChange += new EventHandler<TelnetConnectEventArgs>(OnTelnetConnectionChange);
                        await TelnetInterface.Connect();
                        
                        break;

                    case 2: // Build seq + timecode

                        SFTPClient = new SFTPClient(txt_ip.Text, "data", "data", Timecode);
                        SFTPClient.OnConnectionChanged += OnFTPClientConnectionChange;
                        await SFTPClient.Connect();
                        
                        Cmds = Timecode.GetMacroLines();
                        Cmds[Cmds.Length - 3] = "SelectDrive 1";
                        Cmds[Cmds.Length - 2] = "Import \"" + Timecode.GetTcName() + ".xml\" At Timecode " + Timecode.GetTc() + " /o";
                        Cmds[Cmds.Length - 1] = "Label Timecode " + Timecode.GetTc() + " \"" + Timecode.GetTcName() + "\" /o /nc";


                        TelnetInterface = new TelnetInterface(txt_ip.Text, Cmds, txt_username.Text, txt_password.Text);
                        TelnetInterface.OnConnectionChange += new EventHandler<TelnetConnectEventArgs>(OnTelnetConnectionChange);
                        await TelnetInterface.Connect();

                        break;

                    default:
                        MessageBox.Show("Der selektierte Übertragungsmodus ist nicht verfügbar. Bitte wählen Sie einen verfügbaren Modus aus!", "Übertragungsmodus nicht verfügbar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

            }
            catch (MA2CommandNotExecutedException eMA)
            {
                ConsoleOutput("GrandMA2 Kommando konnte nicht ausgeführt werden!\n", Color.Red, FontStyle.Bold);
                MessageBox.Show("Ein Fehler ist aufgetreten beim ausführen des folgenden Kommando:\t" + eMA.command + "\nFehlermeldung:\t" + eMA.error, "GrandMA2 Command wurde nicht ausgeführt!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TelnetConnectionException eTel)
            {
                switch (eTel.State)
                {
                    case TelnetConnectionStatus.Disabled:
                        ConsoleOutput("Telnet: Verbindung nicht möglich!\n", Color.Red, FontStyle.Bold);
                        MessageBox.Show("Die Telnet-Verbindung zur angegebenen GrandMA2-Konsole kann nicht hergestellt werden!\n" +
                            "Die Telnet Schnittstelle ist inaktiv!\n\n" +
                            "- Setup -> Global Settings -> Telnet muss auf 'Login Enabled' stehen\n", "Fehler beim verbinden zur GrandMA2-Konsole!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case TelnetConnectionStatus.Timeout:
                        ConsoleOutput("Telnet: Verbindung nicht möglich!\n", Color.Red, FontStyle.Bold);
                        MessageBox.Show("Die Telnet-Verbindung zur angegebenen GrandMA2-Konsole kann nicht hergestellt werden!\n" +
                            "Folgende Punkte müssen beachtet werden:\n\n" +
                            "- Setup -> Global Settings -> Telnet muss auf 'Login Enabled' stehen\n" +
                            "- Dieser PC muss mit dem MA-Net verbunden sein und mit ihm kommunizieren können. " +
                            "Mehr dazu unter 'Networking' des GrandMA2 User-Manuals. \n", "Fehler beim verbinden zur GrandMA2-Konsole!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case TelnetConnectionStatus.LoginNeeded:
                        ConsoleOutput("Telnet: Verbindung nicht möglich! Falsche Anmeldedaten!\n", Color.Red, FontStyle.Bold);
                        progressBar1.Value = 0;
                        MessageBox.Show("Die Telnet-Verbindung zur angegebenen GrandMA2-Konsole kann nicht hergestellt werden!\n\n" +
                            "Der angegebene Benutzername oder Passwort ist falsch. Bitte überprüfen Sie ihre Eingabe!", "Fehler beim verbinden zur GrandMA2-Konsole!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        break;
                }
                
            }
            catch (SFTPConnectionException eSFTP)
            {
                switch (eSFTP.State)
                {
                    case SFTPConnectionStatus.Refused:
                        ConsoleOutput("SFTP-Verbindung nicht möglich!\n", Color.Red, FontStyle.Bold);
                        MessageBox.Show("Die SFTP-Verbindung zur angegebenen GrandMA2-Konsole kann nicht hergestellt werden!\n" +
                            "Folgende Punkte müssen beachtet werden:\n\n" +
                            "- Dieser PC muss mit dem MA-Net verbunden sein und mit ihm kommunizieren können. " +
                            "Mehr dazu unter 'Networking' des GrandMA2 User-Manuals. \n", "Fehler beim verbinden zur GrandMA2-Konsole!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case SFTPConnectionStatus.Timeout:
                        ConsoleOutput("SFTP-Verbindung nicht möglich! Zeitüberschreitung\n", Color.Red, FontStyle.Bold);
                        MessageBox.Show("Die SFTP-Verbindung zur angegebenen GrandMA2-Konsole kann nicht hergestellt werden!\n" +
                            "Folgende Punkte müssen beachtet werden:\n\n" +
                            "- Dieser PC muss mit dem MA-Net verbunden sein und mit ihm kommunizieren können. " +
                            "Mehr dazu unter 'Networking' des GrandMA2 User-Manuals. \n", "Fehler beim verbinden zur GrandMA2-Konsole! Zeitüberschreitung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case SFTPConnectionStatus.Refused_CredentialsWrong:
                        ConsoleOutput("SFTP-Verbindung nicht möglich!\n", Color.Red, FontStyle.Bold);
                        MessageBox.Show("Die SFTP-Verbindung zur angegebenen GrandMA2-Konsole kann nicht hergestellt werden!\n" +
                            "Der Benutzername oder das Passwort ist nicht korrekt!", "Fehler beim verbinden zur GrandMA2-Konsole!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;


                    default:
                        ConsoleOutput("SFTP-Verbindung nicht möglich!\n", Color.Red, FontStyle.Bold);
                        break;
                }

            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                btn_send.Enabled = true;
                txt_ip.Enabled = true;
                txt_password.Enabled = true;
                txt_username.Enabled = true;
            }

        }
        
        private void txt_ip_Leave(object sender, EventArgs e)
        {
            IPAddress address;
            if (!IPAddress.TryParse(txt_ip.Text, out address))
            {
                MessageBox.Show("Die angegebene IP-Adresse ist keine gültige IP-Adresse. Bitte geben Sie eine gültige IPv4-Adresse ein!", "Fehler bei der Eingabe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region EventListeners Telnet
        private void OnCommandSend(object sender, TelnetProgressEventArgs e)
        {
            progressBar1.PerformStep();
            ConsoleOutput("Send:", Color.Green, FontStyle.Bold);
            ConsoleOutput(" \t" + e.Command + "\n");
        }

        private void OnFeedbackRecieve(object sender, TelnetProgressEventArgs e)
        {

            ConsoleOutput("Recieve:", Color.Green, FontStyle.Bold);
            ConsoleOutput(" " + e.Command + "\n");
        }

        private void OnProgressFinished(object sender, TelnetProgressEventArgs e)
        {
            ConsoleOutput("Sucessfull finished!\n", Color.Green, FontStyle.Bold);
        }


        private void OnTelnetConnectionChange(object sender, TelnetConnectEventArgs e)
        {
            switch (e.State)
            {
                case TelnetConnectionStatus.Connecting:
                    ConsoleOutput("Verbindung nach " + txt_ip.Text + " wird aufgebaut... Bitte warten...\n", Color.Black, FontStyle.Bold);
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.Value = 0;
                    btn_send.Enabled = false;
                    txt_ip.Enabled = false;
                    txt_password.Enabled = false;
                    txt_username.Enabled = false;
                    break;

                case TelnetConnectionStatus.Connected:

                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = Cmds.Length + 1; //+1 is Login Command
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;


                    TelnetInterface.OnCommandSend += new EventHandler<TelnetProgressEventArgs>(this.OnCommandSend);
                    TelnetInterface.OnFeedbackRecieved += new EventHandler<TelnetProgressEventArgs>(this.OnFeedbackRecieve);
                    TelnetInterface.OnProgressFinished += new EventHandler<TelnetProgressEventArgs>(this.OnProgressFinished);

                    TelnetInterface.Run();

                    btn_send.Enabled = true;
                    txt_ip.Enabled = true;
                    txt_password.Enabled = true;
                    txt_username.Enabled = true;
                    break;
                case TelnetConnectionStatus.Timeout:
                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.Value = 0;
                    btn_send.Enabled = true;
                    txt_ip.Enabled = true;
                    txt_password.Enabled = true;
                    txt_username.Enabled = true;
                    break;
            }

        }
        #endregion

        #region EventListeners SFTP
        private void OnFTPClientConnectionChange(object sender, FTPClientConnectionEventArgs e)
        {
            switch (e.State)
            {
                case SFTPConnectionStatus.Connecting:
                    ConsoleOutput("FTP: Verbindung nach " + txt_ip.Text + " wird aufgebaut... Bitte warten...\n", Color.Black, FontStyle.Bold);
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.Value = 0;
                    btn_send.Enabled = false;
                    txt_ip.Enabled = false;
                    txt_password.Enabled = false;
                    txt_username.Enabled = false;
                    break;

                case SFTPConnectionStatus.Connected:

                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 1;
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;
                    
                    SFTPClient.OnProgressChanged += new EventHandler<FTPClientProgressEventArgs>(this.OnFTPClientProgressChange);
                    
                    SFTPClient.Run();

                    btn_send.Enabled = true;
                    txt_ip.Enabled = true;
                    txt_password.Enabled = true;
                    txt_username.Enabled = true;
                    break;

                case SFTPConnectionStatus.Timeout:
                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.Value = 0;
                    btn_send.Enabled = true;
                    txt_ip.Enabled = true;
                    txt_password.Enabled = true;
                    txt_username.Enabled = true;
                    break;
            }
        }

        private void OnFTPClientProgressChange(object sender, FTPClientProgressEventArgs e)
        {
            switch (e.State)
            {
                case SFTPProgressStatus.Uploading:
                    ConsoleOutput("FTP: Die Datei wird hochgeladen... Bitte warten...\n", Color.Black, FontStyle.Bold);
                    break;

                case SFTPProgressStatus.Uploaded:
                    ConsoleOutput("FTP: Die Datei wurde erfolgreich hochgeladen!\n", Color.Green, FontStyle.Bold);
                    break;

                case SFTPProgressStatus.Refused:
                    ConsoleOutput("FTP: Die Datei konnte nicht hochgeladen werden!\n", Color.Red, FontStyle.Bold);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Console Output Methods

        private void ConsoleOutput(string text)
        {
            richTextBox1.AppendText(text);
            richTextBox1.Update();
        }

        private void ConsoleOutput(string text, Color c)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;

            richTextBox1.SelectionColor = c;

            richTextBox1.AppendText(text);
            richTextBox1.Update();

            richTextBox1.SelectionColor = richTextBox1.ForeColor;
        }

        private void ConsoleOutput(string text, Color c, FontStyle style)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;

            richTextBox1.SelectionColor = c;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, style);

            richTextBox1.AppendText(text);
            richTextBox1.Update();

            richTextBox1.SelectionColor = richTextBox1.ForeColor;
            richTextBox1.SelectionFont = richTextBox1.Font;

        }
        #endregion

    }
}
