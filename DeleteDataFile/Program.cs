using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeleteDataFile {
    class Program {

        private const int maxLoopsLooking = 3;
        private static string dateStarted = string.Empty;
        private static LoadSettings settings = new LoadSettings();

        static void Main(string[] args) {
            WriteHeader();
            WriteMenu();
        }

        /// <summary>
        /// Write the title/header in the Console
        /// </summary>
        static void WriteHeader() {
            Console.Clear();
            Console.WriteLine(" ****************************************************************************");
            Console.WriteLine(" *                       DATA FILE/FOLDER REMOVER                           *");
            Console.WriteLine(" *                        By John Donnelly, 2014                            *");
            Console.WriteLine(" *                       http://my4dashboards.com                           *");
            Console.WriteLine(" *                                                                          *");
            Console.WriteLine(" *   This program is intended to only delete specified files and folders    *");
            Console.WriteLine(" *                This will run until window is closed.                     *");
            Console.WriteLine(" ****************************************************************************");
            Console.WriteLine(ConsoleStrings.NewLine);
        }
        static void WriteMenu() {
            Console.WriteLine("  MENU - Please choose an option.");
            Console.WriteLine("  ---------------------------------");
            if (settings.SettingsXmlExists) {
                Console.WriteLine("    1. Show Configuration");
                Console.WriteLine("    2. Show List of Files/Folders");
                Console.WriteLine("    3. Set Check Interval");
                Console.WriteLine("    4. Run Program");
                Console.WriteLine("    5. Exit");
                Console.WriteLine(ConsoleStrings.NewLine);
                Console.Write("  Enter an option (1-5): ");
                string option = Console.ReadLine();
                MenuSelection(option);
            }
            else {
                Console.WriteLine("    1. Create settings.xml File");
                Console.WriteLine("    2. Exit");
                Console.WriteLine(ConsoleStrings.NewLine);
                Console.Write("  Enter an option (1-2): ");
                string option = Console.ReadLine();
                MenuSelectionNoFile(option);
            }
        }
        static void MenuSelection(string option) {
            switch (option) {
                case "1": // Show Configuration
                    ShowConfiguration();
                    break;

                case "2": // Show Files/Folders
                    ShowFileFolderList();
                    break;

                case "3": // Set Interval
                    SetCheckInterval();
                    break;

                case "4": // Run
                    Thread t = new Thread(RunProgram);
                    t.Start();
                    break;

                case "5": // Exit
                    Environment.Exit(0);
                    break;

                default:  // Reset
                    ResetConsole();
                    break;
            }
        }
        static void MenuSelectionNoFile(string option) {
            switch (option) {
                case "1": // Create settings.xml
                    settings.CreateSettingsXml();
                    ResetConsole();
                    break;

                case "2": // Exit
                    Environment.Exit(0);
                    break;

                default:  // Reset
                    ResetConsole();
                    break;
            }
        }

        static void ResetConsole() {
            WriteHeader();
            WriteMenu();
        }

        /// <summary>
        /// Runs the loop that will search and delete the file(s)/folder
        /// </summary>
        static void RunProgram() {
            WriteHeader();

            if (settings.FolderInfoList.Count == 0) {
                Console.WriteLine(ConsoleStrings.NoFolderFiles);
                Console.Write(ConsoleStrings.Back);
                Console.Read();
                ResetConsole();
            }
            else {
                Console.WriteLine("  SEARCHING FOR FILES/FOLDERS");
                Console.WriteLine("  ---------------------------------");
                Console.WriteLine(ConsoleStrings.Waiting);

                bool foundOne = false;
                int loopsLooking = 0;
                while (true) {

                    if (loopsLooking == maxLoopsLooking) {
                        Console.WriteLine(ConsoleStrings.Waiting);
                    }

                    Thread.Sleep(settings.CheckInterval);

                    bool found = false;

                    foreach (FolderInfo fi in settings.FolderInfoList) {
                        string time = DateTime.Now.ToString().Replace(DateTime.Now.ToShortDateString() + " ", "");
                        if (DateTime.Now.ToShortDateString() != dateStarted) {
                            time = DateTime.Now.ToString();
                        }

                        if (fi.Files.Count == 0) {
                            #region Delete Folder Only
                            if (Directory.Exists(fi.FolderName)) {
                                try {
                                    Directory.Delete(fi.FolderName, true);
                                    Console.WriteLine("  " + fi.FolderName + " deleted at " + time);
                                }
                                catch (Exception e) {
                                    Console.WriteLine("  ERROR: " + e.Message + " - " + time);
                                }

                                found = true;
                                foundOne = true;
                            }
                            #endregion
                        }
                        else {
                            #region Delete Files Only
                            foreach (string file in fi.Files) {
                                string fullPath = Path.Combine(fi.FolderName + "\\", file);
                                if (File.Exists(fullPath)) {
                                    try {
                                        File.Delete(fullPath);
                                        Console.WriteLine("  " + file + " deleted at " + time);
                                    }
                                    catch (Exception e) {
                                        Console.WriteLine("  ERROR: " + e.Message + " - " + time);
                                    }

                                    found = true;
                                    foundOne = true;
                                }
                            }
                            #endregion
                        }
                    }

                    if ((!found) && (foundOne)) {
                        loopsLooking++;
                    }
                    else {
                        loopsLooking = 0;
                    }
                }
            }
        }
        static void SetCheckInterval() {
            WriteHeader();
            Console.WriteLine("  UPDATE CHECK INTERVAL");
            Console.WriteLine("  ---------------------------------");
            Console.WriteLine(ConsoleStrings.NewLine);

            Console.WriteLine("  Current Value: " + (settings.CheckInterval / 1000).ToString());

            Console.Write("  New Value: ");
            string val = Console.ReadLine();
            if (!string.IsNullOrEmpty(val)) {
                settings.UpdateCheckInterval(val);
                settings = new LoadSettings();
            }

            ResetConsole();
        }
        static void ShowConfiguration() {
            string pluralSec = (settings.CheckInterval / 1000).ToString() + " Seconds";
            if (settings.CheckInterval == 1000) {
                pluralSec = "Second";
            }

            dateStarted = DateTime.Now.ToShortDateString();

            WriteHeader();
            Console.WriteLine("  CONFIGURATION");
            Console.WriteLine("  ---------------------------------");
            Console.WriteLine(ConsoleStrings.NewLine);

            Console.WriteLine("  Date/Time Started: " + DateTime.Now.ToString());
            Console.WriteLine("  Check Interval: Every " + pluralSec);

            Console.Write(ConsoleStrings.Back);
            Console.Read();
            ResetConsole();
        }
        static void ShowFileFolderList() {
            WriteHeader();
            Console.WriteLine("  FILES/FOLDERS LIST");
            Console.WriteLine("  ---------------------------------");
            Console.WriteLine(ConsoleStrings.NewLine);

            bool hasFolder = false;
            int totalFolderCount = 0;
            int totalFileCount = 0;

            if (settings.FolderInfoList.Count > 0) {
                foreach (FolderInfo folderInfo in settings.FolderInfoList) {
                    if (string.IsNullOrEmpty(folderInfo.FolderName)) {
                        continue;
                    }

                    hasFolder = true;

                    Console.WriteLine("  Folder: " + folderInfo.FolderName);

                    if (folderInfo.Files.Count > 0) {
                        for (int i = 0; i < folderInfo.Files.Count; i++) {
                            string file = folderInfo.Files[i];
                            if (i == 0) {
                                Console.WriteLine("  Files: " + file);
                            }
                            else {
                                Console.WriteLine("         " + file);
                            }
                            totalFileCount++;
                        }
                    }
                    else {
                        totalFolderCount++;
                    }

                    Console.WriteLine(ConsoleStrings.NewLine);
                }
            }

            if (hasFolder) {
                Console.WriteLine("  Total Folders: " + totalFolderCount);
                Console.WriteLine("  Total Files: " + totalFileCount);
            }
            else {
                Console.WriteLine(ConsoleStrings.NoFolderFiles);
            }

            Console.Write(ConsoleStrings.Back);
            Console.Read();
            ResetConsole();
        }
    }
}
