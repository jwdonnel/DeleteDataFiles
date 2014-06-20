using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteDataFile {
    /// <summary>
    /// A simple class that defines the Console Text for reused strings
    /// </summary>
    public class ConsoleStrings {
        public static string NewLine {
            get {
                return Environment.NewLine;
            }
        }

        public static string Waiting {
            get {
                return NewLine + "  Waiting..." + NewLine;
            }
        }

        public static string Back {
            get {
                return NewLine + "  Press enter to go back";
            }
        }

        public const string NoFolderFiles = "  No folder list specified. Must have at least 1 folder listed.";
    }
}
