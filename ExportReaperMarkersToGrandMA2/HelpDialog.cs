﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportReaperMarkersToGrandMA2
{
    public partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();

            string web = "<html><body><p><h1>Anleitung:</h1><br>Folgende Dinge m&uumlssen beachtet werden: </p><p><h3>1. Reaper-Datei ausw&aumlhlen:</h3><li>Es muss eine von Reaper erstellte Datei ausgew&aumlhlt werden</li><li>Die Timeline muss auf dem Format: <b>'Hours:Minutes:Seconds:Frames'</b> stehen (Rechtsklick auf die Timeline)</li></p><p><h3>2. Namensgebung der Pool-Items:</h3><li>M&oumlglichst keine Lehrzeichen verwenden</li></p><p><h3>3. Speichern der Macro-Datei und der Timecode-Datei:</h3><li>GrandMA2 Ordner ausw&aumlhlen, keine Unterordner z.B.: <b>X:/gma2/</b> oder <b>C:/ProgramData/MA Lighting Technologies/grandma/gma2_V_3.5</b></li><li>Wird kein externes Speichermedium ausgew&aumlhlt, so muss im Macro die 3. letze Zeile <b>'SelectDrive 4'</b> zu <b>'SelectDrive 1'</b> ge&aumlndert werden</li><li>Ist an der Konsole/Session mehr als ein USB-Stick angeschlossen, so muss im Macro die 3. letze Zeile <b>'SelectDrive 4'</b> zu <b>'SelectDrive X'</b> ge&aumlndert werden. Dabei ist X die Anzahl an USB-Sticks plus drei, oder die Tab-Position des USB-Sticks im Backup-Men&uuml!</li></p></body></html>";
            webBrowser1.DocumentText = web;
        }
    }
}
