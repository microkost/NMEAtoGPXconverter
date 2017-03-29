using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using NmeaParser;
using System.Globalization;

namespace MS_NMEAconverter
{
    public partial class NMEAconverterForm : Form
    {
        private string PathToFile { get; set; }

        public List<String> NMEApure { get; set; }

        public List<NMEASentence> NMEAsentences { get; set; }
        public String GPXsentences { get; set; }

        private Dictionary<string, string> global; //for global data and sentences

        const int showedrecords = 400; //just limit for txt output
        public NMEAconverterForm()
        {
            InitializeComponent();
            NMEApure = new List<String>();
            NMEAsentences = new List<NMEASentence>();
            global = new Dictionary<string, string>();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US"); //because of GPX national format (dates)
        }

        private void buttonOpenNMEA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogNMEA = new OpenFileDialog();

            if (Directory.Exists(textBoxNMEApath.Text))
            {
                openFileDialogNMEA.InitialDirectory = textBoxNMEApath.Text; //preset
                if (File.Exists(textBoxNMEApath.Text))
                {
                    PathToFile = textBoxNMEApath.Text;
                }
            }

            openFileDialogNMEA.Filter = "NMEA files (*.nmea)|*.nmea|All files (*.*)|*.*";

            if (openFileDialogNMEA.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((PathToFile = openFileDialogNMEA.FileName) != null)
                    {
                        textBoxNMEApath.Text = PathToFile;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            processLoadedNMEA(); //autoclick
        }

        private void processLoadedNMEA()
        {
            if (PathToFile == null || !File.Exists(textBoxNMEApath.Text))
            {
                textBoxOutput.AppendText("Error of loading line no." + NMEApure.Count + 1 + "\n");
                return;
            }

            try
            {
                NMEApure.Clear();
                GPXsentences = "";

                string line;
                StreamReader file = new System.IO.StreamReader(PathToFile); // Read the file and display it line by line.
                while ((line = file.ReadLine()) != null)
                {
                    try
                    {
                        NMEApure.Add(string.Format("{0}{1}", line, "\r\n"));
                    }
                    catch
                    {
                        textBoxOutput.AppendText("Error of loading line no." + NMEApure.Count + 1 + "\n");
                        continue;
                    }
                }
                labelCounter.Text = "Records: " + NMEApure.Count.ToString(); //should be there statistic about errors
                file.Close();
            }
            catch
            {
                MessageBox.Show("The file could not be read:");
            }

            GPXsentences = SentenceNMEA2GPX(NMEApure); //parsing whole list

            /*foreach (string textsentence in NMEApure) //parsing every sentence 
            {
                try // try to parse nmea sentence
                {
                    var parsedSentence = NMEAParser.Parse(textsentence);
                    if (parsedSentence is NMEAStandartSentence)
                    {
                        NMEAStandartSentence sentence = (parsedSentence as NMEAStandartSentence);

                        if (sentence.TalkerID == TalkerIdentifiers.GP) //generic GPS
                        {
                            GPXsentences.Add(SentenceNMEA2GPX(sentence)); //parsing and showing

                            if (NMEApure.Count < showedrecords)
                            {
                                textBoxOutput.AppendText(GPXsentences.Last());
                            }
                            else
                            {
                                //textBoxOutput.AppendText("printing of output skipped");
                            }

                        }
                        else
                        {
                            if (NMEApure.Count < showedrecords)
                            {
                                textBoxOutput.AppendText("Record done by non generic GPS, skipping...\n");
                            }
                        }
                    }
                    else
                    {
                        if (parsedSentence is NMEAProprietarySentence)
                        {
                            textBoxOutput.AppendText("Sentence is proprietary, skipping...\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    textBoxOutput.AppendText(string.Format("{0}: {1}\n", ex.Message, textsentence));
                    continue;
                }
            }*/
        }

        private string SentenceNMEA2GPX(List<String> sentences)
        {
            List<String> sb = new List<String>();
            int lastchecked = 0; //for speed-up lookup for already updated records

            sb.Add("<gpx>\n<trk>\n");
            sb.Add(String.Format("<name>{0}</name>\n", "Something what can be changed easily"));
            sb.Add("<trkseg>\n");

            foreach (string textsentence in sentences)
            {
                var parsedSentence = NMEAParser.Parse(textsentence);
                if (parsedSentence is NMEAStandartSentence)
                {
                    NMEAStandartSentence sentence = (parsedSentence as NMEAStandartSentence);

                    if (sentence.TalkerID == TalkerIdentifiers.GP) //generic GPS
                    {
                        if (sentence.SentenceID == SentenceIdentifiers.GGA)
                        {
                            //http://www.gpsinformation.org/dale/nmea.htm#GGA

                            //parsing in library is not precise, but easy
                            //sb.Add(String.Format("  <trkpt lat=\"{0}\" lon=\"{1}\">\n", string.Format("{0}", sentence.parameters[1]), string.Format("{0}", sentence.parameters[3])));

                            //extra parsing for lon, lat, speed
                            string[] parsed = textsentence.Split(',');
                            string lat = String.Format("{0}", Convert.ToInt32(parsed[2].Substring(0, 2)) + ((parsed[3] == "N") ? Convert.ToDouble(parsed[2].Substring(2)) / 60 : Convert.ToDouble(parsed[2].Substring(2)) / -60)).ToString();
                            string lon = String.Format("{0}", Convert.ToInt32(parsed[4].Substring(0, 3)) + ((parsed[5] == "E") ? Convert.ToDouble(parsed[4].Substring(3)) / 60 : Convert.ToDouble(parsed[2].Substring(4)) / -60)).ToString();
                            sb.Add(String.Format("<trkpt lat=\"{0}\" lon=\"{1}\">\n", string.Format("{0}", lat), string.Format("{0}", lon)));

                            sb.Add(String.Format("   <ele>{0}</ele>\n", sentence.parameters[8])); //elevation
                            sb.Add(String.Format("   <time>{0}</time>\n", (global.Keys.Contains("time") && global.Keys.Contains("date")) ? String.Format("{0}T{1}Z", global["date"], global["time"]) : "")); //inserted in case that global record is provided otherwise updated later //chyba formátu
                            sb.Add(String.Format("   <course>{0}</course>\n", global.Keys.Contains("course") ? global["course"] : ""));
                            sb.Add(String.Format("   <speed>{0}</speed>\n", global.Keys.Contains("speed") ? global["speed"] : ""));
                            sb.Add(String.Format("   <sat>{0}</sat>\n", sentence.parameters[6]));
                            sb.Add(String.Format("   <hdop>{0}</hdop>\n", sentence.parameters[7]));
                            sb.Add("</trkpt>\n");
                        }

                        if (sentence.SentenceID == SentenceIdentifiers.RMC) //control sentence
                        {
                            //http://www.gpsinformation.org/dale/nmea.htm#RMC

                            //sets global variables for post-update
                            string[] dates = sentence.parameters[8].ToString().Split(new char[] { ' ' });
                            string date = dates[0];
                            string year = date.Split(new char[] { '/' })[2]; //EN settings above!
                            string month = date.Split(new char[] { '/' })[1];
                            string day = date.Split(new char[] { '/' })[0];

                            string[] times = sentence.parameters[0].ToString().Split(new char[] { ' ' });
                            string time = times[1];

                            //update if new RMC
                            global["date"] = String.Format("{0}-{1}-{2}", year, month, day);
                            global["time"] = time; //update
                            global["course"] = sentence.parameters[7].ToString();
                            global["speed"] = Convert.ToString(Convert.ToDouble(sentence.parameters[6]) * 0.514); //convert value

                            for (int i = lastchecked; i < sb.Count; i++) //post-update
                            {
                                if (sb[i].Equals("   <time></time>\n"))
                                {
                                    sb[i] = String.Format("   <time>{0}T{1}Z</time>\n", global["date"], global["time"]); //if missing generally then actual
                                }
                                else if (sb[i].Equals("   <course></course>\n"))
                                {
                                    sb[i] = String.Format("   <course>{0}</course>\n", global["course"]);
                                }
                                else if (sb[i].Equals("   <speed></speed>\n"))
                                {
                                    sb[i] = String.Format("   <speed>{0}</speed>\n", global["speed"]);
                                }
                            }
                            lastchecked = sb.Count;
                        }
                    }
                }
            }
            sb.Add("</trkseg>\n");
            sb.Add("</trk>\n</gpx>\n");
            return String.Join("", sb.ToArray());
        }

        private void buttonSaveGPX_Click(object sender, EventArgs e)
        {
            if (GPXsentences == "")
            {
                textBoxOutput.AppendText("Nothing to put on output\n");
                return;
            }

            SaveFileDialog saveFileDialogGPX = new SaveFileDialog();

            saveFileDialogGPX.Filter = "GPX files (*.gpx)|*.gpx|All files (*.*)|*.*";
            saveFileDialogGPX.FileName = "test";
            saveFileDialogGPX.DefaultExt = "gpx";
            saveFileDialogGPX.RestoreDirectory = true;

            if (saveFileDialogGPX.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(saveFileDialogGPX.FileName, GPXsentences);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Troubles with file, access failed: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selection of output failed, terminated.");
                return;
            }

            textBoxOutput.AppendText("Export finished\n");
        }
    }
}