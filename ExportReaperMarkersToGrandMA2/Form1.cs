﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ExportReaperMarkersToGrandMA2
{
    public partial class Form1 : Form
    {
        Timecode timecode;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            string defaultPath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV-Files(*.csv) | *.csv";
            openFileDialog.FilterIndex = 0;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                defaultPath = openFileDialog.FileName;
                
                StreamReader sr = new StreamReader(defaultPath);
                string temp = sr.ReadLine();
                List<string> csv = new List<string>();
                while (temp != null)
                {
                    csv.Add(temp);
                    temp = sr.ReadLine();
                }
                
                timecode = new Timecode((int) num_ExecPage.Value, (int) num_ExecItem.Value, (int) num_SeqItem.Value, txt_SeqName.Text, (int) num_TcItem.Value, txt_TcName.Text, (int) num_TcFrameRate.Value);
                if (!timecode.ParseCSV(csv.ToArray()))
                {
                    MessageBox.Show("Die ausgewählte Datei entspricht nicht dem erwarteten Format!\n" +
                        "Folgende Punkte müssen beachtet werden:\n\n" +
                        "- Es muss eine von Reaper erstellte Datei ausgewählt werden\n" +
                        "- Die Timeline muss auf dem Format:\n" +
                        "\t 'Hours:Minutes:Seconds:Frames' \n" +
                        "  stehen(Rechtsklick auf die Timeline)", "Fehler beim lesen der Datei!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sr.Close();
                    return;
                }

                gB_Save.Visible = true;
                gB_Timecode.Visible = true;
                txt_Open.Text = defaultPath;

                dataGridView1.DataSource = timecode.timecodeEvents.ToList();
                dataGridView1.AllowUserToResizeColumns = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                sr.Close();
            }

            
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txt_Save.Text = folderBrowserDialog.SelectedPath;

                if (!Directory.Exists(folderBrowserDialog.SelectedPath + "\\importexport"))
                {
                    Directory.CreateDirectory(folderBrowserDialog.SelectedPath + "\\importexport");
                }
                if (!Directory.Exists(folderBrowserDialog.SelectedPath + "\\macros"))
                {
                    Directory.CreateDirectory(folderBrowserDialog.SelectedPath + "\\macros");
                }


                timecode.save(folderBrowserDialog.SelectedPath + "\\importexport");
                saveMacro(folderBrowserDialog.SelectedPath + "\\macros");

                MessageBox.Show("Datei gespeichert!", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void saveMacro(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));

            XmlNode nodeMA = xmlDoc.CreateElement("MA");

            XmlAttribute nodeMAAttribute_XMLNSXSI = xmlDoc.CreateAttribute("xmlns:xsi");
            nodeMAAttribute_XMLNSXSI.Value = "http://www.w3.org/2001/XMLSchema-instance";

            XmlAttribute nodeMAAttribute_XMLNS = xmlDoc.CreateAttribute("xmlns");
            nodeMAAttribute_XMLNS.Value = "http://schemas.malighting.de/grandma2/xml/MA";

            XmlAttribute nodeMAAttribute_XSI = xmlDoc.CreateAttribute("xsi:schemaLocation");
            nodeMAAttribute_XSI.Value = "http://schemas.malighting.de/grandma2/xml/MA http://schemas.malighting.de/grandma2/xml/3.4.0/MA.xsd";

            nodeMA.Attributes.Append(nodeMAAttribute_XMLNS);
            nodeMA.Attributes.Append(nodeMAAttribute_XMLNSXSI);
            nodeMA.Attributes.Append(nodeMAAttribute_XSI);
            xmlDoc.AppendChild(nodeMA);

            XmlNode nodeInfo = xmlDoc.CreateElement("Info");
            XmlAttribute nodeInfoAttribute_DateTime = xmlDoc.CreateAttribute("index");
            nodeInfoAttribute_DateTime.Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            XmlAttribute nodeInfoAttribute_Showfile = xmlDoc.CreateAttribute("name");
            nodeInfoAttribute_Showfile.Value = "ExportReaperMarkerToGrandMA2";

            nodeInfo.Attributes.Append(nodeInfoAttribute_DateTime);
            nodeInfo.Attributes.Append(nodeInfoAttribute_Showfile);
            nodeMA.AppendChild(nodeInfo);

            XmlNode nodeMacro = xmlDoc.CreateElement("Macro");
            XmlAttribute nodeMacroAttrib_index = xmlDoc.CreateAttribute("index");
            nodeMacroAttrib_index.Value = "1";

            XmlAttribute nodeMacroAttrib_name = xmlDoc.CreateAttribute("name");
            nodeMacroAttrib_name.Value = "Import Reaper Marker as Timecode";

            nodeMacro.Attributes.Append(nodeMacroAttrib_index);
            nodeMacro.Attributes.Append(nodeMacroAttrib_name);
            nodeMA.AppendChild(nodeMacro);

            int index = 1;

            foreach (TimecodeEvent e in timecode.timecodeEvents)
            {
                addMacroLine(xmlDoc, nodeMacro, index++, "Store Seq " + num_SeqItem.Value + " Cue " + e.Cue + " \"" + e.Name + "\" /o /nc");
            }

            addMacroLine(xmlDoc, nodeMacro, index++, "Label Seq " + num_SeqItem.Value + " \"" + txt_SeqName.Text + "\"");
            addMacroLine(xmlDoc, nodeMacro, index++, "Store Page " + num_ExecPage.Value + "");
            addMacroLine(xmlDoc, nodeMacro, index++, "Assign Seq " + num_SeqItem.Value + " At Exec 1." + num_ExecPage.Value + "." + num_ExecItem.Value + "");
            addMacroLine(xmlDoc, nodeMacro, index++, "SelectDrive 4");
            addMacroLine(xmlDoc, nodeMacro, index++, "Import \"" + txt_TcName.Text + ".xml\" At Timecode " + num_TcItem.Value + " /o");
            addMacroLine(xmlDoc, nodeMacro, index++, "Label Timecode " + num_TcItem.Value + " \"" + txt_TcName.Text + "\"");


            xmlDoc.Save(path + "\\" + txt_TcName.Text + ".xml");

        }

        private void addMacroLine(XmlDocument doc, XmlNode parent, int index, string cmd)
        {
            XmlNode node = doc.CreateElement("Macroline");
            XmlAttribute node_index = doc.CreateAttribute("index");
            node_index.Value = index.ToString();

            XmlNode node_text = doc.CreateElement("text");
            node_text.InnerText = cmd;

            node.Attributes.Append(node_index);
            node.AppendChild(node_text);
            parent.AppendChild(node);
        }


        #region Eventhandler
        private void num_SeqItem_ValueChanged(object sender, EventArgs e)
        {
            timecode.SetSeq((int) num_SeqItem.Value);
        }

        private void txt_SeqName_TextChanged(object sender, EventArgs e)
        {
            timecode.SetSeqName(txt_SeqName.Text);
        }

        private void num_ExecPage_ValueChanged(object sender, EventArgs e)
        {
            timecode.SetPage((int) num_ExecPage.Value);
        }

        private void num_ExecItem_ValueChanged(object sender, EventArgs e)
        {
            timecode.SetExec((int) num_ExecItem.Value);
        }

        private void num_TcItem_ValueChanged(object sender, EventArgs e)
        {
            timecode.SetTc((int) num_TcItem.Value);
        }

        private void num_TcFrameRate_ValueChanged(object sender, EventArgs e)
        {
            timecode.SetFrameRate((int) num_TcFrameRate.Value);
        }

        private void txt_TcName_TextChanged(object sender, EventArgs e)
        {
            timecode.SetTcName(txt_TcName.Text);
        }
        #endregion

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.SelectedCells[0].ColumnIndex == 1)
            {
                using (var form = new FormTime(timecode.timecodeEvents[dataGridView1.SelectedCells[0].RowIndex].Time, (int) num_TcFrameRate.Value))
                {
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        timecode.timecodeEvents[dataGridView1.SelectedCells[0].RowIndex].Time = form.time;
                    }
                }

                dataGridView1.EndEdit();
            }
        }
    }
}
