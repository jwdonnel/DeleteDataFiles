using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteDataFile {
    /// <summary>
    /// Class for storing Folder Information
    /// </summary>
    public class FolderInfo {

        private string _folderName = string.Empty;

        public FolderInfo() { }

        public string FolderName {
            get {
                if ((_folderName.Length > 0) && (_folderName[_folderName.Length - 1] == '\\')) {
                    _folderName = _folderName.Remove(_folderName.Length - 1);
                }

                return _folderName;
            }
            set {
                _folderName = value;
            }
        }

        public List<string> Files { get; set; }
    }
}
