﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ExportReaperMarkersToGrandMA2
{
    public enum TimelineFormat
    {
        HH_MM_SS_FF = 0,
        HH_MM_SS = 1,
        TotalFrames = 2
    }

    public enum FPS
    {
        FPS30 = 30,
        FPS25 = 25
    }

    public class Timecode
    {
        public static Timecode current;

        public TimecodeEvent[] TimecodeEvents { get; set; }
        public TimelineFormat TimelineFormat;
        public FPS Framerate;


        private int page;
        private int exec;
        private int seq_item;
        private string seq_Name;
        private int tc_item;
        private string tc_name;
        private TimecodeEventTrigger defaultTrigger;


        public Timecode(int page, int exec, int seq, string seqname, int tc, string tcname, FPS framerate, TimelineFormat timelineFormat, TimecodeEventTrigger defaultTrigger)
        {
            this.page = page;
            this.exec = exec;
            this.seq_item = seq;
            this.seq_Name = seqname;
            this.tc_item = tc;
            this.tc_name = tcname;
            this.Framerate = framerate;
            this.defaultTrigger = defaultTrigger;
            this.TimelineFormat = timelineFormat;

            current = this;
        }

        public void ParseCSV(string[] csvtext, FPS baseFramerate)
        {
            string[] names = csvtext[0].Split(',');

            TimecodeEvents = new TimecodeEvent[csvtext.Length-1];

            for (int i = 0; i < csvtext.Length-1; i++)
            {
                TimecodeEvents[i] = TimecodeEvent.ParseCSV(i, csvtext[i + 1], names, TimelineFormat, GetPage(), GetSeq(), baseFramerate, defaultTrigger);
            }
        }

        public void saveTimecodeXMLToFile(String path)
        {
            XmlDocument xmlDoc = createTimecodeXML();

            xmlDoc.Save(path + "\\" + GetTcName() + ".xml");   
        }

        public StreamReader GetTimecodeXMLStream()
        {
            XmlDocument xmlDoc = createTimecodeXML();

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                StringBuilder sb = stringWriter.GetStringBuilder();

                string s = sb.ToString();
                
                char[] c = s.ToCharArray();
                byte[] b = new byte[c.Length];
                for(int i = 0; i < b.Length; i++)
                {
                    b[i] = (byte) c[i];
                }
                
                MemoryStream memoryStream = new MemoryStream(b);
                memoryStream.Position = 0;

                StreamReader streamReader = new StreamReader(memoryStream);

                return streamReader;
            }
        }

        public XmlDocument createTimecodeXML() {

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8",null));
            xmlDoc.AppendChild(xmlDoc.CreateProcessingInstruction("xml-stylesheet", "type='text/xsl' href='styles/timecode@sheet.xsl'"));

            XmlNode nodeMA = xmlDoc.CreateElement("MA");

            XmlAttribute nodeMAAttribute_XMLNSXSI = xmlDoc.CreateAttribute("xmlns:xsi");
            nodeMAAttribute_XMLNSXSI.Value = "http://www.w3.org/2001/XMLSchema-instance";

            XmlAttribute nodeMAAttribute_XSI = xmlDoc.CreateAttribute("xsi:schemaLocation");
            nodeMAAttribute_XSI.Value = "http://schemas.malighting.de/grandma2/xml/MA http://schemas.malighting.de/grandma2/xml/3.4.0/MA.xsd";
            
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


            XmlNode nodeTimecode = xmlDoc.CreateElement("Timecode");
            XmlAttribute nodeTimecodeAttribute_Index = xmlDoc.CreateAttribute("index");
            nodeTimecodeAttribute_Index.Value = "0";

            XmlAttribute nodeTimecodeAttribute_Name = xmlDoc.CreateAttribute("name");
            nodeTimecodeAttribute_Name.Value = GetTcName();

            XmlAttribute nodeTimecodeAttribute_FramRate = xmlDoc.CreateAttribute("frame_format");
            nodeTimecodeAttribute_FramRate.Value = ((int)GetFrameRate()).ToString() + " FPS";

            nodeTimecode.Attributes.Append(nodeTimecodeAttribute_Index);
            nodeTimecode.Attributes.Append(nodeTimecodeAttribute_Name);
            nodeTimecode.Attributes.Append(nodeTimecodeAttribute_FramRate);
            nodeMA.AppendChild(nodeTimecode);

            XmlNode nodeTrack = xmlDoc.CreateElement("Track");
            XmlAttribute nodeTrackAttribute_Index = xmlDoc.CreateAttribute("index");
            nodeTrackAttribute_Index.Value = "0";

            nodeTrack.Attributes.Append(nodeTrackAttribute_Index);
            nodeTimecode.AppendChild(nodeTrack);

            XmlNode nodeTrackObject = xmlDoc.CreateElement("Object");
            XmlAttribute nodeTrackObjectAttribute_Name = xmlDoc.CreateAttribute("name");
            nodeTrackObjectAttribute_Name.Value = GetSeqName();

            nodeTrackObject.Attributes.Append(nodeTrackObjectAttribute_Name);
            nodeTrack.AppendChild(nodeTrackObject);


            XmlNode nodeNo30 = xmlDoc.CreateElement("No");
            nodeNo30.InnerText = "30";
            XmlNode nodeNoConsole = xmlDoc.CreateElement("No");
            nodeNoConsole.InnerText = "1";

            XmlNode nodeNoPage = xmlDoc.CreateElement("No");
            nodeNoPage.InnerText = GetPage().ToString();
            XmlNode nodeNoExec = xmlDoc.CreateElement("No");
            nodeNoExec.InnerText = GetExec().ToString();


            nodeTrackObject.AppendChild(nodeNo30);
            nodeTrackObject.AppendChild(nodeNoConsole);
            nodeTrackObject.AppendChild(nodeNoPage);
            nodeTrackObject.AppendChild(nodeNoExec);


            XmlNode nodeSubTrack = xmlDoc.CreateElement("SubTrack");
            XmlAttribute nodeSubTrackAttribute_Index = xmlDoc.CreateAttribute("index");
            nodeSubTrackAttribute_Index.Value = "0";

            nodeSubTrack.Attributes.Append(nodeSubTrackAttribute_Index);
            nodeTrack.AppendChild(nodeSubTrack);

            foreach (TimecodeEvent t in TimecodeEvents)
            {
                t.writeXML(nodeSubTrack, xmlDoc);
            }

            return xmlDoc;
        }

        public void saveMacroXML(string path)
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

            string[] macroLines = GetMacroLines();
            for (int i = 0; i < macroLines.Length; i++)
            {
                addMacroLine(xmlDoc, nodeMacro, i+1, macroLines[i]);
            }
            xmlDoc.Save(path + "\\" + tc_name + ".xml");

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

        public string[] GetMacroLines()
        {
            string[] macroLines = new string[this.TimecodeEvents.Length + 7];

            int index = 1;

            foreach (TimecodeEvent e in this.TimecodeEvents)
            {
                macroLines[index++] = "Store Seq " + seq_item + " Cue " + e.Cue + " \"" + e.Name + "\" /o /nc";
            }

            macroLines[index++] = "Label Seq " + seq_item + " \"" + seq_Name + "\"";
            macroLines[index++] = "Store Page " + page + "";
            macroLines[index++] = "Assign Seq " + seq_item + " At Exec 1." + page + "." + exec + "";
            macroLines[index++] = "SelectDrive 4";
            macroLines[index++] = "Import \"" + tc_name + ".xml\" At Timecode " + tc_item + " /o";
            macroLines[index++] = "Label Timecode " + tc_item + " \"" + tc_name + "\" /o /nc";

            return macroLines;
        }

        public int GetPage()
        {
            return page;
        }

        public void SetPage(int value)
        {
            this.page = value;
        }

        public int GetExec()
        {
            return exec;
        }

        public void SetExec(int value)
        {
            this.exec = value;
        }

        public int GetSeq()
        {
            return seq_item;
        }

        public void SetSeq(int value)
        {
            seq_item = value;
            foreach(TimecodeEvent t in TimecodeEvents)
            {
                t.Seq = value;
            }
        }

        public string GetSeqName()
        {
            return seq_Name;
        }

        public void SetSeqName(string value)
        {
            seq_Name = value;
        }

        public int GetTc()
        {
            return tc_item;
        }

        public void SetTc(int value)
        {
            tc_item = value;
        }

        public string GetTcName()
        {
            return tc_name;
        }

        public void SetTcName(string value)
        {
            tc_name = value;
        }

        public FPS GetFrameRate()
        {
            return Framerate;
        }

        public void SetFrameRate(FPS value)
        {
            Framerate = value;
            foreach (TimecodeEvent e in TimecodeEvents) e.SetFps(value);
        }

        public TimecodeEventTrigger GetDefaultTrigger()
        {
            return defaultTrigger;
        }

        public void SetDefaultTrigger(TimecodeEventTrigger value)
        {
            defaultTrigger = value;
            foreach (TimecodeEvent e in TimecodeEvents) e.trigger = value;
        }
    }
}

