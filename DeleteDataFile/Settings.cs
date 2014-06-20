using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace DeleteDataFile {
    public class LoadSettings {

        #region public variables

        public List<FolderInfo> FolderInfoList = new List<FolderInfo>();
        public int CheckInterval = 1000;

        #endregion

        private const string settingsFile = "settings.xml";
        private string filePath = string.Empty;

        /// <summary>
        /// Load the Xml settings file that contains the filepath, output xml file name, temp directory, perform async, and perform sync.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public LoadSettings() {
            // assign path variables
            string absolutePath = Assembly.GetExecutingAssembly().Location;
            absolutePath = absolutePath.Substring(0, absolutePath.LastIndexOf(@"\"));
            filePath = Path.Combine(absolutePath, settingsFile);

            if (SettingsXmlExists) {
                // load xml settings file
                XmlDocument xml = new XmlDocument();
                try {
                    xml.Load(filePath);

                    // Get FolderFileList node
                    XmlNodeList folderFileNode = xml.SelectNodes("//FolderInfo");
                    foreach (XmlNode node in folderFileNode) {
                        XmlNode xmlNodeFolder = GetCorrectNode(node, "Folder");
                        XmlNode xmlNodeFiles = GetCorrectNode(node, "Files");

                        FolderInfo fi = new FolderInfo();

                        SetFolderName(ref fi, xmlNodeFolder);
                        SetFileList(ref fi, xmlNodeFiles);

                        if (fi.Files.Count == 0) {
                            // Check to make sure this is not the root of the drive
                            DirectoryInfo di = new DirectoryInfo(fi.FolderName);
                            if (fi.FolderName + "\\" == di.Root.Name) {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(fi.FolderName)) {
                            FolderInfoList.Add(fi);
                        }
                    }

                    // Get CheckInterval node
                    SetCheckInterval(xml);
                }
                catch { }
            }
        }

        private XmlNode GetCorrectNode(XmlNode node, string nodeName) {
            if (node.FirstChild.Name.ToLower() == nodeName.ToLower()) {
                return node.FirstChild;
            }
            else {
                return node.LastChild;
            }
        }

        private void SetFolderName(ref FolderInfo fi, XmlNode folderNode) {
            if ((folderNode != null) && (folderNode.FirstChild != null) && (folderNode.FirstChild.Value != null)) {
                string folder = folderNode.FirstChild.Value.Trim();
                if ((!string.IsNullOrEmpty(folder)) && (Path.IsPathRooted(folder))) {
                    fi.FolderName = folder;
                }
            }
        }

        private void SetFileList(ref FolderInfo fi, XmlNode fileNode) {
            fi.Files = new List<string>(); // Need to initialize List<string>

            if (fileNode != null) {
                XmlNodeList fileNodeList = fileNode.ChildNodes;
                foreach (XmlNode node in fileNodeList) {
                    if ((node != null) && (node.FirstChild != null) && (node.FirstChild.Value != null)) {
                        string file = node.FirstChild.Value.Trim();
                        Regex regFixFileName = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");
                        if (regFixFileName.IsMatch(file)) { continue; }
                        if ((!fi.Files.Contains(file)) && (!string.IsNullOrEmpty(file))) {
                            fi.Files.Add(file);
                        }
                    }
                }
            }
        }

        private void SetCheckInterval(XmlDocument xml) {
            XmlNode intervalNode = xml.SelectSingleNode("//CheckInterval");
            if ((intervalNode != null) && (intervalNode.FirstChild != null)) {
                int.TryParse(intervalNode.FirstChild.Value, out CheckInterval);
                if (CheckInterval < 1) {
                    CheckInterval = 1000;
                }
                else {
                    CheckInterval = CheckInterval * 1000;
                }
            }
        }

        public void UpdateCheckInterval(string val) {
            if (SettingsXmlExists) {
                // load xml settings file
                XmlDocument xml = new XmlDocument();
                try {
                    int finalVal = 1;
                    xml.Load(filePath);
                    XmlNode intervalNode = xml.SelectSingleNode("//CheckInterval");
                    if ((intervalNode != null) && (intervalNode.FirstChild != null)) {
                        int.TryParse(val, out finalVal);
                        if (finalVal < 1) {
                            finalVal = 1;
                        }
                        else {
                            finalVal = finalVal;
                        }

                        intervalNode.FirstChild.Value = finalVal.ToString();
                    }

                    xml.Save(filePath);
                }
                catch { }
            }
        }

        /// <summary>
        /// Create the default settings.xml file if its not found
        /// </summary>
        public void CreateSettingsXml() {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filePath, settings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");

                writer.WriteStartElement("FolderFileList");
                writer.WriteStartElement("FolderInfo");
                writer.WriteElementString("Folder", "DIRECTORY");
                writer.WriteStartElement("Files");
                writer.WriteElementString("File", "SOME FILE");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();


                writer.WriteStartElement("CheckInterval");
                writer.WriteAttributeString("help", "In seconds. Must be greater than or equal to 1");
                writer.WriteString("1");
                writer.WriteEndElement();

                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        public bool SettingsXmlExists {
            get {
                // Check if settings file exists
                if (!File.Exists(filePath)) {
                    return false;
                }
                return true;
            }
        }
    }
}
